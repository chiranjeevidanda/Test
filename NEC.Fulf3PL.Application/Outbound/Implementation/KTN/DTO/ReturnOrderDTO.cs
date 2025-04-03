using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ReturnOrderDTO
    {
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

        [JsonProperty("datePlanned")]
        public string DatePlanned { get; set; }

        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        [JsonProperty("buyer")]
        public string Buyer { get; set; }

        [JsonProperty("addresses")]
        public List<OutboundNotificationAddressDTO> Addresses { get; set; }

        [JsonProperty("returnLines")]
        public List<ReturnLineItemDetailsDTO> Lines { get; set; }
    }
}
