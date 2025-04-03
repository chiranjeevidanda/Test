using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.GEODIS.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CreateOrderDTO
    {
        [JsonProperty("eventId")]
        public string EventId { get; set; }

        [JsonProperty("transaction")]
        public string Transaction { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("deliveryDate")]
        public string DeliveryDate { get; set; }

        [JsonProperty("purchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }

        [JsonProperty("orderDate")]
        public string OrderDate { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }

        [JsonProperty("salesOrder")]
        public string SalesOrder { get; set; }

        [JsonProperty("headervasCodes")]
        public string HeadervasCodes { get; set; }

        [JsonProperty("shipToInfo")]
        public ShipToInfo ShipToInfo { get; set; }

        [JsonProperty("billToInfo")]
        public BillToInfo BillToInfo { get; set; }

        [JsonProperty("shipmentDetails")]
        public ShipmentDetails ShipmentDetails { get; set; }

        [JsonProperty("customerSpecificDetails")]
        public CustomerSpecificDetails CustomerSpecificDetails { get; set; }

        [JsonProperty("lines")]
        public List<OrderLineDetailsDTO> Lines { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CustomerSpecificDetails
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
    public class ShipmentDetails
    {
        [JsonProperty("freightAccountNumber")]
        public string FreightAccountNumber { get; set; }

        [JsonProperty("carrierCode")]
        public string CarrierCode { get; set; }

        [JsonProperty("carrierDesc")]
        public string CarrierDesc { get; set; }

        [JsonProperty("freightTerms")]
        public string FreightTerms { get; set; }

        [JsonProperty("billOfLadinglInstructions")]
        public string BillOfLadinglInstructions { get; set; }

        [JsonProperty("packingSlipMessage")]
        public string PackingSlipMessage { get; set; }

        [JsonProperty("priorityCode")]
        public string PriorityCode { get; set; }

        [JsonProperty("requestedShipDate")]
        public string RequestedShipDate { get; set; }

        [JsonProperty("cancelDate")]
        public string CancelDate { get; set; }

        [JsonProperty("shipCondition")]
        public string ShipCondition { get; set; }

    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class OrderLineDetailsDTO
    {
        [JsonProperty("materialNumber")]
        public string MaterialNumber { get; set; }

        [JsonProperty("upcCode")]
        public string UPCCode { get; set; }

        [JsonProperty("customerLineNumber")]
        public string CustomerLineNumber { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("receivingWarehouseLocation")]
        public string ReceivingWarehouseLocation { get; set; }

        [JsonProperty("vapCode")]
        public string VapCode { get; set; }

        [JsonProperty("customerPOLine")]
        public string CustomerPOLine { get; set; }

        [JsonProperty("customerSKU")]
        public string CustomerSKU { get; set; }

        [JsonProperty("customerZCRS")]
        public string CustomerZCRS { get; set; }

        [JsonProperty("usage")]
        public string Usage { get; set; }

        [JsonProperty("caselotId")]
        public string CaselotId { get; set; }

        [JsonProperty("caselotQty")]
        public string CaselotQty { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

    }
}
