
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NEC.Fulf3PL.Core.DTO
{
    public class CreateOrderRequestDTO
    {
        [Required(ErrorMessage = "RequestId is required")]
        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [Required(ErrorMessage = "OrderNumber is required")]
        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("providerId")]
        public string ProviderId { get; set; }

        [Required(ErrorMessage = "OrderDate is required")]
        [JsonProperty("orderDate")]
        public string OrderDate { get; set; }

        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("buyerId")]
        public string BuyerId { get; set; }

        [Required(ErrorMessage = "Plant is required")]
        [JsonProperty("plant")]
        public string Plant { get; set; }

        [JsonProperty("additionalData")]
        public List<AdditionalDataDTO> AdditionalData { get; set; }

        [Required]
        [JsonProperty("shippingInfo")]
        public ShippingDetailsRequestDTO ShippingInfo { get; set; }

        [Required]
        [JsonProperty("entries")]
        public List<OrderEntryRequestDTO> Entries { get; set; }
    }


}