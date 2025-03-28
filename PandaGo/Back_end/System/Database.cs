using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace PandaGo
{
    public class DataManager
    {
        private static readonly string CustomerFile = "customers.json";
        private static readonly string DriverFile = "drivers.json";
        private static readonly string TripFile = "trips.json";

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            IncludeFields = false
        };

        // Lưu danh sách vào file JSON
        private static void SaveToJson<T>(List<T> data, string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string backupFile = fileName + ".backup";
                    File.Copy(fileName, backupFile, true);
                }   
                string jsonString = JsonSerializer.Serialize(data, Options);
                File.WriteAllText(fileName, jsonString);
                Console.WriteLine("Đã lưu dữ liệu vào " + fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu file " + fileName + ": " + ex.Message);
            }
        }

        // Đọc danh sách từ file JSON
        private static List<T> LoadFromJson<T>(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.WriteAllText(fileName, "[]");
                    return new List<T>();
                }

                string jsonString = File.ReadAllText(fileName);
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    File.WriteAllText(fileName, "[]");
                    return new List<T>();
                }

                List<T> result = JsonSerializer.Deserialize<List<T>>(jsonString, Options);
                return result != null ? result : new List<T>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Lỗi định dạng JSON trong file {fileName}: {ex.Message}");
                File.WriteAllText(fileName, "[]");
                return new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc file " + fileName + ": " + ex.Message);
                return new List<T>();
            }
        }

        // Customer
        public static void SaveCustomers(List<Customer> customers) => SaveToJson(customers, CustomerFile);
        public static List<Customer> LoadCustomers() => LoadFromJson<Customer>(CustomerFile);

        public static void AddCustomer(Customer customer)
        {
            List<Customer> customers = LoadCustomers();
            customers.Add(customer);
            SaveCustomers(customers);
        }
        public static void DeleteCustomer(string id)
        {
            List<Customer> customers = LoadCustomers();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].Id == id)
                {
                    customers.RemoveAt(i);
                    SaveCustomers(customers);
                    return;
                }
            }
        }
        public static void ClearCustomers()
        {
            SaveToJson(new List<Customer>(), CustomerFile);
        }
        public static Customer GetCustomerById(string id)
        {
            List<Customer> customers = LoadCustomers();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].Id == id)
                {
                    return customers[i];
                }
            }
            return null;
        }
        public static Customer GetCustomerByPhoneNumber(string phoneNumber)
        {
            List<Customer> customers = LoadCustomers();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].PhoneNumber == phoneNumber)
                {
                    return customers[i];
                }
            }
            return null;
        }
        // Driver
        public static void SaveDrivers(List<Driver> drivers) => SaveToJson(drivers, DriverFile);
        public static List<Driver> LoadDrivers() => LoadFromJson<Driver>(DriverFile);
        public static void InitializeDriverData()
        {
            List<Driver> drivers = LoadDrivers();
            if (drivers.Count == 0)
            {
                drivers.Add(new Driver
                {
                    Id = "D001",
                    Name = "Nguyễn Văn An",
                    PhoneNumber = "0901234567",
                    Password = "driver123",
                    VehicleType = false, // Car
                    Availability = true,
                    Location = new GMap.NET.PointLatLng(21.0280, 105.8530), // Gần Hà Nội
                    Vehicle = new Car { PlateNumber = "30A-12345" }
                });
                drivers.Add(new Driver
                {
                    Id = "D002",
                    Name = "Trần Thị Bình",
                    PhoneNumber = "0912345678",
                    Password = "driver456",
                    VehicleType = true, // Bike
                    Availability = true,
                    Location = new GMap.NET.PointLatLng(21.0290, 105.8550), // Gần Hà Nội
                    Vehicle = new Bike { PlateNumber = "29B-67890" }
                });
                SaveDrivers(drivers);
            }
            else
            {

            }
        }
        public static void AddDriver(Driver driver)
        {
            List<Driver> drivers = LoadDrivers();
            drivers.Add(driver);
            SaveDrivers(drivers);
        }
        public static void DeleteDriver(string id)
        {
            List<Driver> drivers = LoadDrivers();
            for (int i = 0; i < drivers.Count; i++)
            {
                if (drivers[i].Id == id)
                {
                    drivers.RemoveAt(i);
                    SaveDrivers(drivers);
                    return;
                }
            }
        }
        public static void ClearDrivers()
        {
            SaveToJson(new List<Driver>(), DriverFile);
        }
        public static Driver GetDriverById(string id)
        {
            List<Driver> drivers = LoadDrivers();
            for (int i = 0; i < drivers.Count; i++)
            {
                if (drivers[i].Id == id)
                {
                    return drivers[i];
                }
            }
            return null;
        }
        // Trip
        public static void SaveTrips(List<Trip> trips) => SaveToJson(trips, TripFile);
        public static List<Trip> LoadTrips() => LoadFromJson<Trip>(TripFile);

        public static void AddTrip(Trip trip)
        {
            List<Trip> trips = LoadTrips();
            trips.Add(trip);
            SaveTrips(trips);
        }
        public static void DeleteTrip(string id)
        {
            List<Trip> trips = LoadTrips();
            for (int i = 0; i < trips.Count; i++)
            {
                if (trips[i].Id == id)
                {
                    trips.RemoveAt(i);
                    SaveTrips(trips);
                    return;
                }
            }
        }
        public static void ClearTrips()
        {
            SaveToJson(new List<Trip>(), TripFile);
        }
        public static bool VerifyUserCredentials(string phoneNumber, string password)
        {
            // Kiểm tra trong danh sách Customer
            List<Customer> customers = LoadCustomers();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].PhoneNumber == phoneNumber && customers[i].Password == password)
                {
                    return true;
                }
            }

            // Kiểm tra trong danh sách Driver
            List<Driver> drivers = LoadDrivers();
            for (int i = 0; i < drivers.Count; i++)
            {
                if (drivers[i].PhoneNumber == phoneNumber && drivers[i].Password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}