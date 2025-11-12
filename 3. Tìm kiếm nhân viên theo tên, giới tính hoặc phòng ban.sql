-- Script generated for database: QuanLyNhanVien
IF DB_ID(N'QuanLyNhanVien') IS NULL
BEGIN
    CREATE DATABASE [QuanLyNhanVien];
END
GO
USE [QuanLyNhanVien];
GO


/* 3. Tìm kiếm nhân viên theo từ khoá: tên, giới tính hoặc phòng ban (ví dụ dùng biến) */
-- Thay giá trị trong WHERE theo nhu cầu
DECLARE @tu_khoa NVARCHAR(100) = N'Nguyễn'; -- tìm theo tên
DECLARE @gioitinh NVARCHAR(10) = N'Nam';    -- hoặc NULL nếu bỏ điều kiện
DECLARE @phongban NVARCHAR(100) = NULL;     -- hoặc N'Phòng Kế Toán'

SELECT nv.NhanVienID, nv.HoTen, nv.GioiTinh, pb.TenPhong, nv.ChucVu, nv.NgayVaoLam
FROM dbo.NhanVien nv
LEFT JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
WHERE (@tu_khoa IS NULL OR nv.HoTen LIKE '%' + @tu_khoa + '%')
  AND (@gioitinh IS NULL OR nv.GioiTinh = @gioitinh)
  AND (@phongban IS NULL OR pb.TenPhong = @phongban);
GO
