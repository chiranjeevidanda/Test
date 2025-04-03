using MediatR;
using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options;
using Newtonsoft.Json.Linq;

namespace NEC.Fulf3PL.Application.Admin.Command;

public record RetriggerFailedDocumentCommand(RetriggerRequestDto RequestDto) : IRequest<RetriggerResponseDto>;

public class RetriggerFailedDocumentCommandHandler : IRequestHandler<RetriggerFailedDocumentCommand, RetriggerResponseDto>
{
    private readonly IRetriggerDocumentService _documentService;

    private readonly IAdminOutboundRequestsQueryService _outboundDocumentQueryService;
    private readonly ISapTransactionsQueryService _sapTransactionsQueryService;
    private readonly AdminInboundCustomerOptions _inboundCustomerOptions;
    private readonly AdminOutboundCustomerOptions _outboundCustomerOptions;
    private readonly ExculdePlantOrderOptions _exculdePlantOrder;
    private readonly IInboundServiceBusQueueService _inboundServiceBusQueueService;
    private readonly CustomerPlantOptions _customerPlant;
    public RetriggerFailedDocumentCommandHandler(IRetriggerDocumentService documentService,
    IAdminOutboundRequestsQueryService outboundDocumentQueryService,
    ISapTransactionsQueryService sapTransactionsQueryService, IOptions<AdminInboundCustomerOptions> inboundCustomerOptions, IOptions<AdminOutboundCustomerOptions> outboundCustomerOptions, IOptions<ExculdePlantOrderOptions> exculdePlantOrder,
    IInboundServiceBusQueueService inboundServiceBusQueueService, IOptions<CustomerPlantOptions> customerPlant)
    {
        _documentService = documentService;
        _outboundDocumentQueryService = outboundDocumentQueryService;
        _sapTransactionsQueryService = sapTransactionsQueryService;
        _inboundCustomerOptions = inboundCustomerOptions.Value;
        _outboundCustomerOptions = outboundCustomerOptions.Value;
        _exculdePlantOrder = exculdePlantOrder.Value;
        _inboundServiceBusQueueService = inboundServiceBusQueueService;
        _customerPlant = customerPlant.Value;
    }

    public async Task<RetriggerResponseDto> Handle(RetriggerFailedDocumentCommand request, CancellationToken cancellationToken)
    {
        var documentsToRetrigger = new RetriggerDocumentsListDto();
        var totalDocs = 0;
        var payLoad = Constants.InboundRequest;
        if (!string.IsNullOrEmpty(request.RequestDto.EventId) && !string.IsNullOrEmpty(request.RequestDto.Payload))
        {
            var eventDetails = await GetInboundOutboundEventByEventIdAsync(new string[] { request.RequestDto.EventId });
            if (eventDetails.Count > 0)
            {
                documentsToRetrigger.RetriggerEventDetails = new List<RetriggerEventDetail>() { new RetriggerEventDetail() { EventId = request.RequestDto.EventId, RequestType = eventDetails.FirstOrDefault()?.RequestType } };
                documentsToRetrigger.RequestType = eventDetails.FirstOrDefault()?.RequestType;
                documentsToRetrigger.RetriggerPayload = request.RequestDto.Payload;
                documentsToRetrigger.EventId = request.RequestDto.EventId;
                totalDocs = 1;
            }
        }
        else if (!string.IsNullOrEmpty(request.RequestDto.EventId))
        {
            var eventIds = request.RequestDto.EventId.Replace(" ", ",").Split(",", StringSplitOptions.RemoveEmptyEntries);

            List<RetriggerEventDetail> eventDetails = await GetInboundOutboundEventByEventIdAsync(eventIds);

            totalDocs = SetDocumentId(documentsToRetrigger, eventDetails);
        }
        else if (request.RequestDto.SingleDate != null && request.RequestDto.IsSingleDate.HasValue && request.RequestDto.IsSingleDate.Value)
        {
            List<RetriggerEventDetail> eventDetails = await GetInboundOutboundEventBySingleDateAsync(request);

            totalDocs = SetDocumentId(documentsToRetrigger, eventDetails);
        }
        else if (request.RequestDto.DateFrom != null && request.RequestDto.DateTo != null)
        {
            List<RetriggerEventDetail> items = await GetInboundOutboundEventByDateRangeAsync(request);

            totalDocs = SetDocumentId(documentsToRetrigger, items);
        }
        long requestActiveCount = 0;
        if (totalDocs > 0)
        {
            await _documentService.PostRetriggerDocuments(documentsToRetrigger);

            var isInboundTransactions = documentsToRetrigger.RetriggerEventDetails.Exists(x => x.RequestType == RequestType.Inventory ||
    x.RequestType == RequestType.GoodsIssued ||
    x.RequestType == RequestType.GoodsReceived ||
    x.RequestType == RequestType.ReturnReceipt);
            if (isInboundTransactions == true)
            {
                requestActiveCount = await GetActiveTransactionCount();
                payLoad = Constants.InboundRequest;
            }
            else
            {
                payLoad = Constants.OutboundRequest;
            }
        }

        return new RetriggerResponseDto() { PayloadType = payLoad, SuccessCount = totalDocs, ActiveMessageCount = requestActiveCount };
    }
    private async Task<long> GetActiveTransactionCount()
    {

        var activeServiceBusMessageCount = await _inboundServiceBusQueueService.GetActiveMessageCountAsync();
        return activeServiceBusMessageCount.Values.Sum();
    }

