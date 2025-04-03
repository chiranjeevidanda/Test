using NEC.Fulf3PL.Core.Entities;
namespace NEC.Fulf3PL.Application.Admin.Services
{
    public interface IInboundSubscriberQueryService
    {
        Task<InboundSubscriberLogModel?> GetSubscriberLogByRequestIdAsync(string requestId, string requestType);
    }
}
