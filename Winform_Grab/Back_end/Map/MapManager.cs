using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Winform_Grab;

namespace Winform_Grab
{
    class MapManager
    {
        private readonly GMapControl _mapControl;
        private readonly DistanceService _distanceService;

        public MapManager(GMapControl mapControl, string apiKey = "")
        {
            _mapControl = mapControl;
            _distanceService = new DistanceService(apiKey);
            InitializeMap();
        }

        private void InitializeMap()
        {
            _mapControl.MapProvider = GoogleMapProvider.Instance;
            _mapControl.Position = new PointLatLng(10.7769, 106.7009); // TP.HCM mặc định
            _mapControl.MinZoom = 1;
            _mapControl.MaxZoom = 18;
            _mapControl.Zoom = 12;
            _mapControl.CanDragMap = true;
        }

        public async Task<(double Distance, double Duration, double Fare)> ShowRouteAsync(string origin, string destination, string travelMode)
        {
            var request = new DistanceRequest(origin, destination, travelMode);
            var response = await _distanceService.CalculateDistanceAsync(request);

            double distance = response.Rows[0].Elements[0].Distance.Value / 1000.0; // km
            double duration = response.Rows[0].Elements[0].Duration.Value / 3600.0; // giờ
           
            // Tính giá tiền
            double fare = CalculateFare(distance, travelMode);

            // Giả lập tọa độ
            PointLatLng start = GetCoordinates(origin);
            PointLatLng end = GetCoordinates(destination);

            // Vẽ tuyến đường thẳng
            var routePoints = new List<PointLatLng> { start, end };
            var route = new GMapRoute(routePoints, "Route") { Stroke = new Pen(Color.Blue, 2) };
            var routeOverlay = new GMapOverlay("RouteOverlay");
            routeOverlay.Routes.Add(route);

            // Đánh dấu điểm
            var markers = new GMapOverlay("Markers");
            markers.Markers.Add(new GMarkerGoogle(start, GMarkerGoogleType.pink_pushpin));
            markers.Markers.Add(new GMarkerGoogle(end, GMarkerGoogleType.purple_dot));

            // Cập nhật bản đồ
            _mapControl.Overlays.Clear();
            _mapControl.Overlays.Add(routeOverlay);
            _mapControl.Overlays.Add(markers);
            _mapControl.ZoomAndCenterMarkers("Markers");

            await Task.Delay(100); // Simulate async work
            return (distance: 100.5, duration: 2.5, fare: 100000); // Example values
        }

        private double CalculateFare(double distance, string vehicleType)
        {
            if (vehicleType == "Car")
                return distance * 20000; // 20,000 VNĐ/km
            else if (vehicleType == "Bike")
                return distance * 10000; // 10,000 VNĐ/km
            return 0; // Mặc định nếu không chọn loại xe hợp lệ
        }

        private PointLatLng GetCoordinates(string address)
        {
            if (address.Contains("UEH CS B")) return new PointLatLng(10.76130, 106.66834);
            if (address.Contains("UEH CS N")) return new PointLatLng(10.70697, 106.64021);
            return new PointLatLng(10.7769, 106.7009); // TP.HCM mặc định
        }
    }
}