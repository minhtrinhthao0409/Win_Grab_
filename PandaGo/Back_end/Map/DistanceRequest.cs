using System;

namespace PandaGo
{
    class DistanceRequest
    {
        public string Origin { get; private set; }
        public string Destination { get; private set; }
        public string TravelMode { get; private set; }

        public DistanceRequest(string origin, string destination, string travelMode)
        {
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
                throw new ArgumentException("Điểm xuất phát và điểm đến không được để trống.");

            Origin = origin;
            Destination = destination;
            TravelMode = travelMode.ToLower();
        }

    }
}
