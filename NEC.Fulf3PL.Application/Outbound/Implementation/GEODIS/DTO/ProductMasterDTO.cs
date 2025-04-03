using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.GEODIS.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ProductMasterDTO
    {
        [JsonProperty("eventId")]
        public string EventId { get; set; }

        [JsonProperty("transaction")]
        public string Transaction { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("materialNumber")]
        public string MaterialNumber { get; set; }

        [JsonProperty("upcCode")]
        public string UpcCode { get; set; }

        [JsonProperty("materialDescription")]
        public string MaterialDescription { get; set; }

        [JsonProperty("materialGroupDescription")]
        public string MaterialGroupDescription { get; set; }

        [JsonProperty("plant")]
        public string Plant { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("cost")]
        public decimal Cost { get; set; }

        [JsonProperty("htsNumber")]
        public string HtsNumber { get; set; }

        [JsonProperty("countryofOrigin")]
        public string CountryofOrigin { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("colorCode")]
        public string ColorCode { get; set; }

        [JsonProperty("colorDesc")]
        public string ColorDesc { get; set; }

        [JsonProperty("silhouetteCode")]
        public string SilhouetteCode { get; set; }

        [JsonProperty("silhouetteDesc")]
        public string SilhouetteDesc { get; set; }

        [JsonProperty("seasonCode")]
        public string SeasonCode { get; set; }

        [JsonProperty("seasonDesc")]
        public string SeasonDesc { get; set; }

        [JsonProperty("boxType")]
        public string BoxType { get; set; }

        [JsonProperty("boxUnits")]
        public string BoxUnits { get; set; }
               
    }
}
