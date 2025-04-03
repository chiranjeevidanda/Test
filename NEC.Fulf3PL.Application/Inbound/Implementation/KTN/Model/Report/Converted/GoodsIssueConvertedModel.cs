using CsvHelper.Configuration.Attributes;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Inbound;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Converted
{
    public class GoodsIssueConvertedModel : GoodsIssueCompareModel
    {
        [Index(7)]
        [Name("Material")]
        public string? Material {  get; set; }

        [Index(8)]
        [Name("Grid")]
        public string? Grid { get; set; }
    }
}
