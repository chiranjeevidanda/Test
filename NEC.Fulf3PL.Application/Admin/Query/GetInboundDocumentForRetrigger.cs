using MediatR;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using System.Diagnostics;

namespace NEC.Fulf3PL.Application.Admin.Query;

public record GetInboundDocumentForRetriggerQuery(string Id) : IRequest<DocumentForRetriggerDto>;

public class GetInboundDocumentForRetriggerQueryHandler
    : IRequestHandler<GetInboundDocumentForRetriggerQuery, DocumentForRetriggerDto>
{
    private readonly ISapTransactionsQueryService _queryService;

    public GetInboundDocumentForRetriggerQueryHandler(ISapTransactionsQueryService queryService)
    {
        _queryService = queryService;
    }

    public async Task<DocumentForRetriggerDto> Handle(GetInboundDocumentForRetriggerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _queryService.GetItemAsync(x => x.Id == request.Id && x.ProcessId != null, x => new DocumentForRetriggerDto
        {
            DocumentType = x.RequestType,
            ProcessId = x.ProcessId,
            RequestInput = x.RequestPayload,
            DocumentId = x.RequestPayload.OrderNumber,
            Customer = x.Provider,
            WebhookPayload = x.WebhookPayload            
        });
        return entity;
    }
}
