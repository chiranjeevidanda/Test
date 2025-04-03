using MediatR;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Queries.Pagination;
using NEC.Fulf3PL.Core.Entities.Admin;

namespace NEC.Fulf3PL.Application.Admin.Query;

public record ListOutboundTransactionsQuery(OutboundTransactionsListSearchFilterDto FilterDto, DateTime DefaultStartDate)
    : IRequest<PaginationResponseModel<OutboundResponseDto>>;

public class ListOutboundTransactionsQueryHandler
    : IRequestHandler<ListOutboundTransactionsQuery, PaginationResponseModel<OutboundResponseDto>>
{
    private readonly IAdminOutboundRequestsQueryService _queryService;

    public ListOutboundTransactionsQueryHandler(IAdminOutboundRequestsQueryService queryService)
    {
        _queryService = queryService;
    }   

    public Task<PaginationResponseModel<OutboundResponseDto>> Handle(ListOutboundTransactionsQuery request, CancellationToken cancellationToken)
    {
        return _queryService.ListDocuments(request.FilterDto, request.DefaultStartDate);
    }
}