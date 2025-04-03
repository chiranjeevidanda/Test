
using Azure.Messaging.ServiceBus;
using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Core
{
    public interface IInboundMessageSender
    {
        Task<ExecuteResult<bool>> SendInboundReprocessAsync(string message, string sessionId, string subscriptioName);

        Task<ExecuteResult<bool>> SendQueueMessageAsync(string message, string sessionId, string inboundqueue);

        Task<IEnumerable<ServiceBusReceivedMessage>> ReceiveInboundReprocessAsync();

    }
}