    private async Task<List<RetriggerEventDetail>> GetInboundOutboundEventBySingleDateAsync(RetriggerFailedDocumentCommand request)
    {
        var reporcessRequest = new List<RetriggerEventDetail>();
        if (request.RequestDto.DocumentType == OutboundTransactionRequestType.ProductMaster || request.RequestDto.DocumentType == OutboundTransactionRequestType.PurchaseOrder || request.RequestDto.DocumentType == OutboundTransactionRequestType.CreateOrder || request.RequestDto.DocumentType == OutboundTransactionRequestType.ReturnOrder || string.IsNullOrEmpty(request.RequestDto.DocumentType))
        {
            IEnumerable<RetriggerEventDetail> outboundDocument = await GetOutboundDocuments(request);
            reporcessRequest.AddRange(outboundDocument);
        }

        var validCustomers = GetValidCustomers();
        var excludeGoodsIssuedOrder = GetExculdeInboundOrderForDocumentType(nameof(InboundTransactionRequestType.GoodsIssued));
        var excludeReturnReceivedOrder = GetExculdeInboundOrderForDocumentType(nameof(InboundTransactionRequestType.ReturnReceived));

        var inboundEvent = await GetInboundEventBySingleDateAsync(request, validCustomers, excludeGoodsIssuedOrder, excludeReturnReceivedOrder);

        reporcessRequest.AddRange(inboundEvent);

        return reporcessRequest;
    }

    private async Task<IEnumerable<RetriggerEventDetail>> GetOutboundDocuments(RetriggerFailedDocumentCommand request)
    {
        var validCustomers = GetCustomerForDocumentType(request.RequestDto.DocumentType);

        var outboundDocument = await _outboundDocumentQueryService.GetItemsAsync(x =>
        x.ModifiedDate <= request.RequestDto.SingleDate && x.Provider == _customerPlant.ApplicationProvider &&
        x.Status == TransactionStatus.Failed && validCustomers.Contains(x.Customer)
        && (request.RequestDto.DocumentType != null ? x.DocumentType == request.RequestDto.DocumentType : true),
            x => new RetriggerEventDetail { RequestType = x.DocumentType, EventId = x.Id });
        return outboundDocument;
    }

