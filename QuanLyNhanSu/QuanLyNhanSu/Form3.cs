using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class Form3 : Form
    {
        private string connectionString = "Server=LAPTOP100TOI\\SQL_PROJECT;Database=QuanLyNhanVien;Trusted_Connection=True;";

        public string MaNV { get; set; }
        public string HoTen { get; set; }

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            txtMaNV.Text = MaNV;
            txtHoTen.Text = HoTen;
            LoadPhongBan();
            txtLuongCB.Text = "0";
            txtPhuCap.Text = "0";
            TinhTong();
        }

        private void LoadPhongBan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT PhongBanID, TenPhong FROM dbo.PhongBan ORDER BY TenPhong";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    DataRow row = dt.NewRow();
                    row["PhongBanID"] = DBNull.Value;
                    row["TenPhong"] = "(Chưa chọn)";
                    dt.Rows.InsertAt(row, 0);

                    cboPhongBan.DataSource = dt;
                    cboPhongBan.DisplayMember = "TenPhong";
                    cboPhongBan.ValueMember = "PhongBanID";
                    cboPhongBan.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load phòng ban: " + ex.Message);
            }
        }

        private void txtLuongCB_TextChanged(object sender, EventArgs e) => TinhTong();
        private void txtPhuCap_TextChanged(object sender, EventArgs e) => TinhTong();

        private void TinhTong()
        {
            if (decimal.TryParse(txtLuongCB.Text.Replace(",", ""), out decimal cb) &&
                decimal.TryParse(txtPhuCap.Text.Replace(",", ""), out decimal pc))
            {
                txtTong.Text = (cb + pc).ToString("N0") + " VND";
            }
            else
            {
                txtTong.Text = "0 VND";
            }
        }

        private void txtLuongCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtPhuCap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtLuongCB.Text, out decimal luongCB) || luongCB <= 0)
            {
                MessageBox.Show("Lương cơ bản phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLuongCB.Focus();
                return;
            }

            if (cboPhongBan.SelectedValue == null || cboPhongBan.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Vui lòng chọn phòng ban!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboPhongBan.Focus();
                return;
            }

            decimal phuCap = decimal.TryParse(txtPhuCap.Text, out decimal pc) ? pc : 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // CẬP NHẬT PHÒNG BAN
                            string updatePB = "UPDATE NhanVien SET PhongBanID = @PhongBanID WHERE MaNV = @MaNV";
                            using (SqlCommand cmd = new SqlCommand(updatePB, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@PhongBanID", cboPhongBan.SelectedValue);
                                cmd.Parameters.AddWithValue("@MaNV", MaNV);
                                cmd.ExecuteNonQuery();
                            }

                            // KIỂM TRA TRÙNG LƯƠNG
                            string checkLuong = "SELECT COUNT(*) FROM Luong WHERE MaNV = @MaNV";
                            using (SqlCommand cmd = new SqlCommand(checkLuong, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaNV", MaNV);
                                int count = (int)cmd.ExecuteScalar();
                                if (count > 0)
                                {
                                    MessageBox.Show("Nhân viên đã có lương! Không thể thêm lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    transaction.Rollback();
                                    return;
                                }
                            }

                            // THÊM LƯƠNG
                            string insertLuong = @"
                                INSERT INTO Luong (MaNV, LuongCoBan, PhuCap)
                                VALUES (@MaNV, @LuongCoBan, @PhuCap)";

                            using (SqlCommand cmd = new SqlCommand(insertLuong, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaNV", MaNV);
                                cmd.Parameters.AddWithValue("@LuongCoBan", luongCB);
                                cmd.Parameters.AddWithValue("@PhuCap", phuCap);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Thiết lập thành công lương & phòng ban!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu: " + ex.Message);
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                MessageBox.Show("Bạn phải nhập đầy đủ lương và phòng ban!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
    }
}
