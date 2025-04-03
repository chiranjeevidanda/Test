using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class SofiaOutboundRequestsRepository : ISofiaOutboundRequestsRepository
    {
        private readonly Container container;
        public SofiaOutboundRequestsRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<SofiaOutboundRequestsLogModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<SofiaOutboundRequestsLogModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<SofiaOutboundRequestsLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<SofiaOutboundRequestsLogModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<SofiaOutboundRequestsLogModel>(new QueryDefinition(inputQuery));
            var results = new List<SofiaOutboundRequestsLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<SofiaOutboundRequestsLogModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<SofiaOutboundRequestsLogModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<SofiaOutboundRequestsLogModel> CreateAsync(SofiaOutboundRequestsLogModel sofiaOutboundRequestsLogModel)
        {
            var response = await container.CreateItemAsync(sofiaOutboundRequestsLogModel, new PartitionKey(sofiaOutboundRequestsLogModel.Id));
            return response.Resource;
        }

        public async Task<SofiaOutboundRequestsLogModel> UpdateAsync(string id, SofiaOutboundRequestsLogModel sofiaOutboundRequestsLogModel)
        {
            var response = await container.UpsertItemAsync(sofiaOutboundRequestsLogModel, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<SofiaOutboundRequestsLogModel>(id, new PartitionKey(id));
        }
    }
}
