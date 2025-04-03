using Azure.Core;
using MediatR;
using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Core.Entities.Admin;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options;
using System.Linq.Expressions;

namespace NEC.Fulf3PL.Application.Admin.Command;

public record RetriggerSkuCommandCommand(RetriggerRequestDto RequestDto) : IRequest<SKUCreateResponseDTO>;

public class RetriggerSkuCommandCommandHandler : IRequestHandler<RetriggerSkuCommandCommand, SKUCreateResponseDTO>
{
    private readonly IRetriggerDocumentService _documentService;

    private readonly IAdminOutboundRequestsQueryService _outboundDocumentQueryService;
    private readonly IItemMasterQueryService _itemMasterQueryService;
    private readonly CustomerPlantOptions _customerPlant;

    public RetriggerSkuCommandCommandHandler(IRetriggerDocumentService documentService,
    IAdminOutboundRequestsQueryService outboundDocumentQueryService, IItemMasterQueryService itemMasterQueryService, IOptions<CustomerPlantOptions> customerPlant,
    IInboundServiceBusQueueService inboundServiceBusQueueService)
    {
        _documentService = documentService;
        _outboundDocumentQueryService = outboundDocumentQueryService;
        _itemMasterQueryService = itemMasterQueryService;
        _customerPlant = customerPlant.Value;
    }
    public async Task<SKUCreateResponseDTO> Handle(RetriggerSkuCommandCommand request, CancellationToken cancellationToken)
    {
        var totalDocs = 0;
        var sendForProcess = new List<string>();
        var skuProcessed = new List<string>();
        var plant = GetCustomerPlant(request.RequestDto.Customer);
        if (!string.IsNullOrEmpty(request.RequestDto.DocumentId) && !string.IsNullOrEmpty(request.RequestDto.Customer))
        {
            string[] orderNumbers = request.RequestDto.DocumentId.Replace(" ", ",").Split(",", StringSplitOptions.RemoveEmptyEntries);
            var items = await _outboundDocumentQueryService.GetItemsAsync(x => orderNumbers.Contains(x.DocumentId) && x.DocumentType != null && x.Provider == _customerPlant.ApplicationProvider && x.Customer == request.RequestDto.Customer,
                                       x => new RetriggerEventDetail { EventId = x.DocumentId, RequestType = x.DocumentType });

            skuProcessed = items.Select(x => x.EventId).Distinct().ToList();

            var toBeProcess = orderNumbers.Except(skuProcessed).ToList();

            if (toBeProcess?.Count() == 0 && items?.Count() > 0)
            {
                return new SKUCreateResponseDTO()
                {
                    SuccessCount = 0,
                    ActiveMessageCount = 0,
                    SentForProcess = new List<string>(),
                    SkuProcessed = skuProcessed
                };
            }

            if (toBeProcess.Any())
            {
                var itemMaster = await _itemMasterQueryService.GetItemsAsync(item => toBeProcess.Contains(item.Data.ProductCode) && item.Provider == _customerPlant.ApplicationProvider && item.Data.Plant == plant.Plant.ToString(),
                 item => new ItemMaster { ModifiedDate = item.ModifiedDate, Data = new Core.DTO.ProductMasterRequestDTO() { ProductCode = item.Data.ProductCode } });

                var latestItems = itemMaster
                                  .GroupBy(item => item.Data.ProductCode)
                                  .Select(group => new RetriggerEventDetail
                                  {
                                      EventId = group.Key,
                                      Customer = plant.Plant.ToString(),
                                  }).ToList();


                var filterdItem = latestItems.ToList();
                sendForProcess = filterdItem.Select(X => X.EventId).Distinct().ToList();

                if (filterdItem.Count > 0)
                {
                    await _documentService.PostCreateSku(filterdItem);
                }
            }
        }


        long requestActiveCount = 0;
        if (sendForProcess.Count > 0)
        {
            requestActiveCount = await GetActiveTransactionCount();
        }

        return new SKUCreateResponseDTO()
        {
            SuccessCount = sendForProcess.Count(),
            ActiveMessageCount = requestActiveCount,
            SentForProcess = sendForProcess,
            SkuProcessed = skuProcessed
        };
    }

    private async Task<long> GetActiveTransactionCount()
    {
        var outboundDocument = await _outboundDocumentQueryService.CountAsync(x =>
            x.DocumentType == OutboundTransactionRequestType.ProductMaster &&
            x.Status == TransactionStatus.Inprogress);
        return outboundDocument;
    }
    private CustomerPlantDetails? GetCustomerPlant(string customer)
    {
        return _customerPlant.CustomersPlant?.Find(x => x.Name == customer);
    }
}
