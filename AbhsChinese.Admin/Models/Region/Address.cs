using Newtonsoft.Json;

namespace AbhsChinese.Admin.Models.Region
{
    public abstract class Address
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonIgnore]
        public int BelongsTo { get; set; }
    }
}