using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class InboundRequestRepository : IInboundRequestsRepository
    {
        private readonly Container container;
        public InboundRequestRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<InboundRequestsLogModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<InboundRequestsLogModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<InboundRequestsLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<InboundRequestsLogModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<InboundRequestsLogModel>(new QueryDefinition(inputQuery));
            var results = new List<InboundRequestsLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<InboundRequestsLogModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<InboundRequestsLogModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<InboundRequestsLogModel> CreateAsync(InboundRequestsLogModel inboundRequestsLogModel)
        {
            var response = await container.CreateItemAsync(inboundRequestsLogModel, new PartitionKey(inboundRequestsLogModel.Id));
            return response.Resource;
        }

        public async Task<InboundRequestsLogModel> UpdateAsync(string id, InboundRequestsLogModel inboundRequestsLogModel)
        {
            var response = await container.UpsertItemAsync(inboundRequestsLogModel, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<InboundRequestsLogModel>(id, new PartitionKey(id));
        }
    }
}
