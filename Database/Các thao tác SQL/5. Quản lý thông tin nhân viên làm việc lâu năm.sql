-- Script generated for database: QuanLyNhanVien
IF DB_ID(N'QuanLyNhanVien') IS NULL
BEGIN
    CREATE DATABASE [QuanLyNhanVien];
END
GO
USE [QuanLyNhanVien];
GO


/* 5. Danh sách nhân viên làm việc lâu năm (>= 5 năm) */
DECLARE @soNam INT = 5;
SELECT nv.NhanVienID, nv.HoTen, nv.NgayVaoLam, DATEDIFF(YEAR, nv.NgayVaoLam, GETDATE()) AS NamKinhNghiem, pb.TenPhong
FROM dbo.NhanVien nv
LEFT JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
WHERE DATEDIFF(YEAR, nv.NgayVaoLam, GETDATE()) >= @soNam
ORDER BY NamKinhNghiem DESC;
GO
