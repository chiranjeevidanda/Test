
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.DTO
{
    public class ProductMasterRequestDTO
    {

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("productCode")]
        public string ProductCode { get; set; }

        [JsonProperty("productDescription")]
        public string ProductDescription { get; set; }

        [JsonProperty("standardCost")]
        public string StandardCost { get; set; }

        [JsonProperty("baseProductCode")]
        public string BaseProductCode { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("plant")]
        public string Plant { get; set; }

        [JsonProperty("additionalData")]
        public List<AdditionalDataDTO> AdditionalData { get; set; }

    }

}