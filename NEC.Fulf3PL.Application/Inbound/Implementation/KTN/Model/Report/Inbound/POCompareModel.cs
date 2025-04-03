using CsvHelper.Configuration.Attributes;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Inbound
{
    public class POCompareModel : IReportProductCode
    {
        [Index(0)]
        [Name("Transport IDCustomerReference2")]
        public string? TransportIDCustomerReference2 { get; set; }

        [Index(1)]
        [Name("Customer")]
        public string? Customer { get; set; }

        [Index(2)]
        [Name("Delivery Type")]
        public string? DeliveryType { get; set; }

        [Index(3)]
        [Name("CustomerReference2")]
        public string? CustomerReference2 { get; set; }

        [Index(4)]
        [Name("UPC")]
        public string? Upc { get; set; }

        [Index(5)]
        [Name("LineReference")]
        public string? LineReference { get; set; }

        [Index(6)]
        [Name("KTN Qty Received")]
        public string? KtnQtyReceived { get; set; }
    }
}
