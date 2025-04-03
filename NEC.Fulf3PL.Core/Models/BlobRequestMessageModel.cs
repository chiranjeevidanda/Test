using Newtonsoft.Json;

namespace NEC.Fulfillment.Application.Models
{
    public class BlobRequestMessageModel
    {
        [JsonProperty("blobId")]
        public dynamic BlobId { get; set; }

        [JsonProperty("endpointType")]
        public string InboundEndpoint { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("requestType", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestType { get; set; }
    }
}
