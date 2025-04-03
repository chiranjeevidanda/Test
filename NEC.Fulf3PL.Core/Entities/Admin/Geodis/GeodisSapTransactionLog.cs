using NEC.Fulf3PL.Core.DTO.Admin.Geodis;
using NEC.Fulf3PL.Core.Entities.Persistence;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities.Admin.Geodis
{
    public class GeodisSapTransactionLog : AuditableEntity
    {
        [JsonProperty("eventId")]
        public string? EventId { get; set; }

        [JsonProperty("processId", NullValueHandling = NullValueHandling.Ignore)]
        public string? ProcessId { get; set; }

        [JsonProperty("inboundRequestId")]
        public string? InboundRequestId { get; set; }

        [JsonProperty("provider")]
        public string? Provider { get; set; }

        [JsonProperty("requestType")]
        public string? RequestType { get; set; }

        [JsonProperty("requestPayload")]
        public GeodisRequestPayloadDto? RequestPayload { get; set; }

        [JsonProperty("status")]
        public bool? Status { get; set; }

        [JsonProperty("transactionStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string? TransactionStatus { get; set; }

        [JsonProperty("sapInputRequest")]
        public string? SapInputRequest { get; set; }

        [JsonProperty("sapResponse")]
        public object? SapResponse { get; set; }

        [JsonProperty(PropertyName = "errorMessage")]
        public string? ErrorMessage { get; set; }

        [JsonProperty("webhookPayload")]
        public object? WebhookPayload { get; set; }
        [JsonProperty("customer")]
        public string? Customer { get; set; }
    }
}
