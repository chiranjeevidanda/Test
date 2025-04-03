using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.KTN.Models
{
    public class EventBlobRequestMessage
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("data")]
        public BlobData Data { get; set; }
    }

    public class BlobData
    {
        [JsonProperty("api")]
        public string Api { get; set; }

        [JsonProperty("clientRequestId")]
        public string ClientRequestId { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("eTag")]
        public string ETag { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("contentLength")]
        public string ContentLength { get; set; }

        [JsonProperty("blobType")]
        public string BlobType { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("sequencer")]
        public string Sequencer { get; set; }
    }
}
