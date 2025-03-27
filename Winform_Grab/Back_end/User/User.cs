using System.Text.Json.Serialization;

namespace Winform_Grab
{
    public abstract class User
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

    }

}
