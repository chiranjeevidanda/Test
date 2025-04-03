using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Core.Entities
{
    public class WebhookRequest
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "RequestID")]
        public string RequestId { get; set; }
        [JsonProperty(PropertyName = "requestPayload")]
        public object Payload { get; set; }
        [JsonProperty(PropertyName = "headerInfo")]
        public object Header { get; set; }
        [JsonProperty(PropertyName = "logInformation")]
        public string LogInformation { get; set; }
        [JsonProperty(PropertyName = "requestDate")]
        public DateTime RequestDate { get; set; }      
        
        [JsonProperty(PropertyName = "apiName")]
        public string ApiName { get; set; }
    }
}
