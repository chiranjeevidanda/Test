namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options
{
    public class CustomerPlantOptions
    {
        public const string SectionName = "CustomerPlantDetails";
        public List<CustomerPlantDetails> CustomersPlant { get; set; }
        public string ApplicationProvider { get; set; }
    }

    public class CustomerPlantDetails
    {
        public string? Name { get; set; }
        public string? Plant { get; set; }
    }
}
