using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class ItemMastertRepository : IItemMasterRequestRepository
    {
        private readonly Container container;
        public ItemMastertRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<ItemMasterModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<ItemMasterModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<ItemMasterModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<ItemMasterModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<ItemMasterModel>(new QueryDefinition(inputQuery));
            var results = new List<ItemMasterModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<ItemMasterModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<ItemMasterModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ItemMasterModel> CreateAsync(ItemMasterModel itemMasterModel)
        {
            var response = await container.CreateItemAsync(itemMasterModel, new PartitionKey(itemMasterModel.Id));
            return response.Resource;
        }

        public async Task<ItemMasterModel> UpdateAsync(string id, ItemMasterModel itemMasterModel)
        {
            var response = await container.UpsertItemAsync(itemMasterModel, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<ItemMasterModel>(id, new PartitionKey(id));
        }
    }
}
