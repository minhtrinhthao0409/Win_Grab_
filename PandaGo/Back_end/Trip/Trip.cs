using GMap.NET;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PandaGo
{
    public class Trip
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("startLocation")]
        public PointLatLng StartLocation { get; set; }

        [JsonPropertyName("endLocation")]
        public PointLatLng EndLocation { get; set; }

        [JsonPropertyName("driverId")]
        public string DriverId { get; set; }

        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }

        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("paymentMethod")]
        public List<string> PaymentMethod { get; set; } // Danh sách các phương thức thanh toán

        public Trip(string id, 
            PointLatLng startLocation, PointLatLng endLocation, string driverId, string customerId, double distance, double price)
        {
            Id = id;
            StartLocation = startLocation;
            EndLocation = endLocation;
            DriverId = driverId;
            CustomerId = customerId;
            Distance = distance;
            Price = price;
            PaymentMethod = new List<string>(); // Mặc định là danh sách rỗng
        }

        // Constructor mặc định để hỗ trợ deserialize
        public Trip()
        {
            PaymentMethod = new List<string>(); // Đảm bảo luôn có danh sách rỗng khi deserialize
        }

        // Nạp chồng toán tử + để tính tổng tiền
        public static Trip operator +(Trip trip1, Trip trip2)
        {
            return new Trip("", new PointLatLng(0,0), new PointLatLng(0, 0), "", "", 0, trip1.Price + trip2.Price);
        }
    }

}
