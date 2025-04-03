using System.Linq.Expressions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Extensions;

public static class CosmosContainerExtensions
{
    public static async Task<IEnumerable<T>> GetItemsAsync<T>(this Container container, Expression<Func<T, bool>> predicate)
    {
        var query = container.GetItemLinqQueryable<T>().Where(predicate);
        return await query.ToListAsync();
    }

    public static async Task<IEnumerable<TResult>> GetItemsAsync<T, TResult>(this Container container, Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> projection)
    {
        var query = container.GetItemLinqQueryable<T>()
            .Where(predicate)
            .Select(projection);
        return await query.ToListAsync();
    }

    public static async Task<TResult?> GetItemAsync<T, TResult>(this Container container, Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> projection)
    {
        var query = container.GetItemLinqQueryable<T>()
            .Where(predicate)
            .Select(projection)
            .Take(1);

        var iterator = query.ToFeedIterator();
        var resp = await iterator.ReadNextAsync();
        return resp.FirstOrDefault();
    }

    public static async Task<IEnumerable<TResult>> GetItemsAsync<TResult>(this Container container, QueryDefinition queryDefinition)
    {
        var iterator = container.GetItemQueryIterator<TResult>(queryDefinition);
        return await iterator.ToListAsync();
    }
}
