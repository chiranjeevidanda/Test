using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.GODISE.Model
{
    public class GoodsIssuedDetailsModel
    {

        [JsonProperty("eventId")]
        public string EventId { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("deliveryNumber")]
        public string DeliveryNumber { get; set; }

        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("orderDate")]
        public string OrderDate { get; set; }

        [JsonProperty("purchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }

        [JsonProperty("salesOrder")]
        public string SalesOrder { get; set; }

        [JsonProperty("shipToInfo")]
        public ShipToInfoModel ShipToInfo { get; set; }

        [JsonProperty("shipmentDetails")]
        public ShipmentDetailsModel ShipmentDetails { get; set; }

        [JsonProperty("customerSpecificDetails")]
        public CustomerSpecificDetailsModel CustomerSpecificDetails { get; set; }

        [JsonProperty("lines")]
        public List<ShipmentItemDetailsModel> Lines { get; set; }

        [JsonProperty("additionalAttributes")]
        public List<AdditionalAttributes> AdditionalAttributes { get; set; }

    }

    public class ShipToInfoModel
    {
        [JsonProperty("name1")]
        public string Name1 { get; set; }

        [JsonProperty("name2")]
        public string Name2 { get; set; }

        [JsonProperty("name3")]
        public string Name3 { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("address3")]
        public string Address3 { get; set; }

        [JsonProperty("storeNumber")]
        public string StoreNumber { get; set; }

        [JsonProperty("departmentNumber")]
        public string DepartmentNumber { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }


    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CustomerSpecificDetailsModel
    {
        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("departmentDesc")]
        public string DepartmentDesc { get; set; }

        [JsonProperty("customerPartnerId")]
        public string CustomerPartnerId { get; set; }

        [JsonProperty("customerShipId")]
        public string CustomerShipId { get; set; }

        [JsonProperty("soldToAccount")]
        public string SoldToAccount { get; set; }

        [JsonProperty("soldToName")]
        public string SoldToName { get; set; }

        [JsonProperty("jmName1")]
        public string JmName1 { get; set; }

        [JsonProperty("jmName2")]
        public string JmName2 { get; set; }

    }


    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ShipmentDetailsModel
    {
        [JsonProperty("freightAccountNumber")]
        public string FreightAccountNumber { get; set; }

        [JsonProperty("carrierCode")]
        public string CarrierCode { get; set; }

        [JsonProperty("carrierDesc")]
        public string CarrierDesc { get; set; }

        [JsonProperty("freightTerms")]
        public string FreightTerms { get; set; }

        [JsonProperty("priorityCode")]
        public string PriorityCode { get; set; }

        [JsonProperty("requestedShipDate")]
        public string RequestedShipDate { get; set; }

        [JsonProperty("cancelDate")]
        public string CancelDate { get; set; }

        [JsonProperty("shipCondition")]
        public string ShipCondition { get; set; }

        [JsonProperty("documentDate")]
        public string DocumentDate { get; set; }

        [JsonProperty("actualShipDate")]
        public string ActualShipDate { get; set; }

        [JsonProperty("actualShipTime")]
        public string ActualShipTime { get; set; }

        [JsonProperty("customerCarrierCode")]
        public string CustomerCarrierCode { get; set; }

        [JsonProperty("providerCarrierCode")]
        public string ProviderCarrierCode { get; set; }

        [JsonProperty("carrierName")]
        public string CarrierName { get; set; }

        [JsonProperty("scacCode")]
        public string ScacCode { get; set; }

        [JsonProperty("carrierService")]
        public string CarrierService { get; set; }

        [JsonProperty("routeType")]
        public string RouteType { get; set; }

        [JsonProperty("routeDescription")]
        public string RouteDescription { get; set; }

        [JsonProperty("cartoncount")]
        public int Cartoncount { get; set; }

        [JsonProperty("grossWeight")]
        public string GrossWeight { get; set; }

        [JsonProperty("totalWeight")]
        public string TotalWeight { get; set; }

        [JsonProperty("weightUom")]
        public string WeightUom { get; set; }

        [JsonProperty("carrierPro")]
        public string CarrierPro { get; set; }

        [JsonProperty("billOfLading")]
        public string BillOfLading { get; set; }

        [JsonProperty("masterBillOfLading")]
        public string MasterBillOfLading { get; set; }

    }

    public class ShipmentItemDetailsModel
    {
        [JsonProperty("materialNumber")]
        public string MaterialNumber { get; set; }

        [JsonProperty("upcCode")]
        public string UPCCode { get; set; }

        [JsonProperty("customerLineNumber")]
        public string CustomerLineNumber { get; set; }

        [JsonProperty("quantityOrdered")]
        public int QuantityOrdered { get; set; }

        [JsonProperty("quantityUnshipped")]
        public int QuantityUnshipped { get; set; }

        [JsonProperty("quantityShipped")]
        public int QuantityShipped { get; set; }

        [JsonProperty("quantityConfirmed")]
        public int QuantityConfirmed { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("receivingWarehouseLocation")]
        public string ReceivingWarehouseLocation { get; set; }

        [JsonProperty("customerSKU")]
        public string CustomerSKU { get; set; }

        [JsonProperty("customerZCRS")]
        public string CustomerZCRS { get; set; }

        [JsonProperty("usage")]
        public string Usage { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("trackingDetails")]
        public List<TrackingDetailsModel> TrackingDetails { get; set; }

        [JsonProperty("additionalAttributes")]
        public List<AdditionalAttributes> AdditionalAttributes { get; set; }
    }

    public class TrackingDetailsModel
    {
        [JsonProperty("trackingNumber")]
        public string TrackingNumber { get; set; }

        [JsonProperty("cartonNumber")]
        public string CartonNumber { get; set; }

        [JsonProperty("quantityInbox")]
        public string QuantityInbox { get; set; }

        [JsonProperty("quantityInboxUom")]
        public string QuantityInboxUom { get; set; }

        [JsonProperty("pedimentoNumber")]
        public string PedimentoNumber { get; set; }

        [JsonProperty("packageWeight")]
        public string PackageWeight { get; set; }

        [JsonProperty("packageWeightUom")]
        public string PackageWeightUom { get; set; }

        [JsonProperty("serialNumbers")]
        public List<SerialNumbers> SerialNumbers { get; set; }

    }

    public class SerialNumbers
    {
        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }
    }

}
