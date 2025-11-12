-- Script generated for database: QuanLyNhanVien
IF DB_ID(N'QuanLyNhanVien') IS NULL
BEGIN
    CREATE DATABASE [QuanLyNhanVien];
END
GO
USE [QuanLyNhanVien];
GO


/* 8. Ví dụ: Cập nhật tăng lương cho 1 phòng ban (tăng HeSoLuong +0.2 cho các NV thuộc phòng) và ghi nhật ký thay đổi vào bảng Luong */
BEGIN TRANSACTION;
DECLARE @PhongBanID INT = 4; -- ví dụ: Phòng Kỹ Thuật
DECLARE @TangHeSo DECIMAL(5,2) = 0.20;
DECLARE @NgayApDung DATE = GETDATE();

-- Lấy danh sách nhân viên hiện tại trong phòng ban
DECLARE @tbl NVARCHAR(100) = N'temp_Lookup';
-- Thêm bản ghi lương mới cho từng nhân viên
INSERT INTO dbo.Luong (NhanVienID, NgayApDung, HeSoLuong, PhuCap, GhiChu)
SELECT nv.NhanVienID, @NgayApDung, ISNULL(l2.HeSoLuong, 3.00) + @TangHeSo, ISNULL(l2.PhuCap,0), N'Tăng lương theo đợt phòng'
FROM dbo.NhanVien nv
LEFT JOIN (
    SELECT l1.*
    FROM dbo.Luong l1
    INNER JOIN (
        SELECT NhanVienID, MAX(NgayApDung) AS MaxDate
        FROM dbo.Luong
        GROUP BY NhanVienID
    ) lm ON l1.NhanVienID = lm.NhanVienID AND l1.NgayApDung = lm.MaxDate
) l2 ON nv.NhanVienID = l2.NhanVienID
WHERE nv.PhongBanID = @PhongBanID;

COMMIT TRANSACTION;
GO

-- Kiểm tra kết quả (lấy bản lương mới nhất của phòng)
WITH LatestLuong AS (
    SELECT l1.*
    FROM dbo.Luong l1
    INNER JOIN (
        SELECT NhanVienID, MAX(NgayApDung) AS MaxDate
        FROM dbo.Luong
        GROUP BY NhanVienID
    ) lm ON l1.NhanVienID = lm.NhanVienID AND l1.NgayApDung = lm.MaxDate
)
SELECT nv.NhanVienID, nv.HoTen, nv.PhongBanID, pb.TenPhong, ll.HeSoLuong, ll.MucLuong, ll.NgayApDung
FROM dbo.NhanVien nv
JOIN LatestLuong ll ON nv.NhanVienID = ll.NhanVienID
LEFT JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
WHERE nv.PhongBanID = @PhongBanID;
GO
