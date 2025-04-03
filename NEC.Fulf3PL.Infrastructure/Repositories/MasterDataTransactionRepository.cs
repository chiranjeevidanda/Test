using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class MasterDataTransactionRepository : IMasterDataTransactionRepository
    {
        private readonly Container container;
        public MasterDataTransactionRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<MasterDataTransactionLogModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<MasterDataTransactionLogModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<MasterDataTransactionLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<MasterDataTransactionLogModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<MasterDataTransactionLogModel>(new QueryDefinition(inputQuery));
            var results = new List<MasterDataTransactionLogModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<MasterDataTransactionLogModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<MasterDataTransactionLogModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<MasterDataTransactionLogModel> CreateAsync(MasterDataTransactionLogModel masterDataTransaction)
        {
            var response = await container.CreateItemAsync(masterDataTransaction, new PartitionKey(masterDataTransaction.Id));
            return response.Resource;
        }

        public async Task<MasterDataTransactionLogModel> UpdateAsync(string id, MasterDataTransactionLogModel masterDataTransaction)
        {
            var response = await container.UpsertItemAsync(masterDataTransaction, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<MasterDataTransactionLogModel>(id, new PartitionKey(id));
        }
    }
}
