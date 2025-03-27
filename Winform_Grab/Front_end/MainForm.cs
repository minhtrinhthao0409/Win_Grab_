using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_Grab
{
    public partial class MainForm: Form
    {
        private Customer currentCustomer;
        public MainForm(Customer customer)
        {
            InitializeComponent();
            DataManager.InitializeDriverData();
            currentCustomer = customer;
            txtHello.Text = "Hello, " + currentCustomer.Name + ".\n" + " Không chê em nghèo, lên xe em đèo.";
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            Booking booking = new Booking(currentCustomer);
            booking.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowInfor showInfo = new ShowInfor(this, currentCustomer);
            showInfo.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HistoryForm history = new HistoryForm(currentCustomer);
            history.Show();
            this.Hide();
        }
    }
}
