using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class OutboundSubscriberRepository : IOutboundSubscriberRepository
    {
        private readonly Container container;
        public OutboundSubscriberRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<OutboundSubscriberLogModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<OutboundSubscriberLogModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<OutboundSubscriberLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<OutboundSubscriberLogModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<OutboundSubscriberLogModel>(new QueryDefinition(inputQuery));
            var results = new List<OutboundSubscriberLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<OutboundSubscriberLogModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<OutboundSubscriberLogModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<OutboundSubscriberLogModel> CreateAsync(OutboundSubscriberLogModel outboundSubscriber)
        {
            var response = await container.CreateItemAsync(outboundSubscriber, new PartitionKey(outboundSubscriber.Id));
            return response.Resource;
        }

        public async Task<OutboundSubscriberLogModel> UpdateAsync(string id, OutboundSubscriberLogModel outboundSubscriber)
        {
            var response = await container.UpsertItemAsync(outboundSubscriber, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<OutboundSubscriberLogModel>(id, new PartitionKey(id));
        }
    }
}
