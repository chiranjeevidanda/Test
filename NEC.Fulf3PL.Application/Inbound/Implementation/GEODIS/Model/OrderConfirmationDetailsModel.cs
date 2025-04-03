using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.GODISE.Model
{
    public class OrderConfirmationDetailsModel
    {

        [JsonProperty("eventId")]
        public string EventId { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("orderDate")]
        public string OrderDate { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }

        [JsonProperty("orderStatus")]
        public string OrderStatus { get; set; }

        [JsonProperty("orderStatusDescription")]
        public string OrderStatusDescription { get; set; }

        [JsonProperty("lines")]
        public List<OrderConfirmationItemDetailsModel> Lines { get; set; }

        [JsonProperty("additionalAttributes")]
        public List<AdditionalAttributes> AdditionalAttributes { get; set; }

    }


    public class OrderConfirmationItemDetailsModel
    {
        [JsonProperty("lineStatus")]
        public string LineStatus { get; set; }

        [JsonProperty("statusDescription")]
        public string StatusDescription { get; set; }

        [JsonProperty("upcCode")]
        public string UPCCode { get; set; }

        [JsonProperty("customerLineNumber")]
        public string CustomerLineNumber { get; set; }

        [JsonProperty("quantityOrdered")]
        public int QuantityOrdered { get; set; }

        [JsonProperty("quantityConfirmed")]
        public int QuantityConfirmed { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("additionalAttributes")]
        public List<AdditionalAttributes> AdditionalAttributes { get; set; }
    }

}
