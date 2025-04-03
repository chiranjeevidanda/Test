namespace NEC.Fulf3PL.Core.DTO.Admin
{
    public class ReturnReceiptReportDto
    {
        public string? EventId { get; set; }
        public string? EventTimestamp { get; set; }
        public string? SAPReturnMessage { get; set; }
        public string? KTNUniqueIdentifier { get; set; }
        public string? ReceiptDate { get; set; }
        public string? NecSapPlant { get; set; }
        public string? NecPurchaseOrderNumber  { get; set; }
        public string? DeliveryItem { get; set; }
        public string? PoLineItem { get; set; }
        public string? PoScheduleLine { get; set; }
        public string? QtyReceived { get; set; }
        public string? UPC { get; set; }
        public string? MID { get; set; }
    }
}
