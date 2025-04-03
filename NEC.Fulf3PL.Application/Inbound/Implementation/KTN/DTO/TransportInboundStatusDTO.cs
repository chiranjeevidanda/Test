using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{
    public class TransportInboundStatusDTO
    {
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("customerReference")]
        public string CustomerReference { get; set; }

        [JsonProperty("transportInboundReference")]
        public string TransportInboundReference { get; set; }

        [JsonProperty("containerNumber")]
        public string ContainerNumber { get; set; }

        [JsonProperty("dateTimeExpected")]
        public string DateTimeExpected { get; set; }

        [JsonProperty("dateTimePlanned")]
        public string DateTimePlanned { get; set; }

        [JsonProperty("dateTimeBooked")]
        public string DateTimeBooked { get; set; }

        [JsonProperty("dateTimeArrived")]
        public string DateTimeArrived { get; set; }

        [JsonProperty("dateTimeEndDischarging")]
        public string DateTimeEndDischarging { get; set; }

        [JsonProperty("dateTimeDeparted")]
        public string DateTimeDeparted { get; set; }

        [JsonProperty("dateTimeCancelled")]
        public string DateTimeCancelled { get; set; }

        [JsonProperty("dateTimeClosed")]
        public string DateTimeClosed { get; set; }

        [JsonProperty("isContinuousStockValidation")]
        public bool IsContinuousStockValidation { get; set; }
        
        [JsonProperty("typeTransport")]
        public string TypeTransport { get; set; }

        [JsonProperty("typeTransportInbound")]
        public string TypeTransportInbound { get; set; }

    }
}
