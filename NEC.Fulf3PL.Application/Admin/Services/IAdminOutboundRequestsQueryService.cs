using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Common;
using NEC.Fulf3PL.Application.Queries.Pagination;
using NEC.Fulf3PL.Core.Entities.Admin;

namespace NEC.Fulf3PL.Application.Admin.Services;

public interface IAdminOutboundRequestsQueryService : IQueryService<OutboundRequests>
{
    public Task<PaginationResponseModel<OutboundResponseDto>> ListDocuments(OutboundTransactionsListSearchFilterDto filterDto, DateTime defaultStartDate);
}