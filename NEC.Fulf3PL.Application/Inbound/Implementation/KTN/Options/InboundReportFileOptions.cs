namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Options
{
    public class InboundReportFileOptions
    {
        public const string SectionName = "InboundReportFileSettings";

        public InboundReportFileSettings GoodsIssue { get; set; } = new InboundReportFileSettings
        {
            FileDelimiter = ";",
            FileExtension = ".csv",
            FileNames = new string[]
            {
                "COMPARE REPORT CANCELLED",
                "COMPARE REPORT SHIPPED"
            }
        };

        public InboundReportFileSettings POCompare { get; set; } = new InboundReportFileSettings
        {
            FileDelimiter = ";",
            FileExtension = ".csv",
            FileNames = new string[]
            {
                "COMPARE REPORT INBOUND"
            }
        };

        public InboundReportFileSettings InventoryAdjustment { get; set; } = new InboundReportFileSettings
        {
            FileDelimiter = ";",
            FileExtension = ".csv",
            FileNames = new string[]
            {
                "COMPARE REPORT STOCK"
            }
        };

        public InboundReportFileSettings ReturnReceipt { get; set; } = new InboundReportFileSettings
        {
            FileDelimiter = ";",
            FileExtension = ".csv",
            FileNames = new string[]
            {
                "COMPARE REPORT RETURN"
            }
        };

        public InboundReportFileSettings StockBalance { get; set; } = new InboundReportFileSettings
        {
            FileDelimiter = ";",
            FileExtension = ".csv",
            FileNames = new string[]
            {
                "STKBAL"
            }
        };
    }


    public class InboundReportFileSettings
    {
        public IEnumerable<string> FileNames { get; set; } = Enumerable.Empty<string>();

        public string FileDelimiter { get; set; } = string.Empty;

        public string FileExtension { get; set; } = string.Empty;
    }
}
