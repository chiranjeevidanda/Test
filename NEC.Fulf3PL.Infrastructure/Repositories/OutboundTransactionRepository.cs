using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class OutboundTransactionRepository : IOutboundTransactionRepository
    {
        private readonly Container container;
        public OutboundTransactionRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<OutboundTransactionLogModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<OutboundTransactionLogModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<OutboundTransactionLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<OutboundTransactionLogModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<OutboundTransactionLogModel>(new QueryDefinition(inputQuery));
            var results = new List<OutboundTransactionLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<OutboundTransactionLogModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<OutboundTransactionLogModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<OutboundTransactionLogModel> CreateAsync(OutboundTransactionLogModel outboundTransaction)
        {
            var response = await container.CreateItemAsync(outboundTransaction, new PartitionKey(outboundTransaction.Id));
            return response.Resource;
        }

        public async Task<OutboundTransactionLogModel> UpdateAsync(string id, OutboundTransactionLogModel outboundTransaction)
        {
            var response = await container.UpsertItemAsync(outboundTransaction, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<OutboundTransactionLogModel>(id, new PartitionKey(id));
        }
    }
}
