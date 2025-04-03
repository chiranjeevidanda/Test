using NEC.Fulf3PL.Application.Queries.Pagination;
using NEC.Fulf3PL.Core.Entities.Persistence;

namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class ItemMasterListSearchFilterDto : IPaginationFilter
{
    public string? SkuIds { get; set; }

    public int Skip { get; set; }

    public int Take { get; set; } = 20;

    public string? SortBy { get; set; } = nameof(AuditableEntity.LoggedOn);

    public string? SortDir { get; set; } = "desc";
}
