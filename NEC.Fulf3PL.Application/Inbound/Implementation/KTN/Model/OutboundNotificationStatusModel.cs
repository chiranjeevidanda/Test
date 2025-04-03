using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model
{
    public class OutboundNotificationStatusModel
    {
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("destinationReference")]
        public string DestinationReference { get; set; }

        [JsonProperty("assignmentReference")]
        public string AssignmentReference { get; set; }

        [JsonProperty("customerReference3")]
        public string CustomerReference3 { get; set; }

        [JsonProperty("customerReference4")]
        public string CustomerReference4 { get; set; }

        [JsonProperty("customerReference5")]
        public string CustomerReference5 { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("incotermCode")]
        public string IncotermCode { get; set; }

        [JsonProperty("operationType")]
        public string OperationType { get; set; }

        [JsonProperty("deliveryDate")]
        public string DeliveryDate { get; set; }

        [JsonProperty("dateToLoad")]
        public string DateToLoad { get; set; }

        [JsonProperty("bookingReference")]
        public string BookingReference { get; set; }

        [JsonProperty("lastModifiedDateTime")]
        public string LastModifiedDateTime { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }

        [JsonProperty("addresses")]
        public List<OutboundNotificationAddressDTO> Addresses { get; set; }

        [JsonProperty("lines")]
        public List<OutboundLinesModel> Lines { get; set; }

        [JsonProperty("vasCodes")]
        public List<VasCodesModel> VasCodes { get; set; }
    }

    public class VasCodesModel
    {
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("lineReference")]
        public string LineReference { get; set; }

        [JsonProperty("vasCode")]
        public string VasCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
