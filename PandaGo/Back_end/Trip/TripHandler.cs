
using System.Collections.Generic;
using System;
using GMap.NET;
namespace PandaGo
{
    public class TripHandler
    {
        public static Trip CreateTrip(Customer CurrentCustomer, PointLatLng startLocation, PointLatLng EndLocation, Driver driver, double distance, double price)
        {
            string ID = TripHandler.GenerateID();
            IPayment payment = PaymentGenerator.GenerateRandomPayment();
            List<string> paymentMethod = new List<string>();
            paymentMethod = payment.Pay();
            Trip trip = new Trip(ID, startLocation, EndLocation, driver.Id, CurrentCustomer.Id, distance, price);
            trip.PaymentMethod = paymentMethod;
            DataManager.AddTrip(trip);
            return trip;
        }

        public static string GenerateID()
        {
            string timestamp = DateTime.UtcNow.Ticks.ToString();
            string random = new Random().Next(1000, 9999).ToString();
            return random;
        }
    }
 
}