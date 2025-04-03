using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Infrastructure.Repositories
{
    public class SubscriptionExtensionLogRepository : ISubscriptionExtensionLogRepository
    {
        private readonly Container container;
        public SubscriptionExtensionLogRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<SubscriptionExtensionLog>> GetAsync()
        {
            var query = container.GetItemQueryIterator<SubscriptionExtensionLog>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<SubscriptionExtensionLog>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<SubscriptionExtensionLog>> GetByQueryAsync(string inputQuery)
        {
            var query = container.GetItemQueryIterator<SubscriptionExtensionLog>(new QueryDefinition(inputQuery));
            var results = new List<SubscriptionExtensionLog>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task<SubscriptionExtensionLog> GetAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<SubscriptionExtensionLog>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<SubscriptionExtensionLog> CreateAsync(SubscriptionExtensionLog article)
        {
            var response = await container.CreateItemAsync(article, new PartitionKey(article.Id));
            return response.Resource;
        }

        public async Task<SubscriptionExtensionLog> UpdateAsync(string id, SubscriptionExtensionLog article)
        {
            var response = await container.UpsertItemAsync(article, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await container.DeleteItemAsync<SubscriptionExtensionLog>(id, new PartitionKey(id));
        }
    }
}
