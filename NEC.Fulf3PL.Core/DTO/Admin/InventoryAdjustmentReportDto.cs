namespace NEC.Fulf3PL.Core.DTO.Admin
{
    public class InventoryAdjustmentReportDto
    {
        public string? EventId { get; set; }
        public string? ReceivedTimestamp { get; set; }
        public string? NecSapPlant { get; set; }
        public string? AdjustmentNumber { get; set; }
        public string? AdjustmentDate { get; set; }
        public string? Quantity { get; set; }
        public string? UPC { get; set; }
        public string? AdjustmentDirection { get; set; }
        public string? AdjustmentReasonText { get; set; }
        public string? ErrorReason { get; set; }
    }
}
