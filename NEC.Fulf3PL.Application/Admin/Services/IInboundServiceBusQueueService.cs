namespace NEC.Fulf3PL.Application.Admin.Services
{
    public interface IInboundServiceBusQueueService
    {
        public Task<Dictionary<string, long>> GetActiveMessageCountAsync();
    }
}
