using System.Data;
using System.Linq.Expressions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using NEC.Fulf3PL.Core.Entities.Persistence;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Extensions;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Options;

namespace NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB;

public class GenericQueryService<TEntity> where TEntity : BaseEntity
{
    protected readonly Container _container;

    protected GenericQueryService(CosmosClient cosmosClient, CosmosDbConfigOptions cosmosDbConfig, ContainerOptions containerOptions)
    {
        _container = cosmosClient.GetContainer(cosmosDbConfig.DatabaseId, containerOptions.ContainerId);
    }

    public async Task<IEnumerable<TEntity>> GetItemsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var response = await _container.GetItemsAsync(predicate);
        return response;
    }

    public async Task<IEnumerable<TResult>> GetItemsAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> projection)
    {
        var response = await _container.GetItemsAsync(predicate, projection);
        return response;
    }

    public async Task<TResult?> GetItemAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> projection)
    {
        var response = await _container.GetItemAsync(predicate, projection);
        return response;
    }

    public async Task<TEntity> GetItemAsync(string id)
    {
        var response = await _container.ReadItemAsync<TEntity>(id, new PartitionKey(id));
        return response;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _container.GetItemLinqQueryable<TEntity>()
            .Where(predicate)
            .Select(x => x.Id)
            .Take(1);

        var iterator = query.ToFeedIterator();
        var response = await iterator.ReadNextAsync();
        return response.Count != 0;
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _container.GetItemLinqQueryable<TEntity>()
            .Where(predicate)
            .CountAsync();
    }

    public async Task<int> CountAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> projection)
    {
        return await _container.GetItemLinqQueryable<TEntity>()
            .Where(predicate)
            .Select(projection)
            .CountAsync();
    }
}
