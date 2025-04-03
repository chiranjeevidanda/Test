namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options
{
    public class AdminInboundCustomerOptions
    {
        public const string SectionName = "AdminInboundCustomer";
        public List<string> GoodsReceived { get; set; }
        public List<string> GoodsIssued { get; set; }
        public List<string> ReturnReceived { get; set; }
        public List<string> Inventory { get; set; }
    }
}
