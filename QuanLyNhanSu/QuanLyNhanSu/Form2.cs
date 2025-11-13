using QuanLyNhanSu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class flogin : Form
    {
        public flogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // BẮT BUỘC NHẬP USERNAME HOẶC PASSWORD
            if (string.IsNullOrWhiteSpace(textUsername.Text) && string.IsNullOrWhiteSpace(textPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập hoặc mật khẩu!",
                                "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // GIẢ LẬP ĐĂNG NHẬP THÀNH CÔNG
            this.Hide();
            using (fManager f = new fManager())
            {
                f.ShowDialog(); // Mở fManager
            }
            this.Show(); // Hiện lại form login
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình?", "Thoát",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void flogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                btnExit_Click(sender, e);
            }
        }

        private void flogin_Load(object sender, EventArgs e)
        {
            textPassword.UseSystemPasswordChar = true; // Ẩn mật khẩu
        }
    }
}