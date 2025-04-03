using CsvHelper.Configuration.Attributes;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Inbound
{
    public class StockBalanceModel : IReportProductCode
    {
        [Index(0)]
        [Name("Plant")]
        public string? Plant { get; set; }

        [Index(1)]
        [Name("UPC")]
        public string? Upc { get; set; }

        [Index(2)]
        [Name("On-Hand Quantity")]
        public string? OnHandQuantity { get; set; }

        [Index(3)]
        [Name("Available Quantity")]
        public string? AvailableQuantity { get; set; }
    }
}
