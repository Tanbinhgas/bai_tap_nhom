USE [QuanLyNhanVien];
GO
/* 3. Tìm kiếm nhân viên theo từ khóa: tên, giới tính hoặc phòng ban */
DECLARE 
    @tu_khoa   NVARCHAR(100) = N'Nguyễn',  -- Tìm trong họ tên
    @gioitinh  NVARCHAR(10)  = N'Nam',     -- NULL = bỏ qua
    @phongban  NVARCHAR(100) = NULL;       -- NULL = bỏ qua, hoặc N'Kinh Doanh'

SELECT 
    nv.MaNV,
    nv.HoTen,
    nv.GioiTinh,
    pb.MaPhong,
    pb.TenPhong,
    FORMAT(l.LuongCoBan + ISNULL(l.PhuCap, 0), 'N0') + ' ₫' AS TongLuong
FROM dbo.NhanVien nv
LEFT JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
LEFT JOIN dbo.Luong l ON nv.MaNV = l.MaNV
WHERE 
    (@tu_khoa IS NULL OR nv.HoTen LIKE N'%' + @tu_khoa + N'%')
    AND (@gioitinh IS NULL OR nv.GioiTinh = @gioitinh)
    AND (@phongban IS NULL OR pb.TenPhong LIKE N'%' + @phongban + N'%')  -- Linh hoạt hơn
ORDER BY nv.HoTen;
GO