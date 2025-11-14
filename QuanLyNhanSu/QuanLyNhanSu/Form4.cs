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
    public partial class fTimKiem : Form
    {
        private string connectionString = "Server=LAPTOP100TOI\\SQL_PROJECT;Database=QuanLyNhanVien;Trusted_Connection=True;";
        public DataTable KetQuaTimKiem { get; private set; } = new DataTable();

        public fTimKiem()
        {
            InitializeComponent();
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
                    row["TenPhong"] = "(Tất cả phòng ban)";
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

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT
                            nv.MaNV, nv.HoTen, ISNULL(pb.TenPhong, 'Chưa phân') AS PhongBan,
                            ISNULL(l.LuongCoBan, 0) AS LuongCoBan,
                            ISNULL(l.PhuCap, 0) AS PhuCap,
                            ISNULL(l.LuongCoBan, 0) + ISNULL(l.PhuCap, 0) AS TongLuong
                        FROM NhanVien nv
                        LEFT JOIN PhongBan pb ON nv.PhongBanID = pb.PhongBanID
                        LEFT JOIN Luong l ON nv.MaNV = l.MaNV
                        WHERE 1=1";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    StringBuilder where = new StringBuilder();

                    // Mã NV
                    if (!string.IsNullOrWhiteSpace(txtMaNV.Text))
                    {
                        where.Append(" AND nv.MaNV LIKE @MaNV");
                        cmd.Parameters.AddWithValue("@MaNV", "%" + txtMaNV.Text.Trim() + "%");
                    }

                    // Họ tên
                    if (!string.IsNullOrWhiteSpace(txtHoTen.Text))
                    {
                        where.Append(" AND nv.HoTen LIKE @HoTen");
                        cmd.Parameters.AddWithValue("@HoTen", "%" + txtHoTen.Text.Trim() + "%");
                    }

                    // Phòng ban
                    if (cboPhongBan.SelectedValue != null && cboPhongBan.SelectedValue != DBNull.Value)
                    {
                        where.Append(" AND nv.PhongBanID = @PhongBanID");
                        cmd.Parameters.AddWithValue("@PhongBanID", cboPhongBan.SelectedValue);
                    }

                    // === SỬA TẠI ĐÂY: DÙNG IF-ELSE ===
                    string cotLuong = "ISNULL(l.LuongCoBan, 0)";
                    string selected = cboLoaiLuong.SelectedItem?.ToString();

                    if (selected == "Lương CB")
                        cotLuong = "ISNULL(l.LuongCoBan, 0)";
                    else if (selected == "Phụ cấp")
                        cotLuong = "ISNULL(l.PhuCap, 0)";
                    else if (selected == "Tổng")
                        cotLuong = "ISNULL(l.LuongCoBan, 0) + ISNULL(l.PhuCap, 0)";

                    if (decimal.TryParse(txtTu.Text, out decimal tu))
                    {
                        where.Append($" AND {cotLuong} >= @Tu");
                        cmd.Parameters.AddWithValue("@Tu", tu);
                    }

                    if (decimal.TryParse(txtDen.Text, out decimal den))
                    {
                        where.Append($" AND {cotLuong} <= @Den");
                        cmd.Parameters.AddWithValue("@Den", den);
                    }

                    cmd.CommandText += where.ToString();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    conn.Open();
                    adapter.Fill(dt);
                    conn.Close();

                    KetQuaTimKiem = dt;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void fTimKiem_Load_1(object sender, EventArgs e)
        {
            LoadPhongBan();
            cboLoaiLuong.Items.AddRange(new[] { "Lương CB", "Phụ cấp", "Tổng" });
            cboLoaiLuong.SelectedIndex = 0;
        }

        private void txtTu_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtDen_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
