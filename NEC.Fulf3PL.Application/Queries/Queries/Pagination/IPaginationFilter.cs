namespace NEC.Fulf3PL.Application.Queries.Pagination;

public interface IPaginationFilter
{
    public int Skip { get; }

    public int Take { get; }

    public string? SortBy { get; }

    public string? SortDir { get; }
}

public static class PaginationFilterExtensions
{
    public static bool IsAscending(this IPaginationFilter paginationFilter)
    {
        var sortDir = paginationFilter.SortDir?.ToLower();
        return sortDir == "asc" || sortDir == "reset";
    }
}
