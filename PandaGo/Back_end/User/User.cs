using System;
using System.Text.Json.Serialization;

namespace PandaGo
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


        public virtual void ShowInfor()
        {
            Console.WriteLine($"Người dùng tên {Name} có số điện thoại {PhoneNumber}");
        }
    }
}
