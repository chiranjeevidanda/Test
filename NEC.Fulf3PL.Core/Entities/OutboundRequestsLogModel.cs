using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities
{
    public class OutboundRequestsLogModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "outboundRequestId")]
        public string OutboundRequestId { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string Provider { get; set; }

        [JsonProperty(PropertyName = "loggedOn")]
        public DateTime LoggedOn { get; set; }

        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }

        [JsonProperty(PropertyName = "nextStage")]
        public bool NextStage { get; set; }
    }
}
