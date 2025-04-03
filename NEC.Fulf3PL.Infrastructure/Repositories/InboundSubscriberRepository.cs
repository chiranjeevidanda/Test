using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class InboundSubscriberRepository : IInboundSubscriberRepository
    {
        private readonly Container container;
        public InboundSubscriberRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<InboundSubscriberLogModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<InboundSubscriberLogModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<InboundSubscriberLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<InboundSubscriberLogModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<InboundSubscriberLogModel>(new QueryDefinition(inputQuery));
            var results = new List<InboundSubscriberLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<InboundSubscriberLogModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<InboundSubscriberLogModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<InboundSubscriberLogModel> CreateAsync(InboundSubscriberLogModel inboundSubscriberLogModel)
        {
            var response = await container.CreateItemAsync(inboundSubscriberLogModel, new PartitionKey(inboundSubscriberLogModel.Id));
            return response.Resource;
        }

        public async Task<InboundSubscriberLogModel> UpdateAsync(string id, InboundSubscriberLogModel inboundSubscriberLogModel)
        {
            var response = await container.UpsertItemAsync(inboundSubscriberLogModel, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<InboundSubscriberLogModel>(id, new PartitionKey(id));
        }
    }
}
