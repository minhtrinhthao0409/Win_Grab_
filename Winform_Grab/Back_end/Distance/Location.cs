using System.Text.Json.Serialization;

namespace Winform_Grab
{
    public class Location
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        // Constructor mặc định để hỗ trợ deserialize
        public Location() { }
    }

}
