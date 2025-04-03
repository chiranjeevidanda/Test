using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.KTN.Models
{
    public class OutboundDeliveryMessage
    {
        private Guid? _messageId;
        [JsonProperty("uid")]
        public Guid messageId  // property
        {
            get { return _messageId ?? Guid.NewGuid(); }
            set => _messageId = value;
        }
        public string InboundEndpoint { get; set; }

        [JsonProperty("requestData")]
        public dynamic RequestData { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("requestId")]
        public string ProcessId { get; set; }
    }
}
