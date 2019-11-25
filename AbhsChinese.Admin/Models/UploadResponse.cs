using Newtonsoft.Json;

namespace AbhsChinese.Admin.Models
{
    public class UploadResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}