using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Common;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO;
using NEC.Fulf3PL.Application.Queries.Pagination;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.Entities.Admin;

namespace NEC.Fulf3PL.Application.Admin.Services;

public interface IItemMasterQueryService : IQueryService<ItemMaster>
{
    public Task<PaginationResponseModel<ItemMasterDto>> ListDocuments(ItemMasterListSearchFilterDto filterDto);

    public Task<ExecuteResult<OutboundConverterModel>> MapItemMasterData(string productCode, string plant, string provider);
}