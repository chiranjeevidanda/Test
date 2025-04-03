using CsvHelper.Configuration.Attributes;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Inbound
{
    public class GoodsIssueCompareModel : IReportProductCode
    {
        [Index(0)]
        [Name("Customer")]
        public string? Customer { get; set; }

        [Index(1)]
        [Name("Assignment Reference")]
        public string? AssignmentReference { get; set; }

        [Index(2)]
        [Name("CustomerReference4")]
        public string? CustomerReference4 { get; set; }

        [Index(3)]
        [Name("Line Reference")]
        public string? LineReference { get; set; }

        [Index(4)]
        [Name("UPC")]
        public string? Upc { get; set; }

        [Index(5)]
        [Name("Date Departed")]
        public string? DateDeparted { get; set; }

        [Index(6)]
        [Name("KTN QTY Shipped")]
        public string? KtnQtyShipped { get; set; }
    }
}
