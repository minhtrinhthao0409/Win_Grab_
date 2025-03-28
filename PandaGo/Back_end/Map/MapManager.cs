using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace PandaGo
{
    class MapManager
    {
        private readonly GMapControl _mapControl;

        public MapManager(GMapControl mapControl, string apiKey = "")
        {
            _mapControl = mapControl;
            InitializeMap();
        }
        private void InitializeMap()
        {
            _mapControl.MapProvider = GoogleMapProvider.Instance;
            _mapControl.Position = new PointLatLng(10.7769, 106.6953); // TP.HCM mặc định
            _mapControl.MinZoom = 1;
            _mapControl.MaxZoom = 18;
            _mapControl.Zoom = 12;
            _mapControl.CanDragMap = true;
        }
        public async Task<(double Distance, double Duration, double Fare)> ShowRouteAsync(string origin, string destination, string travelMode)
        {

            PointLatLng start =  GetCoordinates(origin);
            PointLatLng end = GetCoordinates(destination);

            double distance_ = DistanceCalculator.CalculateDistance(start, end);
            double duration = distance_ / 20 * 60;

            // Tính giá tiền
            double fare = CalculateFare(distance_, travelMode);

            // Vẽ tuyến đường thẳng
            List<PointLatLng> routePoints = new List<PointLatLng> { start, end };
            GMapRoute route = new GMapRoute(routePoints, "Route") { Stroke = new Pen(Color.Blue, 2) };
            GMapOverlay routeOverlay = new GMapOverlay("RouteOverlay");
            routeOverlay.Routes.Add(route);

            // Đánh dấu điểm
            GMapOverlay markers = new GMapOverlay("Markers");
            markers.Markers.Add(new GMarkerGoogle(start, GMarkerGoogleType.pink_pushpin));
            markers.Markers.Add(new GMarkerGoogle(end, GMarkerGoogleType.purple_dot));

            // Cập nhật bản đồ
            _mapControl.Overlays.Clear();
            _mapControl.Overlays.Add(routeOverlay);
            _mapControl.Overlays.Add(markers);
            _mapControl.ZoomAndCenterMarkers("Markers");

            await Task.Delay(100); // Simulate async work
            return (distance_, duration, fare); 
        }
        private double CalculateFare(double distance, string vehicleType)
        {
            if (vehicleType == "Car")
                return distance * 20000;
            else if (vehicleType == "Bike")
                return distance * 5000;
            return 0; 
        }
        private PointLatLng GetCoordinates(string address)
        {
            address = address.ToLower();
            if (address.Contains("ueh cs b")) return new PointLatLng(10.76130, 106.66834);
            if (address.Contains("ueh cs n")) return new PointLatLng(10.70697, 106.64021);
            if (address.Contains("bến bạch đằng")) return new PointLatLng(10.77590, 106.70696);
            if (address.Contains("nhà hát lớn tp.hcm")) return new PointLatLng(10.77672, 106.70317);
            if (address.Contains("dinh độc lập") || address.Contains("dinh thống nhất"))
                return new PointLatLng(10.7769, 106.6953); // Dinh Độc Lập
            if (address.Contains("chợ bến thành"))
                return new PointLatLng(10.7719, 106.6981); // Chợ Bến Thành
            if (address.Contains("nhà thờ đức bà"))
                return new PointLatLng(10.7798, 106.6990); // Nhà thờ Đức Bà Sài Gòn
            if (address.Contains("thảo cầm viên"))
                return new PointLatLng(10.7876, 106.7051); // Thảo Cầm Viên Sài Gòn
            return new PointLatLng(10.7769, 106.7009); // TP.HCM mặc định
        }
    }
 }
