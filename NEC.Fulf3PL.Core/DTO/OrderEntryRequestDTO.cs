using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NEC.Fulf3PL.Core.DTO
{
    public class OrderEntryRequestDTO
    {
        [JsonProperty("entryNumber")]
        public int? EntryNumber { get; set; }

        [Required(ErrorMessage = "ProductCode is required")]
        [JsonProperty("productCode")]
        public string? ProductCode { get; set; }

        [JsonProperty("productDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string? ProductDescription { get; set; }

        [JsonProperty("materilId", NullValueHandling = NullValueHandling.Ignore)]
        public string? MaterialId { get; set; }

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public string? Size { get; set; }

        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Total { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; }

        [JsonProperty("unitOfMeasure", NullValueHandling = NullValueHandling.Ignore)]
        public string? UnitOfMeasure { get; set; }

        [Required(ErrorMessage = "ScheduleLineNo is required")]
        [JsonProperty("scheduleLineNo")]
        public string? ScheduleLineNo { get; set; }

        [JsonProperty("additionalData")]
        public List<AdditionalDataDTO> AdditionalData { get; set; }
    }
}