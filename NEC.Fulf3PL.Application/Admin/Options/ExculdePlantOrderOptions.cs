namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options
{
    public class ExculdePlantOrderOptions
    {
        public const string SectionName = "ExculdePlantOrder";
        public List<ExculdePlantOrder> ExculdeOutboundPlantOrders { get; set; }

        public List<ExculdePlantOrder> ExculdeInboundPlantOrders { get; set; }
    }
    
    public class ExculdePlantOrder
    {
        public string? OrderType { get; set; }
        public string? Plant { get; set; }
    }
}
