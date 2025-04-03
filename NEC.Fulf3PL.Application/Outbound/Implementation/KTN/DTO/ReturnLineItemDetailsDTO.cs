using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ReturnLineItemDetailsDTO
    {
        [JsonProperty("returnLineReference")]
        public string LineReference { get; set; }

        [JsonProperty("itemCode")]
        public string ItemCode { get; set; }

        [JsonProperty("itemDescription")]
        public string ItemDescription { get; set; }

        [JsonProperty("countryOfOrigin")]
        public string CountryOfOrigin { get; set; }

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("lotNo")]
        public string LotNo { get; set; }

        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }

        [JsonProperty("qualityCheck")]
        public string QualityCheck { get; set; }

        [JsonProperty("palletNumber")]
        public string PalletNumber { get; set; }

        [JsonProperty("quantityExpected")]
        public int QuantityExpected { get; set; }

        /*        [JsonProperty("quantityToDeliver")]
                public int QuantityToDeliver { get; set; }*/

        [JsonProperty("datePlanned")]
        public string DatePlanned { get; set; }

        [JsonProperty("bestBeforeDate")]
        public string BestBeforeDate { get; set; }

        [JsonProperty("customerExpiryDate")]
        public string CustomerExpiryDate { get; set; }

        [JsonProperty("productionDate")]
        public string ProductionDate { get; set; }

        [JsonProperty("dateTimeToLoad")]
        public string DateTimeToLoad { get; set; }

        [JsonProperty("dateTimeToDeliver")]
        public string DateTimeToDeliver { get; set; }

        /*        [JsonProperty("purchaseAmount")]
                public int PurchaseAmount { get; set; }*/

        [JsonProperty("currency")]
        public string Currency { get; set; }

        /*        [JsonProperty("pricePerUnit")]
                public int PricePerUnit { get; set; }*/

        [JsonProperty("pricePerUnitCurrency")]
        public string PricePerUnitCurrency { get; set; }

        /*        [JsonProperty("pricePerUnit2")]
                public int PricePerUnit2 { get; set; }*/

        [JsonProperty("pricePerUnitCurrency2")]
        public string PricePerUnitCurrency2 { get; set; }

        /*        [JsonProperty("pricePerUnit3")]
                public int PricePerUnit3 { get; set; }*/

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

        [JsonProperty("serviceLevel")]
        public string ServiceLevel { get; set; }

        [JsonProperty("packagingCode")]
        public string PackagingCode { get; set; }

        [JsonProperty("operationType")]
        public string OperationType { get; set; }

        [JsonProperty("retention")]
        public string Retention { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        [JsonProperty("buyer")]
        public string Buyer { get; set; }

        [JsonProperty("transporter")]
        public string Transporter { get; set; }

        [JsonProperty("customsDocumentType")]
        public string CustomsDocumentType { get; set; }

        [JsonProperty("customsDocumentNo")]
        public string CustomsDocumentNo { get; set; }

        [JsonProperty("customsDocumentDate")]
        public string CustomsDocumentDate { get; set; }
    }
}
