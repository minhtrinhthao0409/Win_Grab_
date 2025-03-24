using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winform_Grab
{ 
    class DistanceService : MapService
    {
        public DistanceService(string apiKey = "") : base(apiKey) { }

        public override Task<string> GetDataAsync(string url)
        {
            // Không cần gọi API thật, trả về chuỗi rỗng vì dùng fake data
            return Task.FromResult("");
        }

        public Task<DistanceResponse> CalculateDistanceAsync(DistanceRequest request)
        {
            // Luôn trả về dữ liệu giả lập
            return Task.FromResult(GetFakeResponse(request.Origin, request.Destination));
        }

        private DistanceResponse GetFakeResponse(string origin, string destination)
        {
            int distanceValue = 1000; // 1000 m mặc định (1 km)
            int durationValue = 3600; // 1 giờ mặc định

            // Giả lập khoảng cách cụ thể cho UEH CS B và UEH CS N
            if (origin.Contains("UEH CS B") && destination.Contains("UEH CS N"))
            {
                distanceValue = 1700; // 1.7 km
                durationValue = 1800; // 30 phút (1800 giây)
            }
            else if (origin.Contains("UEH CS N") && destination.Contains("UEH CS B"))
            {
                distanceValue = 1700; // 1.7 km
                durationValue = 1800; // 30 phút
            }

            return new DistanceResponse
            {
                Status = "OK",
                Rows = new[]
                {
                new DistanceResponse.Row
                {
                    Elements = new[]
                    {
                        new DistanceResponse.Element
                        {
                            Distance = new DistanceResponse.Distance { Text = $"{distanceValue / 1000.0:F1} km", Value = distanceValue },
                            Duration = new DistanceResponse.Duration { Text = $"{durationValue / 60} phút", Value = durationValue },
                            Status = "OK"
                        }
                    }
                }
            },
                OriginAddresses = new[] { origin },
                DestinationAddresses = new[] { destination }
            };
        }
    }
}


