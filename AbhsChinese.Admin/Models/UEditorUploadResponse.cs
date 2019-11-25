using Newtonsoft.Json;
using System.IO;

namespace AbhsChinese.Admin.Models
{
    public class UEditorUploadResponse : UploadResponse
    {
        [JsonProperty("name")]
        public string Name { get { return Path.GetFileName(Url); } }

        [JsonProperty("originalName")]
        public string OriginalName { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("type")]
        public string Type { get { return Path.GetExtension(OriginalName); } }

        public UEditorUploadResponse()
        {
        }

        public UEditorUploadResponse(string originalName, string url, int contentLength,
            string state)
        {
            OriginalName = originalName;
            Url = url;
            Size = contentLength;
            State = state;
        }
    }
}