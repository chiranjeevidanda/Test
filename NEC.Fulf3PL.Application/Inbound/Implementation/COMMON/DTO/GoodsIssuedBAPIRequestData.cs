using NEC.Fulf3PL.Core.DTO;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.COMMON.DTO
{
    public class GoodsIssuedBAPIRequestData
    {
        [JsonProperty("provider", NullValueHandling = NullValueHandling.Ignore)]
        public string Provider { get; set; }

        [JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestId { get; set; }

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeStamp { get; set; }

        [JsonProperty("orderNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderNumber { get; set; }

        [JsonProperty("customerOrderNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerOrderNumber { get; set; }

        [JsonProperty("orderDate", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderDate { get; set; }

        [JsonProperty("vendorId", NullValueHandling = NullValueHandling.Ignore)]
        public string VendorId { get; set; }

        [JsonProperty("plant", NullValueHandling = NullValueHandling.Ignore)]
        public string Plant { get; set; }

        [JsonProperty("incoTerms", NullValueHandling = NullValueHandling.Ignore)]
        public string IncoTerms { get; set; }

        [JsonProperty("deliveryNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryNumber { get; set; }

        [JsonProperty("necSoldTo", NullValueHandling = NullValueHandling.Ignore)]
        public string NecSoldTo { get; set; }

        [JsonProperty("freightTerm", NullValueHandling = NullValueHandling.Ignore)]
        public string FreightTerm { get; set; }

        [JsonProperty("storeNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string StoreNumber { get; set; }

        [JsonProperty("deptNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string DeptNumber { get; set; }

        [JsonProperty("shipToDC", NullValueHandling = NullValueHandling.Ignore)]
        public string ShipToDC { get; set; }

        [JsonProperty("shipToCustId", NullValueHandling = NullValueHandling.Ignore)]
        public string ShipToCustID { get; set; }

        [JsonProperty("actualCarrier", NullValueHandling = NullValueHandling.Ignore)]
        public string ActualCarrier { get; set; }

        [JsonProperty("additionalData")]
        public List<AdditionalDataDTO> AdditionalData { get; set; }

        [JsonProperty("shippingInfo")]
        public ShippingDetailsRequestDTO ShippingInfo { get; set; }

        [JsonProperty("entries")]
        public List<ShipmentEntryBapiRequestDTO> Entries { get; set; }

        [JsonProperty("trackingDetails")]
        public ShipmentTrackingDetails TrackingDetails { get; set; }

    }

    public class ShipmentEntryBapiRequestDTO
    {
        [JsonProperty("entryNumber")]
        public int? EntryNumber { get; set; }

        [JsonProperty("productCode")]
        public string? ProductCode { get; set; }

        [JsonProperty("productDescription")]
        public string? ProductDescription { get; set; }

        [JsonProperty("scheduleLineNo")]
        public string? ScheduleLineNo { get; set; }

        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        [JsonProperty("confirmedQuantity")]
        public int ConfirmedQuantity { get; set; }

        [JsonProperty("shippedQuantity")]
        public int ShippedQuantity { get; set; }

        [JsonProperty("unShippedQuantity")]
        public int UnShippedQuantity { get; set; }

        [JsonProperty("customerSKU")]
        public string CustomerSKU { get; set; }

        [JsonProperty("unitOfMeasure", NullValueHandling = NullValueHandling.Ignore)]
        public string? UnitOfMeasure { get; set; }

        [JsonProperty("trackingDetails")]
        public List<TrackingLineDetails> TrackingLineDetails { get; set; }
    }


    public class ShipmentTrackingDetails
    {

        [JsonProperty("firstTrackingNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstTrackingNumber { get; set; }

        [JsonProperty("firstCartonNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstCartonNumber { get; set; }

        [JsonProperty("cartoncount", NullValueHandling = NullValueHandling.Ignore)]
        public int Cartoncount { get; set; }

        [JsonProperty("totalWeight", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalWeight { get; set; }

        [JsonProperty("trackingDetails")]
        public List<TrackingLineDetails> TrackingLineDetails { get; set; }

    }


    public class TrackingLineDetails
    {
        [JsonProperty("lineNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string? LineNumber { get; set; }

        [JsonProperty("trackingNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string? TrackingNumber { get; set; }

        [JsonProperty("cartonNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string? CartonNumber { get; set; }

        [JsonProperty("quantityInbox")]
        public int QuantityInbox { get; set; }

        [JsonProperty("inBoxQty", NullValueHandling = NullValueHandling.Ignore)]
        public string? InBoxQty { get; set; }

        [JsonProperty("inBoxQtyUnitOfMeasure", NullValueHandling = NullValueHandling.Ignore)]
        public string? InBoxQtyUnitOfMeasure { get; set; }

        [JsonProperty("packageWeight", NullValueHandling = NullValueHandling.Ignore)]
        public string? PackageWeight { get; set; }

        [JsonProperty("packageWeightUnitOfMeasure", NullValueHandling = NullValueHandling.Ignore)]
        public string? PackageWeightUnitOfMeasure { get; set; }
    }

}