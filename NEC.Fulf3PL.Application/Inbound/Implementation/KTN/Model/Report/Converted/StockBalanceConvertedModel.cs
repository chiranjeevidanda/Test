using CsvHelper.Configuration.Attributes;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Inbound;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Converted
{
   public class StockBalanceConvertedModel : StockBalanceModel
    {
        [Index(4)]
        [Name("Material")]
        public string? Material { get; set; }

        [Index(5)]
        [Name("Material Description")]
        public string? MaterialDescription { get; set; }

        [Index(6)]
        [Name("Material Group")]
        public string? MaterialGroup { get; set; }

        [Index(7)]
        [Name("Grid")]
        public string? Grid { get; set; }
    }
}
