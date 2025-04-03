using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CreateOrderDTO
    {
        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("destinationReference", NullValueHandling = NullValueHandling.Ignore)]
        public string DestinationReference { get; set; }

        [JsonProperty("assignmentReference", NullValueHandling = NullValueHandling.Ignore)]
        public string AssignmentReference { get; set; }

        [JsonProperty("customerReference3", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerReference3 { get; set; }

        [JsonProperty("customerReference4", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerReference4 { get; set; }

        [JsonProperty("customerReference5", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerReference5 { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("incotermCode", NullValueHandling = NullValueHandling.Ignore)]
        public string IncotermCode { get; set; }

        [JsonProperty("operationType", NullValueHandling = NullValueHandling.Ignore)]
        public string OperationType { get; set; }

        [JsonProperty("deliveryDate", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryDate { get; set; }

        [JsonProperty("bookingReference")]
        public string BookingReference { get; set; }

        [JsonProperty("addresses")]
        public List<OutboundNotificationAddressDTO> Addresses { get; set; }

        [JsonProperty("lines")]
        public List<OrderLineItemDetailsDTO> Lines { get; set; }

        [JsonProperty("vasCodes")]
        public List<VasCodes> VasCodes { get; set; }
    }
}
