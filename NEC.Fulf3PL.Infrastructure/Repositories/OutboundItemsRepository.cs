using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class OutboundItemsRepository : IOutboundItemsRepository
    {
        private readonly Container container;
        public OutboundItemsRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<OutboundItemsLogModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<OutboundItemsLogModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<OutboundItemsLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<OutboundItemsLogModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<OutboundItemsLogModel>(new QueryDefinition(inputQuery));
            var results = new List<OutboundItemsLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<OutboundItemsLogModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<OutboundItemsLogModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<OutboundItemsLogModel> CreateAsync(OutboundItemsLogModel outboundItemsLogModel)
        {
            var response = await container.CreateItemAsync(outboundItemsLogModel, new PartitionKey(outboundItemsLogModel.Id));
            return response.Resource;
        }

        public async Task<OutboundItemsLogModel> UpdateAsync(string id, OutboundItemsLogModel outboundItemsLogModel)
        {
            var response = await container.UpsertItemAsync(outboundItemsLogModel, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<OutboundItemsLogModel>(id, new PartitionKey(id));
        }
    }
}
