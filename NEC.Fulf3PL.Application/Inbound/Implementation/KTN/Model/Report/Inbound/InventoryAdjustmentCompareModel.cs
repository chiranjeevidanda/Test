using CsvHelper.Configuration.Attributes;
using static NEC.Fulf3PL.Core.Common.Enums;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Inbound
{

    public class InventoryAdjustmentCompareModel : IReportProductCode
    {
        [Index(0)]
        [Name("Adjustment Number")]
        public string? AdjustmentNumber { get; set; }

        [Index(1)]
        [Name("Sales Order #")]
        public string? SalesOrder { get; set; }

        [Index(2)]
        [Name("Customer")]
        public string? Customer { get; set; }

        [Index(3)]
        [Name("UPC")]
        public string? Upc { get; set; }

        [Index(4)]
        [Name("Correction code")]
        public string? CorrectionCode { get; set; }

        [Index(5)]
        [Name("Adjustment Reason Text")]
        public string? AdjustmentReasonText { get; set; }

        [Index(6)]
        [Name("Movement Type")]
        public string? MovementType { get; set; }

        [Index(7)]
        [Name("Adjustment Direction")]
        public string? AdjustmentDirection { get; set; }

        [Index(8)]
        [Name("Adjusted QTY")]
        public string? AdjustedQty { get; set; }

        [Index(9)]
        [Name("Adjustment Date")]
        public string? AdjustmentDate { get; set; }
    }
}
