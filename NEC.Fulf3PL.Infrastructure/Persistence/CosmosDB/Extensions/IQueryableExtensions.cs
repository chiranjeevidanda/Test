using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Linq.Expressions;

namespace NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Extensions;

public static class IQueryableExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> query)
    {
        var iterator = query.ToFeedIterator();
        return await iterator.ToListAsync();
    }

    public static async Task<List<T>> ToListAsync<T>(this FeedIterator<T> iterator)
    {
        var results = new List<T>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public static IQueryable<T> ApplyFilterExpressions<T>(this IQueryable<T> query, List<Expression<Func<T, bool>>> expressions)
    {
        foreach (var filter in expressions)
        {
            query = query.Where(filter);
        }

        return query;
    }
}
