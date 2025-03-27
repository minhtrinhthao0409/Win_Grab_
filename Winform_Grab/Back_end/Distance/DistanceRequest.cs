using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winform_Grab
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

        public string BuildUrl()
        {
            return $"fakeurl?origins={Uri.EscapeDataString(Origin)}&destinations={Uri.EscapeDataString(Destination)}&mode={TravelMode}";
        }
    }
}
