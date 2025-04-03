﻿using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities
{
    public class SofiaOutboundRequestsLogModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string Provider { get; set; }

        [JsonProperty(PropertyName = "requestId")]
        public string RequestId { get; set; }

        [JsonProperty(PropertyName = "requestType")]
        public string RequestType { get; set; }

        [JsonProperty(PropertyName = "loggedOn")]
        public DateTime LoggedOn { get; set; }

        [JsonProperty(PropertyName = "requestPayload")]
        public object RequestPayload { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }

        [JsonProperty(PropertyName = "nextStage")]
        public bool NextStage { get; set; }
    }
}
