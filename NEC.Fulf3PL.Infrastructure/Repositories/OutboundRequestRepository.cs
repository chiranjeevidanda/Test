using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class OutboundRequestRepository : IOutboundRequestsRepository
    {
        private readonly Container container;
        public OutboundRequestRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<OutboundRequestsLogModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<OutboundRequestsLogModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<OutboundRequestsLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<OutboundRequestsLogModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<OutboundRequestsLogModel>(new QueryDefinition(inputQuery));
            var results = new List<OutboundRequestsLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<OutboundRequestsLogModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<OutboundRequestsLogModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<OutboundRequestsLogModel> CreateAsync(OutboundRequestsLogModel outboundRequestsLogModel)
        {
            var response = await container.CreateItemAsync(outboundRequestsLogModel, new PartitionKey(outboundRequestsLogModel.Id));
            return response.Resource;
        }

        public async Task<OutboundRequestsLogModel> UpdateAsync(string id, OutboundRequestsLogModel outboundRequestsLogModel)
        {
            var response = await container.UpsertItemAsync(outboundRequestsLogModel, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<OutboundRequestsLogModel>(id, new PartitionKey(id));
        }
    }
}
