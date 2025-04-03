using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.DTO
{
    public class ShipmentEntryRequestDTO
    {
        [JsonProperty("entryNumber")]
        public int? EntryNumber { get; set; }

        [JsonProperty("productCode")]
        public string? ProductCode { get; set; }

        [JsonProperty("productDescription")]
        public string? ProductDescription { get; set; }

        [JsonProperty("scheduleLineNo")]
        public string? ScheduleLineNo { get; set; }

        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        [JsonProperty("confirmedQuantity")]
        public int ConfirmedQuantity { get; set; }

        [JsonProperty("shippedQuantity")]
        public int ShippedQuantity { get; set; }

        [JsonProperty("unShippedQuantity")]
        public int UnShippedQuantity { get; set; }

        [JsonProperty("customerSKU")]
        public string CustomerSKU { get; set; }

        [JsonProperty("unitOfMeasure", NullValueHandling = NullValueHandling.Ignore)]
        public string? UnitOfMeasure { get; set; }

        [JsonProperty("trackingDetails")]
        public List<TrackingLineDetails> TrackingLineDetails { get; set; }
    }
}