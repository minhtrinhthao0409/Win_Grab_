
using System.Collections.Generic;
using GMap.NET;
namespace PandaGo
{

    public delegate void DriverFoundHandler(Driver driver, PointLatLng startLocation);
    public class LookForDriver
    {
        private const double MAX_K = 3; // km, giới hạn tối đa

        // Sự kiện được kích hoạt khi tìm thấy tài xế
        public static event DriverFoundHandler OnDriverFound;

        public static Driver FindDriver(PointLatLng location, bool carType, double k = 1)
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
}