    private async Task<IEnumerable<RetriggerEventDetail>> GetInboundEventBySingleDateAsync(RetriggerFailedDocumentCommand request, ValidCustomerLists validCustomers, ExculdePlantOrder? excludeGoodsIssuedOrder, ExculdePlantOrder? excludeReturnReceivedOrder)
    {
        var result = await _sapTransactionsQueryService.GetItemsAsync(x => x.RequestPayload.Timestamp <= request.RequestDto.SingleDate && x.Provider == _customerPlant.ApplicationProvider &&
                        x.Status == false && x.ProcessId != null && x.ProcessId != string.Empty && (
                        (x.RequestType == nameof(InboundTransactionRequestType.Inventory) && validCustomers.InventoryCustomer.Contains(x.RequestPayload.Plant))
                        ||
                        (x.RequestType == nameof(InboundTransactionRequestType.GoodsIssued) && validCustomers.GoodsIssuedCustomer.Contains(x.RequestPayload.VendorId)
                        )
                        ||
                        (x.RequestType == nameof(InboundTransactionRequestType.GoodsReceived) && validCustomers.GoodsReceivedCustomer.Contains(x.RequestPayload.VendorId))
                        ||
                        (x.RequestType == nameof(InboundTransactionRequestType.ReturnReceived) && validCustomers.ReturnReceivedCustomer.Contains(x.RequestPayload.VendorId)
                        )),
                        x => new
                        {
                            RequestType = x.RequestType,
                            EventId = x.Id,
                            VendorId = x.RequestPayload.VendorId,
                            Plant = x.RequestPayload.Plant,
                        });

        var inboundEvent = result.Select(x => new RetriggerEventDetail
        {
            RequestType = x.RequestType,
            EventId = x.EventId,
            Customer = x.RequestType == nameof(InboundTransactionRequestType.Inventory) ? x.Plant : x.VendorId
        });

        return string.IsNullOrEmpty(request.RequestDto.DocumentType) ?
                                          inboundEvent :
                                          inboundEvent.Where(X => X.RequestType == request.RequestDto.DocumentType);
    }

    private async Task<List<RetriggerEventDetail>> GetInboundOutboundEventByDateRangeAsync(RetriggerFailedDocumentCommand request)
    {
        var reporcessRequest = new List<RetriggerEventDetail>();

        if (request.RequestDto.DocumentType == OutboundTransactionRequestType.ProductMaster || request.RequestDto.DocumentType == OutboundTransactionRequestType.PurchaseOrder || request.RequestDto.DocumentType == OutboundTransactionRequestType.CreateOrder || request.RequestDto.DocumentType == OutboundTransactionRequestType.ReturnOrder || string.IsNullOrEmpty(request.RequestDto.DocumentType))
        {
            var outbound = await GetOutboundEventByDateRange(request);
            reporcessRequest.AddRange(outbound);
            return reporcessRequest;
        }

        var validCustomers = GetValidCustomers();
        var excludeGoodsIssuedOrder = GetExculdeInboundOrderForDocumentType(nameof(InboundTransactionRequestType.GoodsIssued));
        var excludeReturnReceivedOrder = GetExculdeInboundOrderForDocumentType(nameof(InboundTransactionRequestType.ReturnReceived));

        var inboundEvent = await GetInboundEventByDateRangeAsync(request, validCustomers, excludeGoodsIssuedOrder, excludeReturnReceivedOrder);

        reporcessRequest.AddRange(inboundEvent);

        return reporcessRequest;
    }

    private async Task<IEnumerable<RetriggerEventDetail>> GetOutboundEventByDateRange(RetriggerFailedDocumentCommand request)
    {
        var validCustomers = GetCustomerForDocumentType(request.RequestDto.DocumentType);

        return await _outboundDocumentQueryService.GetItemsAsync(x =>
         x.ModifiedDate >= request.RequestDto.DateFrom.Value.Date.AddTicks(-1) &&
         x.ModifiedDate <= request.RequestDto.DateTo && x.Provider == _customerPlant.ApplicationProvider &&
         x.Status == TransactionStatus.Failed
         && validCustomers.Contains(x.Customer)
          && (request.RequestDto.DocumentType != null ? x.DocumentType == request.RequestDto.DocumentType : true),
             x => new RetriggerEventDetail { RequestType = x.DocumentType, EventId = x.Id, Customer = x.Customer });
    }

