-- Script generated for database: QuanLyNhanVien
IF DB_ID(N'QuanLyNhanVien') IS NULL
BEGIN
    CREATE DATABASE [QuanLyNhanVien];
END
GO
USE [QuanLyNhanVien];
GO


/* 4. Tổng chi lương hiện tại theo phòng ban (lấy mức lương áp dụng gần nhất cho mỗi nhân viên) */
WITH LatestLuong AS (
    SELECT l1.*
    FROM dbo.Luong l1
    INNER JOIN (
        SELECT NhanVienID, MAX(NgayApDung) AS MaxDate
        FROM dbo.Luong
        GROUP BY NhanVienID
    ) lm ON l1.NhanVienID = lm.NhanVienID AND l1.NgayApDung = lm.MaxDate
)
SELECT pb.PhongBanID, pb.TenPhong, COUNT(nv.NhanVienID) AS SoNhanVien,
       SUM(ISNULL(ll.MucLuong,0)) AS TongChiLuong
FROM dbo.PhongBan pb
LEFT JOIN dbo.NhanVien nv ON pb.PhongBanID = nv.PhongBanID
LEFT JOIN LatestLuong ll ON nv.NhanVienID = ll.NhanVienID
GROUP BY pb.PhongBanID, pb.TenPhong
ORDER BY TongChiLuong DESC;
GO
