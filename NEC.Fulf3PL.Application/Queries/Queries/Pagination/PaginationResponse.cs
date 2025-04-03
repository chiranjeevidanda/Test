namespace NEC.Fulf3PL.Application.Queries.Pagination;

public record PaginationResponseModel<T>(IEnumerable<T> Results, int Total);