    private async Task<IEnumerable<RetriggerEventDetail>> GetInboundEventByDateRangeAsync(RetriggerFailedDocumentCommand request, ValidCustomerLists validCustomers, ExculdePlantOrder? excludeGoodsIssuedOrder, ExculdePlantOrder? excludeReturnReceivedOrder)
    {
        var result = await _sapTransactionsQueryService.GetItemsAsync(x => x.RequestPayload.Timestamp >= request.RequestDto.DateFrom.Value.Date.AddTicks(-1) && x.Provider == _customerPlant.ApplicationProvider &&
        x.RequestPayload.Timestamp <= request.RequestDto.DateTo && x.Status == false && x.ProcessId != null && x.ProcessId != string.Empty &&
        ((x.RequestType == nameof(InboundTransactionRequestType.Inventory) && validCustomers.InventoryCustomer.Contains(x.RequestPayload.Plant))
        ||
        (x.RequestType == nameof(InboundTransactionRequestType.GoodsIssued) && validCustomers.GoodsIssuedCustomer.Contains(x.RequestPayload.VendorId)
        )
        ||
        (x.RequestType == nameof(InboundTransactionRequestType.GoodsReceived) && validCustomers.GoodsReceivedCustomer.Contains(x.RequestPayload.VendorId))
        ||
        (x.RequestType == nameof(InboundTransactionRequestType.ReturnReceived) && validCustomers.ReturnReceivedCustomer.Contains(x.RequestPayload.VendorId)
        )
        ),
         x => new
         {
             RequestType = x.RequestType,
             EventId = x.Id,
             VendorId = x.RequestPayload.VendorId,
             Plant = x.RequestPayload.Plant,
         });

        var inboundEvent = result.Select(x => new RetriggerEventDetail
        {
            RequestType = x.RequestType,
            EventId = x.EventId,
            Customer = x.RequestType == nameof(InboundTransactionRequestType.Inventory) ? x.Plant : x.VendorId
        });

        return string.IsNullOrEmpty(request.RequestDto.DocumentType) ?
                                 inboundEvent :
                                 inboundEvent.Where(X => X.RequestType == request.RequestDto.DocumentType);
    }

    private static int SetDocumentId(RetriggerDocumentsListDto documentsToRetrigger, List<RetriggerEventDetail> eventDetails)
    {
        documentsToRetrigger.RetriggerEventDetails = eventDetails;

        return documentsToRetrigger.RetriggerEventDetails.Count();
    }

    private async Task<List<RetriggerEventDetail>> GetInboundOutboundEventByEventIdAsync(string[] eventIds, string? requestType = null)
    {
        List<RetriggerEventDetail> requestPayloads = new List<RetriggerEventDetail>();

        if (string.IsNullOrEmpty(requestType))
        {
            var processedIds = await GetSapTransactionsAsync(eventIds);
            requestPayloads.AddRange(processedIds);

            var remainingEventIds = GetRemainingEventIds(eventIds, processedIds);

            if (remainingEventIds.Any())
            {
                var remainingPayloads = await GetOutboundsAsync(remainingEventIds, requestType);
                requestPayloads.AddRange(remainingPayloads);
            }
        }
        else if (requestType == OutboundTransactionRequestType.ProductMaster || requestType == OutboundTransactionRequestType.ReturnOrder
             || requestType == OutboundTransactionRequestType.PurchaseOrder || requestType == OutboundTransactionRequestType.CreateOrder)
        {
            var transactions = await GetOutboundsAsync(eventIds, requestType);
            requestPayloads.AddRange(transactions);
        }
        else
        {
            var transactions = await GetSapTransactionsAsync(eventIds);
            requestPayloads.AddRange(transactions);
        }

        return requestPayloads;
    }

    private async Task<IEnumerable<RetriggerEventDetail>> GetSapTransactionsAsync(string[] eventIds)
    {
        var validCustomers = GetValidCustomers();

        var excludeGoodsIssuedOrder = GetExculdeInboundOrderForDocumentType(nameof(InboundTransactionRequestType.GoodsIssued));
        var excludeReturnReceivedOrder = GetExculdeInboundOrderForDocumentType(nameof(InboundTransactionRequestType.ReturnReceived));

        var result = await _sapTransactionsQueryService.GetItemsAsync(
          spl => eventIds.Contains(spl.Id) && spl.Status == false && spl.ProcessId != null && spl.ProcessId != string.Empty &&  spl.Provider == _customerPlant.ApplicationProvider &&
           ((spl.RequestType == nameof(InboundTransactionRequestType.Inventory) && validCustomers.InventoryCustomer.Contains(spl.RequestPayload.Plant))
           ||
           (spl.RequestType == nameof(InboundTransactionRequestType.GoodsIssued) && validCustomers.GoodsIssuedCustomer.Contains(spl.RequestPayload.VendorId)
           )
            ||
           (spl.RequestType == nameof(InboundTransactionRequestType.GoodsReceived) && validCustomers.GoodsReceivedCustomer.Contains(spl.RequestPayload.VendorId))
           ||
           (spl.RequestType == nameof(InboundTransactionRequestType.ReturnReceived) && validCustomers.ReturnReceivedCustomer.Contains(spl.RequestPayload.VendorId)
            )),
          x => new
          {
              RequestType = x.RequestType,
              EventId = x.Id,
              VendorId = x.RequestPayload.VendorId,
              Plant = x.RequestPayload.Plant,
          });

        return result.Select(x => new RetriggerEventDetail
        {
            RequestType = x.RequestType,
            EventId = x.EventId,
            Customer = x.RequestType == nameof(InboundTransactionRequestType.Inventory) ? x.Plant : x.VendorId
        });
    }

