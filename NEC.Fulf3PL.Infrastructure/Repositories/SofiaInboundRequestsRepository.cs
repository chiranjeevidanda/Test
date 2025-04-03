using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class SofiaInboundRequestsRepository : ISofiaInboundRequestsRepository
    {
        private readonly Container container;
        public SofiaInboundRequestsRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<SofiaInboundRequestsLogModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<SofiaInboundRequestsLogModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<SofiaInboundRequestsLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<SofiaInboundRequestsLogModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<SofiaInboundRequestsLogModel>(new QueryDefinition(inputQuery));
            var results = new List<SofiaInboundRequestsLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<SofiaInboundRequestsLogModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<SofiaInboundRequestsLogModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<SofiaInboundRequestsLogModel> CreateAsync(SofiaInboundRequestsLogModel sofiaInboundRequestsLogModel)
        {
            var response = await container.CreateItemAsync(sofiaInboundRequestsLogModel, new PartitionKey(sofiaInboundRequestsLogModel.Id));
            return response.Resource;
        }

        public async Task<SofiaInboundRequestsLogModel> UpdateAsync(string id, SofiaInboundRequestsLogModel sofiaInboundRequestsLogModel)
        {
            var response = await container.UpsertItemAsync(sofiaInboundRequestsLogModel, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<SofiaOutboundRequestsLogModel>(id, new PartitionKey(id));
        }
    }
}
