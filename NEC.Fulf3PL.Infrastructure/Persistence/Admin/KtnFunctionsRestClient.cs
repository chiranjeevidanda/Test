using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options;
using System.Net.Http.Json;

namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin;

public class KtnFunctionsRestClient : IRetriggerDocumentService
{
    private readonly HttpClient _httpClient;
    private readonly AdminRetriggerServiceOptions _options;

    public KtnFunctionsRestClient(HttpClient httpClient,
    IOptions<AdminRetriggerServiceOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task PostRetriggerDocuments(RetriggerDocumentsListDto request)
    {
        RetriggerDocumentsListDto postPayload = mapPayload(request);

        postPayload.RetriggerEventDetails = request.RetriggerEventDetails.Where(request => request.RequestType == OutboundTransactionRequestType.ProductMaster || request.RequestType == OutboundTransactionRequestType.PurchaseOrder || request.RequestType == OutboundTransactionRequestType.CreateOrder|| request.RequestType == OutboundTransactionRequestType.ReturnOrder).ToList();

        if (postPayload.RetriggerEventDetails.Any())
        {
            await _httpClient.PostAsJsonAsync(_options.PostOutboundRetriggerDocumentsHttpTriggerUrl, postPayload);
        }

        postPayload.RetriggerEventDetails = request.RetriggerEventDetails.Where(request => request.RequestType == nameof(InboundTransactionRequestType.GoodsReceived) || request.RequestType == nameof(InboundTransactionRequestType.ReturnReceived) ||
                                                                                  request.RequestType == nameof(InboundTransactionRequestType.GoodsIssued) || request.RequestType == nameof(InboundTransactionRequestType.Inventory)).ToList();
        if (postPayload.RetriggerEventDetails.Any())
        {
            await _httpClient.PostAsJsonAsync(_options.PostInboundRetriggerDocumentsHttpTriggerUrl, postPayload);
        }
    }

    private static RetriggerDocumentsListDto mapPayload(RetriggerDocumentsListDto request)
    {
        return new RetriggerDocumentsListDto()
        {
            RetriggerPayload = request.RetriggerPayload,
            RequestType = request.RequestType,
            EventId = request.EventId,
        };
    }

    public async Task PostCreateSku(List<RetriggerEventDetail> request)
    {
        if (request.Any())
        {
            await _httpClient.PostAsJsonAsync(_options.PostCreateSkuHttpTriggerUrl, request);
        }
    }
}
