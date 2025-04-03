using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class OrderLineItemConverterDTO
    {
        [JsonProperty("lineReference")]
        public string LineReference { get; set; }

        [JsonProperty("itemCode")]
        public string ItemCode { get; set; }

        [JsonProperty("itemDescription")]
        public string ItemDescription { get; set; }

        [JsonProperty("lotNo", NullValueHandling = NullValueHandling.Ignore)]
        public string LotNo { get; set; }

        [JsonProperty("quantityToDeliver")]
        public int? QuantityToDeliver { get; set; }

        [JsonProperty("pricePerUnit")]
        public decimal PricePerUnit { get; set; }

        [JsonProperty("pricePerUnitCurrency", NullValueHandling = NullValueHandling.Ignore)]
        public string PricePerUnitCurrency { get; set; }

        [JsonProperty("pricePerUnit2")]
        public decimal PricePerUnit2 { get; set; }

        [JsonProperty("pricePerUnitCurrency2", NullValueHandling = NullValueHandling.Ignore)]
        public string PricePerUnitCurrency2 { get; set; }

        [JsonProperty("pricePerUnit3", NullValueHandling = NullValueHandling.Ignore)]
        public decimal PricePerUnit3 { get; set; }

        [JsonProperty("pricePerUnitCurrency3", NullValueHandling = NullValueHandling.Ignore)]
        public string PricePerUnitCurrency3 { get; set; }

        [JsonProperty("reference1", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference1 { get; set; }

        [JsonProperty("reference2", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference2 { get; set; }

        [JsonProperty("reference3", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference3 { get; set; }

        [JsonProperty("reference4", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference4 { get; set; }

        [JsonProperty("destinationItemCode", NullValueHandling = NullValueHandling.Ignore)]
        public string DestinationItemCode { get; set; }

        [JsonProperty("destinationItemDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string DestinationItemDescription { get; set; }

        [JsonProperty("blockingCode", NullValueHandling = NullValueHandling.Ignore)]
        public string BlockingCode { get; set; }

        [JsonProperty("customerPackagingCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerPackagingCode { get; set; }

        [JsonProperty("configurationCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfigurationCode { get; set; }

        [JsonProperty("bestBeforeDate", NullValueHandling = NullValueHandling.Ignore)]
        public string BestBeforeDate { get; set; }

        [JsonProperty("uom", NullValueHandling = NullValueHandling.Ignore)]
        public string UOM { get; set; }
    }
}
