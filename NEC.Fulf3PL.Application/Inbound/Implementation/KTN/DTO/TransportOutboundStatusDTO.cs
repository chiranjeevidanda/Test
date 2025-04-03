using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{
    public class TransportOutboundStatusDTO
    {
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("customerReference")]
        public string CustomerReference { get; set; }

        [JsonProperty("loadingReference")]
        public string LoadingReference { get; set; }

        [JsonProperty("transporter")]
        public string Transporter { get; set; }

        [JsonProperty("via")]
        public string Via { get; set; }

        [JsonProperty("typeTransport")]
        public string TypeTransport { get; set; }

        [JsonProperty("typeTransportOutbound")]
        public string TypeTransportOutbound { get; set; }

        [JsonProperty("containerNo")]
        public string ContainerNo { get; set; }

        [JsonProperty("trailerPlateNo")]
        public string TrailerPlateNo { get; set; }

        [JsonProperty("seal1")]
        public string Seal1 { get; set; }

        [JsonProperty("seal2")]
        public string Seal2 { get; set; }

        [JsonProperty("shippingAgent")]
        public string ShippingAgent { get; set; }

        [JsonProperty("emptyDepot")]
        public string EmptyDepot { get; set; }

        [JsonProperty("dateTimePreloaded")]
        public string DateTimePreloaded { get; set; }

        [JsonProperty("dateTimeArrived")]
        public string DateTimeArrived { get; set; }

        [JsonProperty("dateTimeLoadingStarted")]
        public string DateTimeLoadingStarted { get; set; }

        [JsonProperty("dateTimeLoadingEnded")]
        public string DateTimeLoadingEnded { get; set; }

        [JsonProperty("dateTimeDeparted")]
        public string DateTimeDeparted { get; set; }

        [JsonProperty("dateTimeCancelled")]
        public string DateTimeCancelled { get; set; }

        [JsonProperty("dateTimeClosed")]
        public string DateTimeClosed { get; set; }

        [JsonProperty("orderedBy")]
        public string OrderedBy { get; set; }

        [JsonProperty("dateTimeExpectedArrival")]
        public string DateTimeExpectedArrival { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }

        [JsonProperty("lastModifiedDateTime")]
        public string LastModifiedDateTime { get; set; }


    }
}
