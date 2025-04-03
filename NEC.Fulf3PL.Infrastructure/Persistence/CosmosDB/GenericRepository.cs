using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities.Persistence;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Options;

namespace NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB;

public abstract class GenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly Container _container;

    protected GenericRepository(CosmosClient cosmosClient, CosmosDbConfigOptions cosmosDbConfig, ContainerOptions containerOptions)
    {
        _container = cosmosClient.GetContainer(cosmosDbConfig.DatabaseId, containerOptions.ContainerId);
    }

    public async Task<TEntity> GetAsync(string id)
    {
        var response = await _container.ReadItemAsync<TEntity>(id, new PartitionKey(id));
        return response;
    }

    public async Task<TEntity> CreateAsync(TEntity model)
    {
        if (model is IAuditableEntity auditableEntity)
        {
            var timeNow = DateTime.UtcNow;
            auditableEntity.LoggedOn = timeNow;
            auditableEntity.ModifiedDate = timeNow;
        }

        var response = await _container.CreateItemAsync(model, new PartitionKey(model.Id));
        return response;
    }

    public async Task<TEntity> UpdateAsync(TEntity model)
    {
        if (model is IAuditableEntity auditableEntity)
        {
            auditableEntity.ModifiedDate = DateTime.UtcNow;
        }

        var response = await _container.UpsertItemAsync(model, new PartitionKey(model.Id));
        return response;
    }

    public async Task DeleteAsync(string id)
    {
        await _container.DeleteItemAsync<TEntity>(id, new PartitionKey(id));
    }

    public async Task DeleteAsync(TEntity model)
    {
        await DeleteAsync(model.Id);
    }
}
