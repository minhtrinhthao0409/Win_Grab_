using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PandaGo
{
    public partial class Loading : Form
    {
        private Customer currentCustomer;
        private PointLatLng startLocation;
        private PointLatLng endLocation;
        private string travelMode;
        private double distance;
        private double duration;
        private double fare;

        public Loading(Customer customer, PointLatLng startLocation, PointLatLng endLocation, string travelMode, double distance, double duration, double fare)
        {
            InitializeComponent();
            this.currentCustomer = customer;
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            this.travelMode = travelMode;
            this.distance = distance;
            this.duration = duration;
            this.fare = fare;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.TopMost = true;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 2;
                process.Text = progressBar1.Value.ToString() + "%";
            }
            else
            {
                timer1.Stop();

                // Đăng ký sự kiện OnDriverFound
                LookForDriver.OnDriverFound += (Driver, location) =>
                {
                    MessageBox.Show($"Đã tìm thấy tài xế {Driver.Name} tại vị trí ({location.Lat}, {location.Lng})!");
                    // Có thể thêm logic khác, ví dụ: ghi log, gửi thông báo, v.v.
                };

                bool carType = travelMode.ToLower() == "car" ? false : true;
                Driver driver = LookForDriver.FindDriver(startLocation, carType);
                if (driver == null)
                {
                    MessageBox.Show("Không tìm thấy tài xế phù hợp. Vui lòng thử lại sau.");
                    this.Close();
                    return;
                }

                if (currentCustomer != null)
                {
                    Trip trip = TripHandler.CreateTrip(currentCustomer, startLocation, endLocation, driver, distance, fare);
                    MessageBox.Show($"Chuyến đi {trip.Id} đã được tạo thành công.");
                }
                else
                {
                    MessageBox.Show("Không thể tạo chuyến đi. Thông tin khách hàng không tồn tại.");
                }

                this.Close();
            }
        }

        private void brnCancel_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            DialogResult result = MessageBox.Show("Bạn có chắc muốn hủy chuyến?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
                if (currentCustomer != null)
                {
                    new Booking(currentCustomer).Show();
                }
                else
                {
                    MessageBox.Show("Không thể mở form Booking. Thông tin khách hàng không tồn tại.");
                }
            }
            else
            {
                timer1.Start();
            }
        }
    }
}
