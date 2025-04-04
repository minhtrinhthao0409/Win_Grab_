﻿using GMap.NET;
using System;
using System.Drawing;
using System.Text.Json.Serialization;

namespace PandaGo
{
    public class Driver : User
    {
        [JsonPropertyName("vehicle")]
        public Vehicle Vehicle { get; set; }

        [JsonPropertyName("availability")]
        public bool Availability { get; set; }

        [JsonPropertyName("vehicleType")]
        public bool VehicleType { get; set; } // True: bike, False: car

        [JsonPropertyName("location")]
        public PointLatLng Location { get; set; }
        public override void ShowInfor() 
        { 
            Console.WriteLine($"Tài xế tên {Name} có số điện thoại {PhoneNumber}"); 
        }

    }

}
