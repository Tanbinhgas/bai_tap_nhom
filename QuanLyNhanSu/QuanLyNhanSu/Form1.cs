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
        private string connectionString = "Server=LAPTOP100TOI\\SQL_PROJECT;Database=QuanLyNhanVien;Trusted_Connection=True;";

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
                            FORMAT(l.LuongCoBan + ISNULL(l.PhuCap, 0), 'N0') + ' VND' AS TongLuong_VND
                        FROM dbo.Luong l
                        INNER JOIN dbo.NhanVien nv ON l.MaNV = nv.MaNV
                        LEFT JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
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
                    string maNV = txtID.Text.Trim();

                    // TỰ ĐỘNG TẠO MÃ NV ĐÚNG ĐỊNH DẠNG NV1xxx
                    if (string.IsNullOrEmpty(maNV))
                    {
                        string queryMax = "SELECT MAX(CAST(SUBSTRING(MaNV, 4, 4) AS INT)) FROM NhanVien WHERE MaNV LIKE 'NV1[0-9][0-9][0-9]'";
                        using (SqlCommand cmdMax = new SqlCommand(queryMax, conn))
                        {
                            conn.Open();
                            object result = cmdMax.ExecuteScalar();
                            int nextId = (result == DBNull.Value) ? 1001 : (int)result + 1;
                            maNV = "NV" + nextId.ToString("D4");
                            conn.Close();
                        }
                    }

                    // KIỂM TRA TRÙNG MÃ NV
                    string checkExist = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV";
                    using (SqlCommand cmdCheck = new SqlCommand(checkExist, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaNV", maNV);
                        conn.Open();
                        int count = (int)cmdCheck.ExecuteScalar();
                        conn.Close();
                        if (count > 0)
                        {
                            MessageBox.Show($"Mã NV {maNV} đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // THÊM NHÂN VIÊN
                    string query = @"
                        INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, Email, DiaChi, DienThoai)
                        VALUES (@MaNV, @HoTen, @NgaySinh, @GioiTinh, @Email, @DiaChi, @DienThoai)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", radNam.Checked ? "Nam" : "Nữ");
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                        cmd.Parameters.AddWithValue("@DienThoai", txtPhone.Text.Trim());

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    MessageBox.Show($"Thêm thành công! Mã NV: {maNV}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // MỞ FORM3 ĐỂ NHẬP LƯƠNG & PHÒNG BAN
                    using (Form3 frm = new Form3())
                    {
                        frm.MaNV = maNV;
                        frm.HoTen = txtHoTen.Text.Trim();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            LoadNhanVienFromDB();
                            LoadAllLuongFromDB();
                            LoadPhongBanFromDB();
                        }
                        // Form3 sẽ ngăn đóng nếu chưa nhập đủ
                    }

                    ClearInput(); // XÓA FORM CHỈ SAU KHI XONG
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

        private void fManager_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (fTimKiem frm = new fTimKiem())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.KetQuaTimKiem.Rows.Count > 0)
                    {
                        // HIỂN THỊ KẾT QUẢ TRONG DATAGRIDVIEW
                        dgvNhanVien.DataSource = frm.KetQuaTimKiem;

                        // THÔNG BÁO
                        MessageBox.Show(
                            $"Tìm thấy {frm.KetQuaTimKiem.Rows.Count} nhân viên phù hợp!",
                            "Kết quả tìm kiếm",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        // KHÔNG CÓ KẾT QUẢ
                        dgvNhanVien.DataSource = null;
                        MessageBox.Show(
                            "Không tìm thấy nhân viên nào phù hợp với điều kiện!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }
            }
        }
    }
}
