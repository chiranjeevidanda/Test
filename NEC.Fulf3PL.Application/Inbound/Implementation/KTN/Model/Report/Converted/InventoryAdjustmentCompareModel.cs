using CsvHelper.Configuration.Attributes;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Inbound;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Converted
{
    public class InventoryAdjustmentConvertedModel : InventoryAdjustmentCompareModel
    {
        [Index(10)]
        [Name("Material")]
        public string? Material { get; set; }

        [Index(11)]
        [Name("Material Description")]
        public string? MaterialDescription { get; set; }

        [Index(12)]
        [Name("MaterialGroup")]
        public string? MaterialGroup { get; set; }

        [Index(13)]
        [Name("UOM")]
        public string? Uom { get; set; }

        [Index(14)]
        [Name("Grid")]
        public string? Grid { get; set; }
    }
}
