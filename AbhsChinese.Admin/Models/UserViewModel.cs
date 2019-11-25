using Newtonsoft.Json;

namespace AbhsChinese.Admin.Models
{
    public class UserViewModel
    {
        [JsonProperty("id")]
        public int Id { set; get; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("hobby")]
        public string Hobby { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("resume")]
        public UEditor Resume { get; set; }

        [JsonProperty("profilePicture")]
        public string ProfilePicture { get; set; }
    }
}