using System.Collections.Generic;
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

    public class Driver : User
    {
        [JsonPropertyName("vehicle")]
        public Vehicle Vehicle { get; set; }

        [JsonPropertyName("availability")]
        public bool Availability { get; set; }

        [JsonPropertyName("vehicleType")]
        public bool VehicleType { get; set; } // True: bike, False: car

        [JsonPropertyName("location")]
        public Location Location { get; set; }
    }

    public class Customer : User
    {
    }

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

    public class Bike : Vehicle
    {
        public Bike()
        {
            VehicleType = true; // Bike
        }
    }

    public class Car : Vehicle
    {
        public Car()
        {
            VehicleType = false; // Car
        }
    }

    public class Trip
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("startLocation")]
        public Location StartLocation { get; set; }

        [JsonPropertyName("endLocation")]
        public Location EndLocation { get; set; }

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

        public Trip(string id, Location startLocation, Location endLocation, string driverId, string customerId, double distance, double price)
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
            return new Trip("", null, null, "", "", 0, trip1.Price + trip2.Price);
        }
    }

}
