using System;
using GMap.NET;
namespace PandaGo
{
    public class DistanceCalculator
    {
        private const double R = 6371; // Bán kính Trái Đất (km)
        public static double CalculateDistance(PointLatLng location1, PointLatLng location2)
        {
            double lat1 = DegreeToRadian(location1.Lat);
            double lon1 = DegreeToRadian(location1.Lng);
            double lat2 = DegreeToRadian(location2.Lat);
            double lon2 = DegreeToRadian(location2.Lng);

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
}