using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class MasterDataRepository : IMasterDataRequestRepository
    {
        private readonly Container container;
        public MasterDataRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<MasterDataModel>> GetAsync()
        {
            var query = container.GetItemQueryIterator<MasterDataModel>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<MasterDataModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<MasterDataModel>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<MasterDataModel>(new QueryDefinition(inputQuery));
            var results = new List<MasterDataModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<MasterDataModel> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<MasterDataModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<MasterDataModel> CreateAsync(MasterDataModel masterDataModel)
        {
            var response = await container.CreateItemAsync(masterDataModel, new PartitionKey(masterDataModel.Id));
            return response.Resource;
        }

        public async Task<MasterDataModel> UpdateAsync(string id, MasterDataModel masterDataModel)
        {
            var response = await container.UpsertItemAsync(masterDataModel, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<MasterDataModel>(id, new PartitionKey(id));
        }
    }
}
