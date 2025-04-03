using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{
    public class TransportInboundExpandLinesDTO
    {

        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("assignmentReference")]
        public string AssignmentReference { get; set; }

        [JsonProperty("supplierOrderReference")]
        public string SupplierOrderReference { get; set; }

        [JsonProperty("deliverytype")]
        public string Deliverytype { get; set; }

        [JsonProperty("customerReference2")]
        public string CustomerReference2 { get; set; }

        [JsonProperty("reasonCode")]
        public string ReasonCode { get; set; }

        [JsonProperty("datePlanned")]
        public string DatePlanned { get; set; }

        [JsonProperty("lastModifiedDateTime")]
        public string LastModifiedDateTime { get; set; }

        [JsonProperty("buyer")]
        public string Buyer { get; set; }

        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }

        [JsonProperty("addresses")]
        public List<InboundAdressDTO> Addresses { get; set; }

        [JsonProperty("lines")]
        public List<InboundLineItemDetails> Lines { get; set; }

    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class InboundAdressDTO
    {
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("partyQualifier")]
        public string PartyQualifier { get; set; }

        [JsonProperty("partyIdentification")]
        public string PartyIdentification { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("postcodeIdentification")]
        public string Postalcode { get; set; }

        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("addressReference")]
        public string AddressReference { get; set; }

        [JsonProperty("name2")]
        public string Name2 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class InboundLineItemDetails
    {
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("lineReference")]
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

        [JsonProperty("quantityAssigned")]
        public int QuantityAssigned { get; set; }

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

        [JsonProperty("purchaseAmount")]
        public int PurchaseAmount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("reference1")]
        public string Reference1 { get; set; }

        [JsonProperty("reference2")]
        public string Reference2 { get; set; }

        [JsonProperty("reference3")]
        public string Reference3 { get; set; }

        [JsonProperty("reference4")]
        public string Reference4 { get; set; }

        [JsonProperty("transporter")]
        public string Transporter { get; set; }

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

        [JsonProperty("customsDocumentType")]
        public string CustomsDocumentType { get; set; }

        [JsonProperty("customsDocumentNo")]
        public string CustomsDocumentNo { get; set; }

        [JsonProperty("customsDocumentDate")]
        public string CustomsDocumentDate { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }
    }
}
