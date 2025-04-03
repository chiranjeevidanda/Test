using NEC.Fulf3PL.Core.Entities.Persistence;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities.Admin
{
    public class OutboundRequests : AuditableEntity
    {
        [JsonProperty(PropertyName = "provider")]
        public string? Provider { get; set; }

        [JsonProperty(PropertyName = "customer")]
        public string? Customer { get; set; }

        [JsonProperty(PropertyName = "processId", NullValueHandling = NullValueHandling.Ignore)]
        public string? ProcessId { get; set; }

        [JsonProperty(PropertyName = "documentId")]
        public string? DocumentId { get; set; }

        [JsonProperty(PropertyName = "documentType")]
        public string? DocumentType { get; set; }

        [JsonProperty(PropertyName = "requestInput", NullValueHandling = NullValueHandling.Ignore)]
        public object? RequestInput { get; set; }

        [JsonProperty(PropertyName = "requestData")]
        public string? RequestData { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string? Status { get; set; }

        [JsonProperty(PropertyName = "errorMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrorMessage { get; set; } 

        [JsonProperty("systemId", NullValueHandling = NullValueHandling.Ignore)]
        public string? SystemId { get; set; }

        [JsonProperty("outboundRequestId", NullValueHandling = NullValueHandling.Ignore)]
        public string? OutboundRequestId { get; set; }
    }
}
