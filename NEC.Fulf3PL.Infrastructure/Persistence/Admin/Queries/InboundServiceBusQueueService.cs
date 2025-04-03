using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options;
using Azure.Messaging.ServiceBus.Administration;

namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Queries
{
    public class InboundServiceBusQueueService : IInboundServiceBusQueueService
    {
        private readonly string _serviceBusConnection;
        private readonly Dictionary<string, string> _queues;
        public InboundServiceBusQueueService(IOptions<InboundServiceBusQueuesOptions> options)
        {
            _serviceBusConnection = options.Value.ServiceBusConnection;
            _queues = options.Value.Queues;
        }

        public async Task<Dictionary<string, long>> GetActiveMessageCountAsync()
        {
            var adminClient = new ServiceBusAdministrationClient(_serviceBusConnection);

            var queueDetailsTasks = await GetAllQueueActiveMessageCountsAsync(adminClient);

            return queueDetailsTasks;
        }

        private async Task<Dictionary<string, long>> GetAllQueueActiveMessageCountsAsync(ServiceBusAdministrationClient adminClient)
        {
            var activeMessageCountsTasks = _queues
                .ToDictionary(
                    pair => pair.Key,
                    pair => GetQueueActiveMessageCountAsync(adminClient, pair.Value)
                );

            var activeMessageCounts = await Task.WhenAll(activeMessageCountsTasks.Values);
            return activeMessageCountsTasks.Keys.Zip(activeMessageCounts, (key, count) => new { key, count })
                                                .ToDictionary(x => x.key, x => x.count);
        }
        private async Task<long> GetQueueActiveMessageCountAsync(ServiceBusAdministrationClient adminClient, string queueName)
        {
            var queueProperties = await adminClient.GetQueueRuntimePropertiesAsync(queueName);
            return queueProperties.Value.ActiveMessageCount;
        }
    }
}
