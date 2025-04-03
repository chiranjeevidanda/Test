using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities
{
    public class Article //: BaseEntity<Guid>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("articleId")]
        public string ArticleId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
