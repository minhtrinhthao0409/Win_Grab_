

using System.Collections.Generic;
using System.Threading;
using System;
using Winform_Grab;
namespace Winform_Grab
{
    public class DistanceCalculator
    {
        private const double R = 6371; // Bán kính Trái Đất (km)

        public static double CalculateDistance(Location location1, Location location2)
        {
            double lat1 = DegreeToRadian(location1.Latitude);
            double lon1 = DegreeToRadian(location1.Longitude);
            double lat2 = DegreeToRadian(location2.Latitude);
            double lon2 = DegreeToRadian(location2.Longitude);

            double deltaLat = lat2 - lat1;
            double deltaLon = lon2 - lon1;

            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(deltaLon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c; // Khoảng cách theo km
        }

        private static double DegreeToRadian(double degree)
        {
            return (degree * Math.PI / 180.0);
        }
    }

    public class PriceCalculator
    {
        static int car = 10000;
        static int bike = 5000;
        public static double CalculatePrice(double distance, bool type)
        {
            if (type == false)
            {
                return distance * car;
            }
            else
            {
                return distance * bike;
            }
        }
    }
    public delegate void DriverFoundHandler(Driver driver, Location startLocation);
    public class LookForDriver
    {
        private const double MAX_K = 3; // km, giới hạn tối đa

        // Sự kiện được kích hoạt khi tìm thấy tài xế
        public static event DriverFoundHandler OnDriverFound;

        public static Driver FindDriver(Location location, bool carType, double k = 1)
        {
            List<Driver> drivers = DataManager.LoadDrivers();

            if (drivers.Count == 0)
            {
                return null;
            }

            foreach (Driver d in drivers)
            {
                if (d.VehicleType == carType && d.Availability)
                {
                    double distance = DistanceCalculator.CalculateDistance(location, d.Location);
                    if (distance < k)
                    {
                        OnDriverFound?.Invoke(d, location);
                        return d;
                    }
                }
            }

            if (k + 1 <= MAX_K)
            {
                return FindDriver(location, carType, k + 1);
            }
            return null;
        }
    }

    public class TripHandler
    {
        public static Trip CreateTrip(Customer CurrentCustomer, Location startLocation, Location EndLocation, Driver driver, double distance, double price)
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

    public class TripHistory
    {
        private Customer customer;
        private List<Trip> trips;

        public TripHistory(Customer customer)
        {
            this.customer = customer;
            trips = new List<Trip>();
            LoadTripsForCustomer();
        }

        // Tải danh sách chuyến đi của khách hàng từ DataManager
        private void LoadTripsForCustomer()
        {
            List<Trip> allTrips = DataManager.LoadTrips();
            for (int i = 0; i < allTrips.Count; i++)
            {
                if (allTrips[i].CustomerId == customer.Id)
                {
                    trips.Add(allTrips[i]);
                }
            }
        }

        //In thông tin lịch sử chuyến đi
        public string PrintTripHistory()
        {
            if (trips.Count == 0)
            {
                return $"Khách hàng {customer.Name} chưa có chuyến đi nào.";
            }
            else
            {
                string result = "Lịch sử chuyến đi của khách hàng:\n";
                result += $"ID: {customer.Id}, Tên: {customer.Name}, SĐT: {customer.PhoneNumber}\n";
                result += "Danh sách chuyến đi:\n";
                for (int i = 0; i < trips.Count; i++)
                {
                    Trip trip = trips[i];
                    string paymentMethods = trip.PaymentMethod.Count > 0 ? string.Join(", ", trip.PaymentMethod) : "Chưa xác định";
                    result += $" - Chuyến {trip.Id}: Khoảng cách: {trip.Distance}km, Giá: {trip.Price}, Phương thức thanh toán: {paymentMethods}\n";
                }

                double totalPrice = CalculateTotalPrice();
                result += $"Tổng tiền đã trả: {totalPrice}";
                return result;
            }
        }

        // Tính tổng tiền bằng toán tử +
        private double CalculateTotalPrice()
        {
            if (trips.Count == 0)
            {
                return 0;
            }

            Trip totalTrip = trips[0];
            for (int i = 1; i < trips.Count; i++)
            {
                totalTrip = totalTrip + trips[i];
            }
            return totalTrip.Price;
        }

        public static void ViewTripHistory(Customer currentCustomer)
        {
            TripHistory history = new TripHistory(currentCustomer);
            history.PrintTripHistory();
        }
    }
    public class Book
    {
        public static void BookTrip(Customer currentCustomer)
        {
            double sLongitude = Check.GetValidDoubleInput();
            double sLatitude = Check.GetValidDoubleInput();
            Location startLocation = new Location(sLatitude, sLongitude);

            double eLongitude = Check.GetValidDoubleInput();
            double eLatitude = Check.GetValidDoubleInput();
            Location endLocation = new Location(eLatitude, eLongitude);

            string vehicleInput = Console.ReadLine().ToLower();
            bool carType = vehicleInput == "car" ? false : true;

            // Tìm tài xế trước
            Driver driver = LookForDriver.FindDriver(startLocation, carType);
            if (driver == null)
            {
                return;
            }

            // Tính khoảng cách và giá tiền sau khi tìm thấy tài xế
            double distance = DistanceCalculator.CalculateDistance(startLocation, endLocation);
            double price = PriceCalculator.CalculatePrice(distance, carType);

            // Chờ tài xế
            bool tripCancelled = false;
            int waitTime = 15000;
            int elapsed = 0;
            while (elapsed < waitTime)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.KeyChar == '1')
                    {
                        tripCancelled = true;
                        break;
                    }
                }
                Thread.Sleep(1000);
                elapsed += 1000;
            }

            if (!tripCancelled)
            {
                Trip trip = TripHandler.CreateTrip(currentCustomer, startLocation, endLocation, driver, distance, price);
            }
        }
    }
    public class Check
    {
        public static double GetValidDoubleInput()
        {
            double value;
            while (!double.TryParse(Console.ReadLine(), out value))
            {
            }
            return value;
        }
    }
}