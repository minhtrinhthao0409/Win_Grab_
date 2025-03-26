using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;
using System.IO;

namespace Winform_Grab
{
    
    public partial class frmLogin: Form
    {

        private string jsonFilePath = "customers.json";
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string phoneNumber = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ số điện thoại và mật khẩu!",
                                "Đăng nhập thất bại",
                                MessageBoxButtons.OK);
                return;
            }

            // Đọc danh sách khách hàng từ file JSON
            List<Customer> customers;
            string jsonContent = File.ReadAllText(jsonFilePath);
            customers = JsonSerializer.Deserialize<List<Customer>>(jsonContent);

            // Kiểm tra đăng nhập
            Customer matchedCustomer = null;
            bool phoneNumberExists = false;

            foreach (Customer c in customers)
            {
                if (c.PhoneNumber == phoneNumber)
                {
                    phoneNumberExists = true; // Đánh dấu số điện thoại tồn tại
                    if (c.Password == password)
                    {
                        matchedCustomer = c; // Tìm thấy khách hàng khớp
                        break;
                    }
                }
            }
            // Xử lý kết quả sau khi kiểm tra hết danh sách
            if (matchedCustomer != null)
            {
                //MessageBox.Show("Đăng nhập thành công",
                //                "Đăng nhập thành công",
                //                MessageBoxButtons.OK);
                MainForm mainForm = new MainForm(matchedCustomer);
                mainForm.Show();
                this.Hide();
            }
            else if (phoneNumberExists)
            {
                MessageBox.Show("Sai mật khẩu. Xin vui lòng thử lại",
                                "Đăng nhập thất bại",
                                MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("SĐT này chưa đăng ký",
                                "Đăng nhập thất bại",
                                MessageBoxButtons.OK);
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            
            new frmRegister().Show();
            this.Hide();

        }

        private void checkboxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxShowPas.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else txtPassword.UseSystemPasswordChar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtUsername.Clear();
            checkboxShowPas.Checked = false;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
