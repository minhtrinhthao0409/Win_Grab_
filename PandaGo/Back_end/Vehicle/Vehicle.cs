using System.Text.Json.Serialization;

namespace PandaGo
{
    [JsonDerivedType(typeof(Bike), typeDiscriminator: "bike")]
    [JsonDerivedType(typeof(Car), typeDiscriminator: "car")]
    public abstract class Vehicle
    {
        [JsonPropertyName("plateNumber")]
        public string PlateNumber { get; set; }
        [JsonPropertyName("vehicleType")]
        public bool VehicleType { get; set; } // True: bike, False: car
        // Constructor mặc định để hỗ trợ deserialize
        public Vehicle() { }
    }
}
