
using System.Collections.Generic;
namespace PandaGo
{
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
 
}