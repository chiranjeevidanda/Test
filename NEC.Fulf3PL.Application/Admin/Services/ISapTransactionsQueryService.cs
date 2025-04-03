using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Common;
using NEC.Fulf3PL.Application.Queries.Pagination;
using NEC.Fulf3PL.Core.Entities.Admin;

namespace NEC.Fulf3PL.Application.Admin.Services;

public interface ISapTransactionsQueryService : IQueryService<SapTransactionLog>
{
    public Task<PaginationResponseModel<InboundTransactionsDto>> ListDocuments(InboundTransactionsListSearchFilterDto filterDto, DateTime defaultStartDate);
}