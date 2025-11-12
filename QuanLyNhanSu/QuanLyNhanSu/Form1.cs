using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyNhanSu
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=PC100TOI;Database=QuanLyNhanVien;Trusted_Connection=True;";

        public Form1()
        {
            InitializeComponent();
            LoadNhanVienFromDB();
            LoadPhongBanFromDB();
        }

        private void LoadNhanVienFromDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT NhanVienID, MaNV, HoTen, NgaySinh, GioiTinh, 
                               DienThoai, Email, DiaChi, ChucVu
                        FROM NhanVien
                        ORDER BY NhanVienID";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvNhanVien.DataSource = dt;

                    // Đặt tiêu đề cột
                    dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
                    dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên";
                    dgvNhanVien.Columns["ChucVu"].HeaderText = "Chức vụ";
                    dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load nhân viên: " + ex.Message);
            }
        }

        private void LoadAllLuongFromDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            nv.MaNV,
                            nv.HoTen,
                            pb.TenPhong AS PhongBan,
                            l.LuongCoBan,
                            ISNULL(l.PhuCap, 0) AS PhuCap,
                            (l.LuongCoBan + ISNULL(l.PhuCap, 0)) AS TongLuong,
                            FORMAT(l.LuongCoBan + ISNULL(l.PhuCap, 0), 'N0') + ' ₫' AS TongLuong_VND,
                            FORMAT(l.NgayApDung, 'dd/MM/yyyy') AS NgayApDung,
                            l.GhiChu
                        FROM dbo.Luong l
                        INNER JOIN dbo.NhanVien nv ON l.NhanVienID = nv.NhanVienID
                        INNER JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
                        WHERE nv.TrangThai = 1
                          AND l.NgayApDung = (
                              SELECT MAX(NgayApDung) 
                              FROM dbo.Luong l2 
                              WHERE l2.NhanVienID = l.NhanVienID
                          )
                        ORDER BY TongLuong DESC, nv.HoTen";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvLuong.DataSource = dt;

                    // ĐẸP CỘT
                    dgvLuong.Columns["MaNV"].HeaderText = "Mã NV";
                    dgvLuong.Columns["HoTen"].HeaderText = "Họ tên";
                    dgvLuong.Columns["PhongBan"].HeaderText = "Phòng ban";
                    dgvLuong.Columns["LuongCoBan"].HeaderText = "Lương CB";
                    dgvLuong.Columns["PhuCap"].HeaderText = "Phụ cấp";
                    dgvLuong.Columns["TongLuong_VND"].HeaderText = "Tổng lương";
                    dgvLuong.Columns["NgayApDung"].HeaderText = "Ngày áp dụng";
                    dgvLuong.Columns["GhiChu"].HeaderText = "Ghi chú";

                    // Ẩn cột số
                    dgvLuong.Columns["TongLuong"].Visible = false;

                    // Định dạng tiền
                    dgvLuong.Columns["LuongCoBan"].DefaultCellStyle.Format = "N0";
                    dgvLuong.Columns["PhuCap"].DefaultCellStyle.Format = "N0";
                    dgvLuong.Columns["LuongCoBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvLuong.Columns["PhuCap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgvLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load lương: " + ex.Message);
                dgvLuong.DataSource = null;
            }
        }

        private void LoadPhongBanFromDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            pb.MaPhong,
                            pb.TenPhong,
                            pb.MoTa,
                            COUNT(nv.NhanVienID) AS SoLuongNhanVien,
                            CASE WHEN pb.TrangThai = 1 THEN N'Hoạt động' ELSE N'Ngừng' END AS TrangThai
                        FROM PhongBan pb
                        LEFT JOIN NhanVien nv ON pb.PhongBanID = nv.PhongBanID
                        GROUP BY pb.MaPhong, pb.TenPhong, pb.MoTa, pb.TrangThai";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPhongBan.DataSource = dt;

                    dgvPhongBan.Columns["MaPhong"].HeaderText = "Mã phòng";
                    dgvPhongBan.Columns["TenPhong"].HeaderText = "Tên phòng";
                    dgvPhongBan.Columns["SoLuongNhanVien"].HeaderText = "Số NV";
                    dgvPhongBan.Columns["TrangThai"].HeaderText = "Trạng thái";
                    dgvPhongBan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load phòng ban: " + ex.Message);
            }
        }
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabLuong)
            {
                LoadAllLuongFromDB();
            }
        }

        private void ClearInput()
        {
            txtHoTen.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtPhone.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            radNam.Checked = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, Email, DiaChi, DienThoai) " +
                                   "VALUES (@HoTen, @NgaySinh, @GioiTinh, @Email, @DiaChi, @DienThoai)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", radNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@DienThoai", txtPhone.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!");
                    LoadNhanVienFromDB();
                    ClearInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (ViewState.NhanVienID == 0)
            {
                MessageBox.Show("Chọn nhân viên cần sửa!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE NhanVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, " +
                                   "Email=@Email, DiaChi=@DiaChi, DienThoai=@DienThoai WHERE NhanVienID=@ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", ViewState.NhanVienID);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", radNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@DienThoai", txtPhone.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thành công!");
                    LoadNhanVienFromDB();
                    ClearInput();
                    ViewState.NhanVienID = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (ViewState.NhanVienID == 0)
            {
                MessageBox.Show("Chọn nhân viên cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM NhanVien WHERE NhanVienID=@ID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", ViewState.NhanVienID);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa thành công!");
                        LoadNhanVienFromDB();
                        ClearInput();
                        ViewState.NhanVienID = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT NhanVienID, MaNV, HoTen, NgaySinh, GioiTinh, Email, DiaChi, DienThoai " +
                                   "FROM NhanVien WHERE HoTen LIKE @k OR Email LIKE @k OR DienThoai LIKE @k";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@k", "%" + txtTimKiem.Text + "%");
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvNhanVien.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        public static class ViewState
        {
            public static int NhanVienID { get; set; } = 0;
        }

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e) { }
        private void lblPhone_Click(object sender, EventArgs e) { }
        private void cboSapXep_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dgvLuong_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvLuong_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }
    }
}
