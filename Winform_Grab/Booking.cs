using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Web;
using System.Windows.Forms;



namespace Winform_Grab
{
    public partial class Booking : Form
    {
        private readonly MapManager _mapManager;
        private readonly string _apiKey = "FAKE_API_KEY_123456789"; // Thay bằng API Key thực tế
        private Label label1;
        private MainForm parentForm;
        public Booking(MainForm p)
        {
            InitializeComponent();
            _mapManager = new MapManager(gmapControl, _apiKey);
            InitializeControls();
            InitializeMap();
            parentForm = p;
        }

        private void InitializeControls()
        {
            cbTravelMode.Items.AddRange(new[] { "Car", "Bike" });
            cbTravelMode.SelectedIndex = 0;
        }

        private void InitializeMap()
        {
            gmapControl.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmapControl.Position = new PointLatLng(21.0285, 105.8542); // Default to Hanoi, Vietnam
            gmapControl.MinZoom = 1;
            gmapControl.MaxZoom = 20;
            gmapControl.Zoom = 5; // Reasonable initial zoom level
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Loading loading = new Loading();
            loading.Show();
            //this.Hide();
            //string origin = txtOrigin.Text.Trim();
            //string destination = txtDestination.Text.Trim();
            //string travelMode = cbTravelMode.SelectedItem.ToString().ToLower();

            //if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
            //{
            //    MessageBox.Show("Vui lòng nhập điểm xuất phát và điểm đến!");
            //    return;
            //}

            //try
            //{
            //    //btnCalculate.Enabled = false;
            //    if (travelMode == "Car")
            //    {
            //        (double distance, double duration, double fare) = await _mapManager.ShowRouteAsync(origin, destination, travelMode);
            //        lblResult.Text = $"Distance: {distance:F2} km\nDuration: {duration/2:F2} giờ\nFare: {fare * 2:N0} VNĐ";
            //    }
            //    else if (travelMode == "Bike")
            //    {
            //        (double distance, double duration, double fare) = await _mapManager.ShowRouteAsync(origin, destination, travelMode);
            //        lblResult.Text = $"Distance: {distance:F2} km\nDuration: {duration:F2} giờ\nFare: {fare:N0} VNĐ";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Lỗi: {ex.Message}");
            //}
            //finally
            //{
            //    //btnCalculate.Enabled = true;
            //}
        }

        // Designer-generated code
        private void InitializeComponent()
        {
            this.gmapControl = new GMap.NET.WindowsForms.GMapControl();
            this.txtOrigin = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.cbTravelMode = new System.Windows.Forms.ComboBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gmapControl
            // 
            this.gmapControl.Bearing = 0F;
            this.gmapControl.CanDragMap = true;
            this.gmapControl.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.gmapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmapControl.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gmapControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.gmapControl.GrayScaleMode = false;
            this.gmapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmapControl.LevelsKeepInMemory = 5;
            this.gmapControl.Location = new System.Drawing.Point(13, 23);
            this.gmapControl.MarkersEnabled = true;
            this.gmapControl.MaxZoom = 2;
            this.gmapControl.MinZoom = 2;
            this.gmapControl.MouseWheelZoomEnabled = true;
            this.gmapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmapControl.Name = "gmapControl";
            this.gmapControl.NegativeMode = false;
            this.gmapControl.PolygonsEnabled = true;
            this.gmapControl.RetryLoadTile = 0;
            this.gmapControl.RoutesEnabled = true;
            this.gmapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmapControl.ShowTileGridLines = false;
            this.gmapControl.Size = new System.Drawing.Size(257, 297);
            this.gmapControl.TabIndex = 0;
            this.gmapControl.Zoom = 0D;
            // 
            // txtOrigin
            // 
            this.txtOrigin.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrigin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.txtOrigin.Location = new System.Drawing.Point(12, 337);
            this.txtOrigin.Multiline = true;
            this.txtOrigin.Name = "txtOrigin";
            this.txtOrigin.Size = new System.Drawing.Size(256, 27);
            this.txtOrigin.TabIndex = 2;
            this.txtOrigin.Text = "Pick-up Location";
            this.txtOrigin.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtOrigin_MouseClick);
            // 
            // txtDestination
            // 
            this.txtDestination.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestination.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.txtDestination.Location = new System.Drawing.Point(12, 380);
            this.txtDestination.Multiline = true;
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(256, 27);
            this.txtDestination.TabIndex = 2;
            this.txtDestination.Text = "Drop-off Location";
            this.txtDestination.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtDestination_MouseClick);
            // 
            // cbTravelMode
            // 
            this.cbTravelMode.FormattingEnabled = true;
            this.cbTravelMode.Items.AddRange(new object[] {
            ""});
            this.cbTravelMode.Location = new System.Drawing.Point(12, 431);
            this.cbTravelMode.Name = "cbTravelMode";
            this.cbTravelMode.Size = new System.Drawing.Size(120, 21);
            this.cbTravelMode.TabIndex = 3;
            this.cbTravelMode.SelectedIndexChanged += new System.EventHandler(this.cbTravelMode_SelectedIndexChanged);
            // 
            // btnCalculate
            // 
            this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculate.ForeColor = System.Drawing.Color.White;
            this.btnCalculate.Location = new System.Drawing.Point(15, 471);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(120, 29);
            this.btnCalculate.TabIndex = 4;
            this.btnCalculate.Text = "CONFIRM";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.lblResult.Location = new System.Drawing.Point(135, 431);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(138, 69);
            this.lblResult.TabIndex = 5;
            this.lblResult.Click += new System.EventHandler(this.lblResult_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.label1.Location = new System.Drawing.Point(204, 518);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "GO BACK";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Booking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(285, 544);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.cbTravelMode);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.txtOrigin);
            this.Controls.Add(this.gmapControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Booking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distance Map App";
            this.Load += new System.EventHandler(this.Booking_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private GMapControl gmapControl;
        private TextBox txtOrigin;
        private TextBox txtDestination;
        private ComboBox cbTravelMode;
        private Button btnCalculate;
        private Label lblResult;

        
        private void lblResult_Click(object sender, EventArgs e)
        {

        }

        private void Booking_Load(object sender, EventArgs e)
        {

        }

        private async void cbTravelMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string origin = txtOrigin.Text.Trim();
            string destination = txtDestination.Text.Trim();
            string travelMode = cbTravelMode.SelectedItem.ToString();
            (double distance, double duration, double fare) = await _mapManager.ShowRouteAsync(origin, destination, travelMode);

            if (travelMode == "Car")
            {   
                lblResult.Text = $"Distance: {distance:F2} km\nDuration: {duration / 2:F2} giờ\nFare: {fare * 2:N0} VNĐ";
            }
            else if (travelMode == "Bike")
            {
                lblResult.Text = $"Distance: {distance:F2} km\nDuration: {duration:F2} giờ\nFare: {fare:N0} VNĐ";
            }
        }
        int click_pick = 0;
        int click_drop = 0;
        private void label1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận",
                      MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                click_pick = 0;
                click_drop = 0;
                parentForm.Show();
                this.Close();
            }
            else
            {
                //new Booking(parentForm).Show();
            }
        }

        private void txtOrigin_MouseClick(object sender, MouseEventArgs e)
        {
            if (click_pick == 0)

            {
                txtOrigin.Text = "";
                click_pick++;
            }
        }

        private void txtDestination_MouseClick(object sender, MouseEventArgs e)
        {
            if (click_drop == 0)

            {
                txtDestination.Text = "";
                click_drop++;
            }
        }
    }
}