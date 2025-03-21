using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_Grab
{
    public partial class Loading: Form
    {
        private MainForm parentForm;
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        //private void timer1_Tick(object sender, EventArgs e)
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
                MessageBox.Show("Đã tìm thấy tài xế!");
            }
        }
        
        private void brnCancel_Click(object sender, EventArgs e)
        {

            timer1.Stop();
            DialogResult result = MessageBox.Show("Bạn có chắc muốn hủy chuyến?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
                new Booking(parentForm).Show();
            }
            else timer1.Start();
        }
    }
}
