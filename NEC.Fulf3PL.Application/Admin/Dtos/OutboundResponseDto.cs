using NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Helpers;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities.Admin
{
    public class OutboundResponseDto
    {
        [JsonProperty("eventId")]
        public string? EventId { get; set; }

        [JsonProperty("provider")]
        public string? Provider { get; set; }

        [JsonProperty("customer")]
        public string? Customer { get; set; }

        [JsonProperty("processId")]
        public string? ProcessId { get; set; }

        [JsonProperty("documentId")]
        public string? DocumentId { get; set; }

        [JsonProperty("documentType")]
        public string? DocumentType { get; set; }

        [JsonProperty("requestInput")]
        public DeliveryDataModel? RequestInput { get; set; }

        [JsonProperty("requestData")]
        public string? RequestData { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("errorMessage")]
        public string? ErrorMessage { get; set; }

        [JsonProperty("modifiedDate")]
        public string? ModifiedDate { get; set; }

        [JsonProperty("systemId", NullValueHandling = NullValueHandling.Ignore)]
        public string? SystemId { get; set; }
        
        [JsonProperty("response", NullValueHandling = NullValueHandling.Ignore)]
        public string? Response { get; set; }
       
        [JsonProperty("bookingReference", NullValueHandling = NullValueHandling.Ignore)]
        public string? BookingReference { get; set; }

        [JsonProperty(PropertyName = "containerNumber")]
        public string? ContainerNumber { get; set; }

        [JsonProperty("outboundRequestId", NullValueHandling = NullValueHandling.Ignore)]
        public string? OutboundRequestId { get; set; }

        public bool IgnoreEmailNotification { get; set; } = false;
    }
}
