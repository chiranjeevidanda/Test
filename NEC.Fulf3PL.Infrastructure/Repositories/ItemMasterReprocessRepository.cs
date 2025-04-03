using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class ItemMasterReprocessRepository : IItemMasterReprocessLogRepository
    {
        private readonly Container container;
        public ItemMasterReprocessRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<ItemMasterReprocessLogModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<ItemMasterReprocessLogModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<ItemMasterReprocessLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<ItemMasterReprocessLogModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<ItemMasterReprocessLogModel>(new QueryDefinition(inputQuery));
            var results = new List<ItemMasterReprocessLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<ItemMasterReprocessLogModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<ItemMasterReprocessLogModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<ItemMasterReprocessLogModel> CreateAsync(ItemMasterReprocessLogModel reprocessLogModel)
        {
            var response = await container.CreateItemAsync(reprocessLogModel, new PartitionKey(reprocessLogModel.Id));
            return response.Resource;
        }

        public async Task<ItemMasterReprocessLogModel> UpdateAsync(string id, ItemMasterReprocessLogModel reprocessLogModel)
        {
            var response = await container.UpsertItemAsync(reprocessLogModel, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<ItemMasterReprocessLogModel>(id, new PartitionKey(id));
        }
    }
}
