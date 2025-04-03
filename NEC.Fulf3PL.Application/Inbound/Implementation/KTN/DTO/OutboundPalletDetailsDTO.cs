using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{

    public class OutboundPalletDetailsDTO
    {
        [JsonProperty("value")]
        public List<OutboundPallets> OutboundPallets { get; set; }
    }
    public class OutboundPallets
    {
        [JsonProperty("parcelCode")]
        public string ParcelCode { get; set; }

        [JsonProperty("packagingCode")]
        public string PackagingCode { get; set; }

        [JsonProperty("parcelTrackingCode")]
        public string ParcelTrackingCode { get; set; }

        [JsonProperty("parcelTrackingURL")]
        public string ParcelTrackingURL { get; set; }

        [JsonProperty("shippingTrackingCode")]
        public string ShippingTrackingCode { get; set; }

        [JsonProperty("barcodeShippingLabel")]
        public string BarcodeShippingLabel { get; set; }

        [JsonProperty("barcodeReturnLabel")]
        public string BarcodeReturnLabel { get; set; }

        [JsonProperty("totalGrossWeight")]
        public decimal TotalGrossWeight { get; set; }

        [JsonProperty("totalTareWeight")]
        public decimal TotalTareWeight { get; set; }

        [JsonProperty("totalNetWeight")]
        public decimal TotalNetWeight { get; set; }

        [JsonProperty("parcelWidth")]
        public decimal ParcelWidth { get; set; }

        [JsonProperty("parcelHeight")]
        public decimal ParcelHeight { get; set; }

        [JsonProperty("parcelDepth")]
        public decimal ParcelDepth { get; set; }

        [JsonProperty("masterId")]
        public string MasterId { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("parcelId")]
        public string ParcelId { get; set; }
    }
}
