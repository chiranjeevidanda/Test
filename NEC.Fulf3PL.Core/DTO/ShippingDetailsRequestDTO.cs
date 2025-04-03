
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NEC.Fulf3PL.Core.DTO
{
    public class ShippingDetailsRequestDTO
    {
        [JsonProperty("deliveryMode", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryMode { get; set; }

        [JsonProperty("deliveryCost")]
        public decimal DeliveryCost { get; set; }

        [JsonProperty("deliveryZone", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryZone { get; set; }

        [JsonProperty("shippingProvider", NullValueHandling = NullValueHandling.Ignore)]
        public string ShippingProvider { get; set; }

        [JsonProperty("shipmentId", NullValueHandling = NullValueHandling.Ignore)]
        public string ShipmentId { get; set; }

        [JsonProperty("labelRequestId", NullValueHandling = NullValueHandling.Ignore)]
        public string LabelRequestId { get; set; }

        [JsonProperty("sourceAddress", NullValueHandling = NullValueHandling.Ignore)]
        public SourceAddress SourceAddress { get; set; }

        [Required]
        [JsonProperty("destinationAddress")]
        public DestinationAddress DestinationAddress { get; set; }

        [JsonProperty("additionalData")]
        public List<AdditionalDataDTO> AdditionalData { get; set; }

    }

    public class SourceAddress
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name1")]
        public string Name1 { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }

        [JsonProperty("town", NullValueHandling = NullValueHandling.Ignore)]
        public string Town { get; set; }

        [JsonProperty("district", NullValueHandling = NullValueHandling.Ignore)]
        public string District { get; set; }

        [JsonProperty("address1", NullValueHandling = NullValueHandling.Ignore)]
        public string Address1 { get; set; }

        [JsonProperty("address2", NullValueHandling = NullValueHandling.Ignore)]
        public string Address2 { get; set; }

        [JsonProperty("postalcode")]
        public string Postalcode { get; set; }

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("remarks", NullValueHandling = NullValueHandling.Ignore)]
        public string Remarks { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("additionalData")]
        public List<AdditionalDataDTO> AdditionalData { get; set; }

    }

    public class DestinationAddress
    {
        [Required(ErrorMessage = "ID is required")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name1", NullValueHandling = NullValueHandling.Ignore)]
        public string Name1 { get; set; }

        [JsonProperty("name2")]
        public string Name2 { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }

        [JsonProperty("district", NullValueHandling = NullValueHandling.Ignore)]
        public string District { get; set; }

        [JsonProperty("address1", NullValueHandling = NullValueHandling.Ignore)]
        public string Address1 { get; set; }

        [JsonProperty("address2", NullValueHandling = NullValueHandling.Ignore)]
        public string Address2 { get; set; }

        [JsonProperty("address3", NullValueHandling = NullValueHandling.Ignore)]
        public string Address3 { get; set; }

        [JsonProperty("address4", NullValueHandling = NullValueHandling.Ignore)]
        public string Address4 { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [JsonProperty("postalcode")]
        public string Postalcode { get; set; }

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("remarks", NullValueHandling = NullValueHandling.Ignore)]
        public string Remarks { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        /*        [JsonProperty("additionalData")]
                public List<AdditionalDataDTO> AdditionalData { get; set; }*/

    }
}