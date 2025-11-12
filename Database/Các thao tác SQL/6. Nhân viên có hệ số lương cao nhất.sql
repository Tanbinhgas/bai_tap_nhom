-- Script generated for database: QuanLyNhanVien
IF DB_ID(N'QuanLyNhanVien') IS NULL
BEGIN
    CREATE DATABASE [QuanLyNhanVien];
END
GO
USE [QuanLyNhanVien];
GO


/* 6. Nhân viên có hệ số lương cao nhất (dựa trên bản ghi lương mới nhất) */
SELECT nv.NhanVienID, nv.HoTen, l.HeSoLuong, l.PhuCap, l.HeSoLuong
FROM dbo.NhanVien nv
JOIN dbo.Luong l ON nv.NhanVienID = l.NhanVienID
WHERE l.NgayApDung = (
    SELECT MAX(NgayApDung) FROM dbo.Luong l2 WHERE l2.NhanVienID = nv.NhanVienID
)
AND l.HeSoLuong = (
    SELECT MAX(l3.HeSoLuong) FROM dbo.Luong l3
    WHERE l3.NgayApDung = (
        SELECT MAX(NgayApDung) FROM dbo.Luong l4 WHERE l4.NhanVienID = l3.NhanVienID
    )
);
GO
