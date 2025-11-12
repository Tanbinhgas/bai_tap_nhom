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
        private string connectionString = "Server=LAPTOP100TOI\\SQL_PROJECT;Database=QuanLyNhanVien;Trusted_Connection=True;";

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
                    string query = "SELECT NhanVienID, HoTen, NgaySinh, GioiTinh, Email, DiaChi, DienThoai FROM NhanVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvNhanVien.DataSource = dt;

                    dgvNhanVien.Columns["NhanVienID"].HeaderText = "Mã NV";
                    dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên";
                    dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                    dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới tính";
                    dgvNhanVien.Columns["Email"].HeaderText = "Email";
                    dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa chỉ";
                    dgvNhanVien.Columns["DienThoai"].HeaderText = "Điện thoại";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void LoadPhongBanFromDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // DÙNG TRỰC TIẾP BẢNG NhanVien + PhongBan (KHÔNG CẦN VIEW)
                    string query = @"
                        SELECT 
                            pb.PhongBanID,
                            pb.TenPhong AS TenPhongBan,
                            COUNT(nv.NhanVienID) AS SoLuongNhanVien
                        FROM PhongBan pb
                        LEFT JOIN NhanVien nv ON pb.PhongBanID = nv.PhongBanID
                        GROUP BY pb.PhongBanID, pb.TenPhong";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPhongBan.DataSource = dt;

                    dgvPhongBan.Columns["TenPhongBan"].HeaderText = "Tên phòng ban";
                    dgvPhongBan.Columns["SoLuongNhanVien"].HeaderText = "Số lượng NV";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load phòng ban: " + ex.Message);
            }
        }

        private void LoadLuongFromDB(int nhanVienID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT l.LuongID, l.NgayApDung, l.HeSoLuong, l.PhuCap, l.GhiChu
                FROM Luong l
                WHERE l.NhanVienID = @ID
                ORDER BY l.NgayApDung DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ID", nhanVienID);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvLuong.DataSource = dt;  // ĐÚNG TÊN
                }
            }
            catch { dgvLuong.DataSource = null; }
        }

        private void LoadHopDongFromDB(int nhanVienID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT hd.HopDongID, hd.NgayKy, hd.NgayHet, hd.LoaiHopDong, hd.LuongCoBan
                FROM HopDong hd
                WHERE hd.NhanVienID = @ID";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ID", nhanVienID);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvHopDong.DataSource = dt;  // ĐÚNG TÊN
                }
            }
            catch { dgvHopDong.DataSource = null; }
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

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                    txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                    radNam.Checked = row.Cells["GioiTinh"].Value?.ToString() == "Nam";
                    txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                    txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
                    txtPhone.Text = row.Cells["DienThoai"].Value?.ToString() ?? "";

                    int id = Convert.ToInt32(row.Cells["NhanVienID"].Value);
                    ViewState.NhanVienID = id;

                    // GỌI 2 HÀM NÀY ĐỂ HIỂN THỊ LƯƠNG + HỢP ĐỒNG
                    LoadLuongFromDB(id);
                    LoadHopDongFromDB(id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi chọn nhân viên: " + ex.Message);
                }
            }
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
            if (ViewState.NhanVienID == 0) { MessageBox.Show("Chọn nhân viên!"); return; }
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
                    cmd.Parameters.AddWithValue("@GioiTTinh", radNam.Checked ? "Nam" : "Nữ");
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
            if (ViewState.NhanVienID == 0) { MessageBox.Show("Chọn nhân viên!"); return; }
            if (MessageBox.Show("Xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                    string query = "SELECT NhanVienID, HoTen, NgaySinh, GioiTinh, Email, DiaChi, DienThoai " +
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
    }
}
