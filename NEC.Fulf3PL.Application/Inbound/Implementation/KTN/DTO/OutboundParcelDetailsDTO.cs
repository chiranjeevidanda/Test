using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{

    public class OutboundParcelDetailsDTO
    {
        [JsonProperty("value")]
        public List<OutboundParcels> OutboundParcels { get; set; }
    }
    public class OutboundParcels
    {
        [JsonProperty("documentId")]
        public string DocumentId { get; set; }

        [JsonProperty("colliNo")]
        public int ColliNo { get; set; }

        [JsonProperty("parcelId")]
        public string ParcelId { get; set; }

        [JsonProperty("parcelCode")]
        public string ParcelCode { get; set; }

        [JsonProperty("packagingCode")]
        public string PackagingCode { get; set; }

        [JsonProperty("parcelTrackingCode")]
        public string ParcelTrackingCode { get; set; }

        [JsonProperty("shippingTrackingCode")]
        public string ShippingTrackingCode { get; set; }

        [JsonProperty("barcodeShippingLabel")]
        public string BarcodeShippingLabel { get; set; }

        [JsonProperty("barcodeReturnLabel")]
        public string BarcodeReturnLabel { get; set; }

        [JsonProperty("parcelTrackingURL")]
        public string ParcelTrackingURL { get; set; }

        [JsonProperty("grossWeight")]
        public decimal GrossWeight { get; set; }

        [JsonProperty("tareWeight")]
        public decimal TareWeight { get; set; }

        [JsonProperty("netWeight")]
        public decimal NetWeight { get; set; }

        [JsonProperty("width")]
        public decimal Width { get; set; }

        [JsonProperty("height")]
        public decimal Height { get; set; }

        [JsonProperty("depth")]
        public decimal Depth { get; set; }

        [JsonProperty("barcodeDestinationLabel")]
        public string BarcodeDestinationLabel { get; set; }

        [JsonProperty("masterId")]
        public string MasterId { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

    }
}
