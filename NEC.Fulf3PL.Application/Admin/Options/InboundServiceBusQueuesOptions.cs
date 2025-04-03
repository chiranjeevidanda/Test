namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options
{
    public class InboundServiceBusQueuesOptions
    {
        public const string SectionName = "InboundServiceBusQueues";
        public Dictionary<string, string> Queues { get; set; } = new Dictionary<string, string>();
        public string ServiceBusConnection { get; set; } = string.Empty;
    }
}
