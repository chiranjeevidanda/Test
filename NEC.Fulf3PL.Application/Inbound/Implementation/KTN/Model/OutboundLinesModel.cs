using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model
{
    public class OutboundLinesModel
    {
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("lineReference")]
        public string LineReference { get; set; }

        [JsonProperty("itemCode")]
        public string ItemCode { get; set; }

        [JsonProperty("itemDescription")]
        public string ItemDescription { get; set; }

        [JsonProperty("quantityToDeliver")]
        public int QuantityToDeliver { get; set; }

        [JsonProperty("quantityCancelled")]
        public int QuantityCancelled { get; set; }

        [JsonProperty("quantityLB2S")]
        public int QuantityLB2S { get; set; }

        [JsonProperty("pricePerUnit")]
        public int PricePerUnit { get; set; }

        [JsonProperty("pricePerUnitCurrency")]
        public string PricePerUnitCurrency { get; set; }

        [JsonProperty("priceDescription")]
        public string PriceDescription { get; set; }

        [JsonProperty("pricePerUnit2")]
        public int PricePerUnit2 { get; set; }

        [JsonProperty("pricePerUnitCurrency2")]
        public string PricePerUnitCurrency2 { get; set; }

        [JsonProperty("pricePerUnit3")]
        public int PricePerUnit3 { get; set; }

        [JsonProperty("pricePerUnitCurrency3")]
        public string PricePerUnitCurrency3 { get; set; }

        [JsonProperty("reference1")]
        public string Reference1 { get; set; }

        [JsonProperty("reference2")]
        public string Reference2 { get; set; }

        [JsonProperty("reference3")]
        public string Reference3 { get; set; }

        [JsonProperty("reference4")]
        public string Reference4 { get; set; }

        [JsonProperty("destinationItemCode")]
        public string DestinationItemCode { get; set; }

        [JsonProperty("destinationItemDescription")]
        public string DestinationItemDescription { get; set; }

        [JsonProperty("blockingCode")]
        public string BlockingCode { get; set; }

        [JsonProperty("customerPackagingCode")]
        public string CustomerPackagingCode { get; set; }

        [JsonProperty("lotNo")]
        public string LotNo { get; set; }

        [JsonProperty("configurationCode")]
        public string ConfigurationCode { get; set; }

        [JsonProperty("bestBeforeDate")]
        public string BestBeforeDate { get; set; }

        [JsonProperty("cancelCode")]
        public string CancelCode { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }

        [JsonProperty("uom")]
        public string UOM { get; set; }
    }
}
