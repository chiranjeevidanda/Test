using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class InboundTransactionRepository : IInboundTransactionRepository
    {
        private readonly Container container;
        public InboundTransactionRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<InboundTransactionLogModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<InboundTransactionLogModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<InboundTransactionLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<InboundTransactionLogModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<InboundTransactionLogModel>(new QueryDefinition(inputQuery));
            var results = new List<InboundTransactionLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<InboundTransactionLogModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<InboundTransactionLogModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<InboundTransactionLogModel> CreateAsync(InboundTransactionLogModel inboundTransaction)
        {
            var response = await container.CreateItemAsync(inboundTransaction, new PartitionKey(inboundTransaction.Id));
            return response.Resource;
        }

        public async Task<InboundTransactionLogModel> UpdateAsync(string id, InboundTransactionLogModel inboundTransaction)
        {
            var response = await container.UpsertItemAsync(inboundTransaction, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<InboundTransactionLogModel>(id, new PartitionKey(id));
        }
    }
}
