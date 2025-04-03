using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model
{
    public class GoodsReceivedDetailsModel
    {

        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("assignmentReference")]
        public string AssignmentReference { get; set; }

        [JsonProperty("supplierOrderReference")]
        public string SupplierOrderReference { get; set; }

        [JsonProperty("deliverytype")]
        public string Deliverytype { get; set; }

        [JsonProperty("customerReference2")]
        public string CustomerReference2 { get; set; }

        [JsonProperty("reasonCode")]
        public string ReasonCode { get; set; }

        [JsonProperty("datePlanned")]
        public string DatePlanned { get; set; }

        [JsonProperty("lastModifiedDateTime")]
        public string LastModifiedDateTime { get; set; }

        [JsonProperty("dateTimeClosed")]
        public string DateTimeClosed { get; set; }

        [JsonProperty("buyer")]
        public string Buyer { get; set; }

        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }

        [JsonProperty("sapReferenceNumber")]
        public string SapReferenceNumber { get; set; }

        [JsonProperty("lines")]
        public List<LineItemDetailsModel> Lines { get; set; }

    }
}
