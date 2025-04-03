using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model
{
    public class ReturnReceivedDetailsModel
    {

        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("assignmentReference")]
        public string AssignmentReference { get; set; }

        [JsonProperty("orderReference")]
        public string OrderReference { get; set; }

        [JsonProperty("deliverytype")]
        public string Deliverytype { get; set; }

        [JsonProperty("customerReference2")]
        public string CustomerReference2 { get; set; }

        [JsonProperty("reasonCode")]
        public string ReasonCode { get; set; }

        [JsonProperty("datePlanned")]
        public string DatePlanned { get; set; }

        [JsonProperty("buyer")]
        public string Buyer { get; set; }

        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }

        [JsonProperty("lastModifiedDateTime")]
        public string LastModifiedDateTime { get; set; }

        [JsonProperty("dateTimeClosed")]
        public string DateTimeClosed { get; set; }

        [JsonProperty("returnLines")]
        public List<ReturnLineItemDetailsModel> Lines { get; set; }

    }
}
