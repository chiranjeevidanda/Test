using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Core;
using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Infrastructure.ServiceBus
{
    public class InboundMessageSender : IInboundMessageSender
    {
        private readonly string connectionKey;
        private readonly string queueName;
        private readonly ILogger<InboundMessageSender> logger;

        public InboundMessageSender(string connectionKey, string queueName, ILogger<InboundMessageSender> logger)
        {
            this.connectionKey = connectionKey;
            this.queueName = queueName;
            this.logger = logger;
        }

        public async Task<ExecuteResult<bool>> SendInboundReprocessAsync(string message, string sessionId, string subscriptioName)
        {
            var result = new ExecuteResult<bool>();
            try
            {
                await using (ServiceBusClient client = new ServiceBusClient(connectionKey))
                {
                    // create this reprocess queue for line materials
                    ServiceBusSender sender = client.CreateSender(queueName);

                    ServiceBusMessage serializedContents = new ServiceBusMessage(message);
                    serializedContents.SessionId = sessionId;

                    serializedContents.ApplicationProperties.Add("SubscriptionName", subscriptioName);

                    await sender.SendMessageAsync(serializedContents);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorOccured);
                throw;
            }
            return result;
        }

        public async Task<ExecuteResult<bool>> SendQueueMessageAsync(string message, string sessionId, string inboundqueue)
        {
            var result = new ExecuteResult<bool>();
            try
            {
                await using (ServiceBusClient client = new ServiceBusClient(connectionKey))
                {
                    // create this reprocess queue for line materials
                    ServiceBusSender sender = client.CreateSender(inboundqueue);

                    ServiceBusMessage serializedContents = new ServiceBusMessage(message);
                    serializedContents.SessionId = sessionId;

                    serializedContents.ApplicationProperties.Add("SubscriptionName", inboundqueue);

                    await sender.SendMessageAsync(serializedContents);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorOccured);
                throw;
            }
            return result;
        }

        public async Task<IEnumerable<ServiceBusReceivedMessage>> ReceiveInboundReprocessAsync()
        {
            List<ServiceBusReceivedMessage> receivedMessages = new List<ServiceBusReceivedMessage>();
            try
            {
                await using (ServiceBusClient client = new ServiceBusClient(connectionKey))
                {
                    ServiceBusReceiver receiver = client.CreateReceiver(queueName);

                    // Receive messages in a loop until there are no more messages
                    while (true)
                    {
                        // Receive a batch of messages
                        IEnumerable<ServiceBusReceivedMessage> messages = await receiver.ReceiveMessagesAsync(1);

                        // If there are no more messages, break out of the loop
                        if (messages == null || !messages.Any())
                        {
                            break;
                        }

                        // Add received message to the list
                        receivedMessages.AddRange(messages);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorOccured);
                throw;
            }
            return receivedMessages;
        }


    }
}
