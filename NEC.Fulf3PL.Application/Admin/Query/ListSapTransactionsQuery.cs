using MediatR;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Queries.Pagination;

namespace NEC.Fulf3PL.Application.Admin.Query;

public record ListSapTransactionsQuery(InboundTransactionsListSearchFilterDto FilterDto, DateTime DefaultStartDate)
    : IRequest<PaginationResponseModel<InboundTransactionsDto>>;

public class ListSapTransactionsQueryHandler
    : IRequestHandler<ListSapTransactionsQuery, PaginationResponseModel<InboundTransactionsDto>>
{
    private readonly ISapTransactionsQueryService _queryService;

    public ListSapTransactionsQueryHandler(ISapTransactionsQueryService queryService)
    {
        _queryService = queryService;
    }   

    public Task<PaginationResponseModel<InboundTransactionsDto>> Handle(ListSapTransactionsQuery request, CancellationToken cancellationToken)
    {
        return _queryService.ListDocuments(request.FilterDto, request.DefaultStartDate);
    }
}