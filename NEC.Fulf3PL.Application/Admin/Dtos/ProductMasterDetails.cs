using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Admin.Dtos
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ProductMasterDetails
    {
        public string? ModifiedDate { get; set; }
        public string? Status { get; set; }
        public SkuDetails ProductDetails { get; set; }
    }
}
