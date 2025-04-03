using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NEC.Fulf3PL.Core.DTO
{
    public class ReturnOrderRequestDTO
    {
        [Required(ErrorMessage = "RequestId is required")]
        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [Required(ErrorMessage = "OrderNumber is required")]
        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "ReturnNumber is required")]
        [JsonProperty("returnNumber")]
        public string ReturnNumber { get; set; }

        [Required(ErrorMessage = "ReturnCreationDate is required")]
        [JsonProperty("returnCreationDate")]
        public string ReturnCreationDate { get; set; }

        [JsonProperty("returnType")]
        public string ReturnType { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("plant")]
        public string Plant { get; set; }

        [JsonProperty("additionalData")]
        public List<AdditionalDataDTO> AdditionalData { get; set; }

        [JsonProperty("shippingInfo")]
        public ShippingDetailsRequestDTO ShippingInfo { get; set; }

        [JsonProperty("entries")]
        public List<OrderEntryRequestDTO> Entries { get; set; }

    }

}