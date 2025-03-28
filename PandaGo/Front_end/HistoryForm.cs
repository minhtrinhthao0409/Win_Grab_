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
    public partial class HistoryForm: Form
    {
        private Customer currentCustomer;
        public HistoryForm(Customer customer)
        {
            InitializeComponent();
            this.currentCustomer = customer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(currentCustomer);
            mainForm.Show();
            this.Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            try
            {
                TripHistory history = new TripHistory(currentCustomer);
                richTextBox1.Text = history.PrintTripHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không thể tải lịch sử: {ex.Message}");
            }
        }
    }
}
