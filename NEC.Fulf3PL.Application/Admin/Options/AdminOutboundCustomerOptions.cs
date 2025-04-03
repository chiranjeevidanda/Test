namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options
{
    public class AdminOutboundCustomerOptions
    {
        public const string SectionName = "AdminOutboundCustomer";
        public List<string> ProductMaster { get; set; }
        public List<string> PurchaseOrder { get; set; }
        public List<string> CreateOrder { get; set; }
        public List<string> ReturnOrder { get; set; }
    }
}
