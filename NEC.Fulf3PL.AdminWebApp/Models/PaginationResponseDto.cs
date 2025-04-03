using NEC.Fulf3PL.Application.Queries.Pagination;

namespace NEC.Fulf3PL.AdminWebApp.Models;

public class PaginationResponseDto<T>(int skip, int take, string? sortBy, string? sortDir, IEnumerable<T> results, int total)
{
    public PaginationResponseDto(IPaginationFilter paginationFilter, PaginationResponseModel<T> paginationResponse)
        : this(paginationFilter.Skip, paginationFilter.Take, paginationFilter.SortDir, paginationFilter.SortBy, paginationResponse.Results, paginationResponse.Total)
    {

    }

    public PaginationResponseDto(IPaginationFilter paginationFilter, IEnumerable<T> results, int total)
        : this(paginationFilter.Skip, paginationFilter.Take, paginationFilter.SortDir, paginationFilter.SortBy, results, total)
    {

    }

    public int Total { get; } = total;

    public int Skip { get; } = skip;

    public int Take { get; } = take;

    public string? SortBy { get; } = sortBy;

    public string? SortDir { get; } = sortDir;

    public IEnumerable<T> Results { get; } = results;
}
