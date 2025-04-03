
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;

namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Queries
{
    public class InboundSubscriberQueryService : IInboundSubscriberQueryService
    {
        private readonly IInboundSubscriberRepository _subscriberRepository;

        public InboundSubscriberQueryService(IInboundSubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }

        public async Task<InboundSubscriberLogModel?> GetSubscriberLogByRequestIdAsync(string requestId, string requestType)
        {
            var inputQuery = string.Empty;
            if (requestType == InboundTransactionRequestType.Inventory)
            {
                inputQuery = $"SELECT * FROM c WHERE c.requestPayload.InventoryAdjustmentRequest.EventID='{requestId}' and c.requestType = 'Inventory' ORDER BY c._ts DESC";
            }
            else
            {
                inputQuery = $"SELECT * FROM c WHERE c.requestPayload.systemId='{requestId}' ORDER BY c._ts DESC";
            }

            var inbound = await _subscriberRepository.GetByQueryAsync(inputQuery);

            return inbound?.FirstOrDefault();
        }
    }
}
