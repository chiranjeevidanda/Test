using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{
    public class OutboundShipmentDetails
    {
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("outboundNotificationStatus")]
        public OutboundNotificationStatusModel OutboundNotificationStatus { get; set; }

        [JsonProperty("outboundParcelDetails")]
        public List<OutboundParcels> OutboundParcelDetails { get; set; }

        [JsonProperty("lineParcels")]
        public List<OutboundLineParcels> LineParcels { get; set; }

        [JsonProperty("palletDetails")]
        public List<OutboundPallets> PalletDetails { get; set; }

        [JsonProperty("stockChanges")]
        public List<OutboundStock> StockChanges { get; set; }

        [JsonProperty("dateTimeDeparted")]
        public string DateTimeDeparted { get; set; }
    }
}