    private ValidCustomerLists GetValidCustomers()
    {
        return new ValidCustomerLists
        {
            InventoryCustomer = GetCustomerForDocumentType(nameof(InboundTransactionRequestType.Inventory)),
            GoodsIssuedCustomer = GetCustomerForDocumentType(nameof(InboundTransactionRequestType.GoodsIssued)),
            GoodsReceivedCustomer = GetCustomerForDocumentType(nameof(InboundTransactionRequestType.GoodsReceived)),
            ReturnReceivedCustomer = GetCustomerForDocumentType(nameof(InboundTransactionRequestType.ReturnReceived))
        };
    }

    private ValidCustomerLists GetValidOutboundCustomers()
    {
        return new ValidCustomerLists
        {
            PurchaseOrderCustomer = GetCustomerForDocumentType(nameof(OutboundTransactionRequestType.PurchaseOrder)),
            ProductMasterCustomer = GetCustomerForDocumentType(nameof(OutboundTransactionRequestType.ProductMaster)),
            ReturnOrderCustomer = GetCustomerForDocumentType(nameof(OutboundTransactionRequestType.ReturnOrder)),
            CreateOrderCustomer = GetCustomerForDocumentType(nameof(OutboundTransactionRequestType.CreateOrder))
        };
    }

    private async Task<IEnumerable<RetriggerEventDetail>> GetOutboundsAsync(string[] remainingEventIds, string requestType)
    {
        var validCustomers = GetValidOutboundCustomers();
        return await _outboundDocumentQueryService.GetItemsAsync(spl => remainingEventIds.Contains(spl.Id)
        && spl.Status == TransactionStatus.Failed
        && spl.Provider == _customerPlant.ApplicationProvider
        && (
            (spl.DocumentType == nameof(OutboundTransactionRequestType.PurchaseOrder)
                && validCustomers.PurchaseOrderCustomer.Contains(spl.Customer))
            ||
            (spl.DocumentType == nameof(OutboundTransactionRequestType.CreateOrder)
                && validCustomers.CreateOrderCustomer.Contains(spl.Customer))
            ||
            (spl.DocumentType == OutboundTransactionRequestType.ReturnOrder
                && validCustomers.ReturnOrderCustomer.Contains(spl.Customer))
            ||
            (spl.DocumentType == nameof(OutboundTransactionRequestType.ProductMaster)
                && validCustomers.ProductMasterCustomer.Contains(spl.Customer))
        ),
        x => new RetriggerEventDetail { RequestType = x.DocumentType, EventId = x.Id, Customer = x.Customer }
    );
    }

    private string[] GetRemainingEventIds(string[] eventIds, IEnumerable<RetriggerEventDetail> processedIds)
    {
        return eventIds.Except(processedIds.Select(x => x.EventId)).ToArray();
    }

    public List<string> GetCustomerForDocumentType(string requestType) => requestType switch
    {
        nameof(InboundTransactionRequestType.GoodsReceived) => _inboundCustomerOptions.GoodsReceived,
        nameof(InboundTransactionRequestType.GoodsIssued) => _inboundCustomerOptions.GoodsIssued,
        nameof(InboundTransactionRequestType.ReturnReceived) => _inboundCustomerOptions.ReturnReceived,
        nameof(InboundTransactionRequestType.Inventory) => _inboundCustomerOptions.Inventory,
        OutboundTransactionRequestType.ProductMaster => _outboundCustomerOptions.ProductMaster,
        OutboundTransactionRequestType.PurchaseOrder => _outboundCustomerOptions.PurchaseOrder,
        OutboundTransactionRequestType.CreateOrder => _outboundCustomerOptions.CreateOrder,
        OutboundTransactionRequestType.ReturnOrder => _outboundCustomerOptions.ReturnOrder,
        _ => new List<string>()
    };

    private ExculdePlantOrder? GetExculdeInboundOrderForDocumentType(string requestType)
    {
        return _exculdePlantOrder.ExculdeInboundPlantOrders?.Find(x => x.OrderType == requestType);
    }
}