using Newtonsoft.Json;

namespace NEC.Fulfillment.Application.Models
{
    public class InboundMessageModel
    {
        [JsonProperty("requestData")]
        public dynamic RequestData { get; set; }

        public string InboundEndpoint { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("requestType", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestType { get; set; }
    }
}
