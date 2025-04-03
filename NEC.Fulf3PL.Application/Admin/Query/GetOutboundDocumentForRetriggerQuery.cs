using MediatR;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;

namespace NEC.Fulf3PL.Application.Admin.Query;

public record GetOutboundDocumentForRetriggerQuery(string Id) : IRequest<DocumentForRetriggerDto>;

public class GetOutboundDocumentForRetriggerQueryHandler
    : IRequestHandler<GetOutboundDocumentForRetriggerQuery, DocumentForRetriggerDto>
{
    private readonly IAdminOutboundRequestsQueryService _queryService;

    public GetOutboundDocumentForRetriggerQueryHandler(IAdminOutboundRequestsQueryService queryService)
    {
        _queryService = queryService;
    }

    public async Task<DocumentForRetriggerDto> Handle(GetOutboundDocumentForRetriggerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _queryService.GetItemAsync(x => x.Id == request.Id, x => new DocumentForRetriggerDto
        {
            DocumentType = x.DocumentType,
            RequestData = x.RequestData,
            RequestInput = x.RequestInput,
            DocumentId = x.DocumentId,
            Customer = x.Customer
        });
        return entity!;
    }
}
