using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class fManager : Form
    {
        private string connectionString = "Server=PC100TOI;Database=QuanLyNhanVien;Trusted_Connection=True;";

        public fManager()
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
                        SELECT MaNV, HoTen, GioiTinh, NgaySinh, 
                               DienThoai, Email, DiaChi
                        FROM dbo.NhanVien
                        ORDER BY MaNV";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvNhanVien.DataSource = dt;

                    dgvNhanVien.Columns["MaNV"].HeaderText = "ID";
                    dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên";
                    dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới tính";
                    dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                    dgvNhanVien.Columns["DienThoai"].HeaderText = "Số điện thoại";
                    dgvNhanVien.Columns["Email"].HeaderText = "Email";
                    dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa chỉ";

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
                            ISNULL(pb.TenPhong, N'Chưa phân phòng') AS PhongBan,
                            l.LuongCoBan,
                            ISNULL(l.PhuCap, 0) AS PhuCap,
                            (l.LuongCoBan + ISNULL(l.PhuCap, 0)) AS TongLuong,
                            FORMAT(l.LuongCoBan + ISNULL(l.PhuCap, 0), 'N0') +' ₫' AS TongLuong_VND,
                            FORMAT(l.NgayApDung, 'dd/MM/yyyy') AS NgayApDung,
                            l.GhiChu
                        FROM dbo.Luong l
                        INNER JOIN dbo.NhanVien nv ON l.MaNV = nv.MaNV
                        LEFT JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
                        WHERE l.NgayApDung = (
                            SELECT MAX(NgayApDung)
                            FROM dbo.Luong l2
                            WHERE l2.MaNV = l.MaNV
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
                            COUNT(nv.MaNV) AS SoLuongNhanVien
                        FROM dbo.PhongBan pb
                        LEFT JOIN dbo.NhanVien nv ON pb.PhongBanID = nv.PhongBanID
                        GROUP BY pb.MaPhong, pb.TenPhong
                        ORDER BY pb.MaPhong";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPhongBan.DataSource = dt;

                    // ĐẶT TIÊU ĐỀ CHỈ CHO CÁC CỘT TỒN TẠI
                    dgvPhongBan.Columns["MaPhong"].HeaderText = "Mã phòng";
                    dgvPhongBan.Columns["TenPhong"].HeaderText = "Tên phòng";
                    dgvPhongBan.Columns["SoLuongNhanVien"].HeaderText = "Số NV";
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

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            // KIỂM TRA HỌ TÊN
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // TỰ ĐỘNG TẠO MÃ NV
                    string maNV = txtID.Text.Trim();
                    if (string.IsNullOrEmpty(maNV))
                        maNV = "NV" + DateTime.Now.ToString("yyMMddHHmmss");

                    string query = @"
                        INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, Email, DiaChi, DienThoai)
                        VALUES (@MaNV, @HoTen, @NgaySinh, @GioiTinh, @Email, @DiaChi, @DienThoai)";  // ĐÃ BỎ , 1

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", radNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@DienThoai", txtPhone.Text.Trim());

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show($"Thêm thành công! Mã NV: {maNV}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVienFromDB(); // CẬP NHẬT LẠI BẢNG
                    ClearInput(); // XÓA FORM
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ViewState.MaNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        UPDATE NhanVien SET
                            MaNV = @MaNV,
                            HoTen = @HoTen,
                            NgaySinh = @NgaySinh,
                            GioiTinh = @GioiTinh,
                            Email = @Email,
                            DiaChi = @DiaChi,
                            DienThoai = @DienThoai
                        WHERE MaNV = @MaNVCu";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNV", txtID.Text.Trim());
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", radNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@DienThoai", txtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaNVCu", ViewState.MaNV);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Sửa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNhanVienFromDB();
                        ClearInput();
                        ViewState.MaNV = "";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên để sửa!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ViewState.MaNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn xóa nhân viên:\n{ViewState.MaNV}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                // XÓA LƯƠNG TRƯỚC (nếu có)
                                string deleteLuong = "DELETE FROM Luong WHERE MaNV = @MaNV";
                                using (SqlCommand cmd = new SqlCommand(deleteLuong, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MaNV", ViewState.MaNV);
                                    cmd.ExecuteNonQuery();
                                }

                                // XÓA NHÂN VIÊN
                                string deleteNV = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                                using (SqlCommand cmd = new SqlCommand(deleteNV, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MaNV", ViewState.MaNV);
                                    cmd.ExecuteNonQuery();
                                }

                                transaction.Commit();
                                MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadNhanVienFromDB();
                                ClearInput();
                                ViewState.MaNV = "";
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
                    MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                // LẤY MaNV DƯỚI DẠNG CHUỖI
                string maNV = row.Cells["MaNV"].Value?.ToString() ?? "";
                ViewState.MaNV = maNV; // GÁN CHUỖI

                txtID.Text = maNV;
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";

                // XỬ LÝ NGÀY SINH AN TOÀN
                if (row.Cells["NgaySinh"].Value is DBNull || row.Cells["NgaySinh"].Value == null)
                    dtpNgaySinh.Value = DateTime.Now;
                else
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);

                // XỬ LÝ GIỚI TÍNH
                string gioiTinh = row.Cells["GioiTinh"].Value?.ToString() ?? "Nam";
                radNam.Checked = (gioiTinh == "Nam");
                radNu.Checked = (gioiTinh == "Nữ");

                txtPhone.Text = row.Cells["DienThoai"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
            }
        }

        public static class ViewState
        {
            public static string MaNV { get; set; } = "";
        }

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e) { }
        private void lblPhone_Click(object sender, EventArgs e) { }
        private void cboSapXep_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dgvLuong_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvLuong_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }

        private void radNu_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
