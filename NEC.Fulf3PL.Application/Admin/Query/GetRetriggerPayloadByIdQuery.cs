using Azure;
using MediatR;
using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Core.DTO.Admin;
using NEC.Fulf3PL.Core.Entities.Admin;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Admin.Query;

public record GetRetriggerPayloadByIdQuery(string Id) : IRequest<RetriggerPayloadDto?>;

public class GetRetriggerPayloadByIdQueryHandler
    : IRequestHandler<GetRetriggerPayloadByIdQuery, RetriggerPayloadDto?>
{
    private readonly IAdminOutboundRequestsQueryService _outboundQueryService;
    private readonly ISapTransactionsQueryService _inboundQueryService;
    private readonly AdminInboundCustomerOptions _inboundCustomerOptions;
    private readonly AdminOutboundCustomerOptions _outboundCustomerOptions;
    private readonly ExculdePlantOrderOptions _exculdePlantOrder;

    public GetRetriggerPayloadByIdQueryHandler(IAdminOutboundRequestsQueryService outboundQueryService, ISapTransactionsQueryService inboundQueryService,
        IOptions<AdminInboundCustomerOptions> inboundCustomerOptions, IOptions<AdminOutboundCustomerOptions> outboundCustomerOptions, IOptions<ExculdePlantOrderOptions> exculdePlantOrder)
    {
        _outboundQueryService = outboundQueryService;
        _inboundQueryService = inboundQueryService;
        _inboundCustomerOptions = inboundCustomerOptions.Value;
        _outboundCustomerOptions = outboundCustomerOptions.Value;
        _exculdePlantOrder = exculdePlantOrder.Value;
    }

    public async Task<RetriggerPayloadDto?> Handle(GetRetriggerPayloadByIdQuery request, CancellationToken cancellationToken)
    {
        var validProductMasterCustomer = GetOutboundCustomerForDocumentType(OutboundTransactionRequestType.ProductMaster);

        var payload = await _outboundQueryService.GetItemAsync(x => x.Id == request.Id && x.Status == TransactionStatus.Failed && x.RequestInput != null &&
        x.DocumentType == nameof(OutboundTransactionRequestType.ProductMaster) && (validProductMasterCustomer.Count() == 0 ? true : validProductMasterCustomer.Contains(x.Customer)),
        x => new { x.RequestData });

        if (payload != null && payload.RequestData != null)
        {
            return MapPaylaod(payload.RequestData, request.Id);
        }
        else
        {
            var inbpundPayload = await GetInboundRequestPayload(request);

            if (inbpundPayload != null && inbpundPayload.RequestPayload != null)
            {
                return MapPaylaod(inbpundPayload.RequestPayload, request.Id);
            }
        }

        return null;
    }

    private async Task<RetriggerResponsePayload?> GetInboundRequestPayload(GetRetriggerPayloadByIdQuery request)
    {
        var validCustomer = GetValidCustomers();

        var excludeGoodsIssuedOrder = GetExculdeInboundOrderForDocumentType(nameof(InboundTransactionRequestType.GoodsIssued));
        var excludeReturnReceivedOrder = GetExculdeInboundOrderForDocumentType(nameof(InboundTransactionRequestType.ReturnReceived));

        var InventoryGoodsReceivedPayload = await GetInventory_GoodsReceivedPayload(request, validCustomer.InventoryCustomer, validCustomer.ReturnReceivedCustomer);

        if (InventoryGoodsReceivedPayload != null)
        {
            return InventoryGoodsReceivedPayload;
        }

        var GoodsIssued_ReturnReceivedPayload = await GetGoodsIssued_ReturnReceivedPayload(request, validCustomer.GoodsIssuedCustomer, validCustomer.GoodsReceivedCustomer, excludeGoodsIssuedOrder, excludeReturnReceivedOrder);
        return GoodsIssued_ReturnReceivedPayload;
    }

    private async Task<RetriggerResponsePayload?> GetInventory_GoodsReceivedPayload(GetRetriggerPayloadByIdQuery request, List<string> validInventoryCustomer, List<string> validReturnReceivedCustomer)
    {
        var response = await _inboundQueryService.GetItemAsync(x =>
                 x.Id == request.Id &&
                 x.Status == false &&
                 x.ProcessId != null &&
                 x.ProcessId != string.Empty &&
                 (
                 (x.RequestType == nameof(InboundTransactionRequestType.Inventory) && (validInventoryCustomer.Count() == 0 ? true : validInventoryCustomer.Contains(x.RequestPayload.Plant))) ||
                 (x.RequestType == nameof(InboundTransactionRequestType.GoodsReceived) && (validReturnReceivedCustomer.Count() == 0 ? true : validReturnReceivedCustomer.Contains(x.RequestPayload.VendorId)))
                 ),
                 x => new SapTransactionLog
                 {
                     Id = x.Id,
                     ProcessId = x.ProcessId,
                     RequestType = x.RequestType,
                     RequestPayload = x.RequestPayload
                 });

        return response switch
        {
            null => null,
            _ => new RetriggerResponsePayload()
            {
                Id = response.Id,
                ProcessId = response.ProcessId,
                RequestType = response.RequestType,
                RequestPayload = JsonConvert.SerializeObject(response.RequestPayload)
            }
        };
    }

    private async Task<RetriggerResponsePayload?> GetGoodsIssued_ReturnReceivedPayload(GetRetriggerPayloadByIdQuery request, List<string> validGoodsIssuedCustomer, List<string> validGoodsReceivedCustomer, ExculdePlantOrder? excludeGoodsIssuedOrder, ExculdePlantOrder? excludeReturnReceivedOrder)
    {

        var response = await _inboundQueryService.GetItemAsync(x =>
                 x.Id == request.Id &&
                 x.Status == false &&
                 x.ProcessId != null &&
                 x.ProcessId != string.Empty &&
                 (x.RequestType == nameof(InboundTransactionRequestType.GoodsIssued) &&
                 (validGoodsIssuedCustomer.Contains(x.RequestPayload.VendorId) 
                 )
                 ||
                 (x.RequestType == nameof(InboundTransactionRequestType.ReturnReceived) &&
                 (validGoodsReceivedCustomer.Contains(x.RequestPayload.VendorId)) 
                 )),

                 x => new SapTransactionLog
                 {
                     Id = x.Id,
                     ProcessId = x.ProcessId,
                     RequestType = x.RequestType,
                     RequestPayload = x.RequestPayload
                 });

        return response switch
        {
            null => null,
            _ => new RetriggerResponsePayload()
            {
                Id = response.Id,
                ProcessId = response.ProcessId,
                RequestType = response.RequestType,
                RequestPayload = JsonConvert.SerializeObject(response.RequestPayload)
            }
        };
    }

    private static RetriggerPayloadDto MapPaylaod(string payload, string id)
    {
        return new RetriggerPayloadDto()
        {
            RequestPayload = payload,
            Id = id,
        };
    }

    public List<string> GetInboundCustomerForDocumentType(string requestType) => requestType switch
    {
        nameof(InboundTransactionRequestType.GoodsReceived) => _inboundCustomerOptions.GoodsReceived,
        nameof(InboundTransactionRequestType.GoodsIssued) => _inboundCustomerOptions.GoodsIssued,
        nameof(InboundTransactionRequestType.ReturnReceived) => _inboundCustomerOptions.ReturnReceived,
        nameof(InboundTransactionRequestType.Inventory) => _inboundCustomerOptions.Inventory,
        _ => new List<string>()
    };

    private List<string> GetOutboundCustomerForDocumentType(string requestType) => requestType switch
    {
        OutboundTransactionRequestType.ReturnOrder => _outboundCustomerOptions.ReturnOrder,
        OutboundTransactionRequestType.PurchaseOrder => _outboundCustomerOptions.PurchaseOrder,
        OutboundTransactionRequestType.CreateOrder => _outboundCustomerOptions.CreateOrder,
        OutboundTransactionRequestType.ProductMaster => _outboundCustomerOptions.ProductMaster,
        _ => new List<string>()
    };

    private ExculdePlantOrder? GetExculdeInboundOrderForDocumentType(string requestType)
    {
        return _exculdePlantOrder.ExculdeInboundPlantOrders?.Find(x => x.OrderType == requestType);
    }

    private ValidCustomerLists GetValidCustomers()
    {
        return new ValidCustomerLists
        {
            InventoryCustomer = GetInboundCustomerForDocumentType(nameof(InboundTransactionRequestType.Inventory)),
            GoodsIssuedCustomer = GetInboundCustomerForDocumentType(nameof(InboundTransactionRequestType.GoodsIssued)),
            GoodsReceivedCustomer = GetInboundCustomerForDocumentType(nameof(InboundTransactionRequestType.GoodsReceived)),
            ReturnReceivedCustomer = GetInboundCustomerForDocumentType(nameof(InboundTransactionRequestType.ReturnReceived))
        };
    }
}
