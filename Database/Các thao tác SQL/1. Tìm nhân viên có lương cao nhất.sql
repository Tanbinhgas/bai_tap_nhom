-- Script generated for database: QuanLyNhanVien
IF DB_ID(N'QuanLyNhanVien') IS NULL
BEGIN
    CREATE DATABASE [QuanLyNhanVien];
END
GO
USE [QuanLyNhanVien];
GO


/* 1. Tìm nhân viên có MucLuong cao nhất (lấy bản ghi lương mới nhất cho mỗi nhân viên) */
SELECT TOP 1 nv.NhanVienID, nv.HoTen, l.MucLuong, l.NgayApDung
FROM dbo.NhanVien nv
JOIN dbo.Luong l ON nv.NhanVienID = l.NhanVienID
WHERE l.NgayApDung = (
    SELECT MAX(NgayApDung) FROM dbo.Luong l2 WHERE l2.NhanVienID = nv.NhanVienID
)
ORDER BY l.MucLuong DESC;
GO
