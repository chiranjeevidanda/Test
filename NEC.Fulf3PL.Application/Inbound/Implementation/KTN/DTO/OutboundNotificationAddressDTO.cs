using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class OutboundNotificationAddressDTO
    {

        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("partyQualifier")]
        public string PartyQualifier { get; set; }

        [JsonProperty("partyIdentification")]
        public string PartyIdentification { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("postcodeIdentification")]
        public string Postalcode { get; set; }

        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("webPage")]
        public string WebPage { get; set; }

        [JsonProperty("addressReference")]
        public string AddressReference { get; set; }

        [JsonProperty("transporterServiceLevel")]
        public string TransporterServiceLevel { get; set; }

        [JsonProperty("vatNumber")]
        public string VatNumber { get; set; }

        [JsonProperty("salesNumber")]
        public string SalesNumber { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("name2")]
        public string Name2 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("gsmNo")]
        public string GsmNo { get; set; }
    }
}
