using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities
{
    public class InboundRequestsLogModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string Provider { get; set; }

        [JsonProperty(PropertyName = "subscriptionId")]
        public string SubscriptionId { get; set; }

        [JsonProperty(PropertyName = "requestType")]
        public string RequestType { get; set; }

        [JsonProperty(PropertyName = "requestOn")]
        public DateTime RequestOn { get; set; }

        [JsonProperty(PropertyName = "requestHeader")]
        public object RequestHeader { get; set; }

        [JsonProperty(PropertyName = "requestPayload")]
        public object RequestPayload { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }
    }
}
