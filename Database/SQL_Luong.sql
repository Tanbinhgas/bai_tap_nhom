-- Script generated for database: QuanLyNhanVien
IF DB_ID(N'QuanLyNhanVien') IS NULL
BEGIN
    CREATE DATABASE [QuanLyNhanVien];
END
GO
USE [QuanLyNhanVien];
GO


/* Tạo bảng Luong (ghi nhận lịch sử lương, phụ cấp, ngày áp dụng) */
IF OBJECT_ID('dbo.Luong', 'U') IS NOT NULL DROP TABLE dbo.Luong;
GO
CREATE TABLE dbo.Luong (
    LuongID INT IDENTITY(1,1) PRIMARY KEY,
    NhanVienID INT NOT NULL,
    NgayApDung DATE NOT NULL,
    HeSoLuong DECIMAL(5,2) NOT NULL,
    PhuCap DECIMAL(18,2) NULL,
    MucLuong DECIMAL(18,2) AS (HeSoLuong * 3000000 + ISNULL(PhuCap,0)) PERSISTED, -- công thức giả định
    GhiChu NVARCHAR(255) NULL,
    CONSTRAINT FK_Luong_NhanVien FOREIGN KEY (NhanVienID) REFERENCES dbo.NhanVien(NhanVienID)
);
GO

-- Dữ liệu mẫu lương (lịch sử)
INSERT INTO dbo.Luong (NhanVienID, NgayApDung, HeSoLuong, PhuCap, GhiChu) VALUES
(1001, '2021-06-01', 3.50, 1000000, N'Áp dụng khi tuyển'),
(1001, '2024-01-01', 4.00, 1200000, N'Tăng lương định kỳ'),
(1002, '2019-03-15', 7.00, 2500000, N'Kế toán trưởng'),
(1003, '2022-01-10', 3.00, 800000, N''),
(1004, '2020-08-20', 5.00, 1500000, N''),
(1005, '2018-11-01', 9.00, 3000000, N'Trưởng phòng'),
(1006, '2017-07-01', 4.20, 900000, N''),
(1007, '2015-02-20', 6.00, 2000000, N''),
(1008, '2016-10-05', 3.20, 500000, N''),
(1009, '2023-04-01', 1.20, 0, N'Thực tập'),
(1010, '2010-09-15', 8.00, 2200000, N'');
GO
