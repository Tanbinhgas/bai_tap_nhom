-- Script generated for database: QuanLyNhanVien
IF DB_ID(N'QuanLyNhanVien') IS NULL
BEGIN
    CREATE DATABASE [QuanLyNhanVien];
END
GO
USE [QuanLyNhanVien];
GO


/* Tạo bảng HopDong */
IF OBJECT_ID('dbo.HopDong', 'U') IS NOT NULL DROP TABLE dbo.HopDong;
GO
CREATE TABLE dbo.HopDong (
    HopDongID INT IDENTITY(1,1) PRIMARY KEY,
    NhanVienID INT NOT NULL,
    LoaiHopDong NVARCHAR(50) NOT NULL, -- Hợp đồng xác định thời hạn / không xác định thời hạn / thử việc
    NgayKy DATE NOT NULL,
    NgayHetHan DATE NULL,
    LuongCoBan DECIMAL(18,2) NULL,
    GhiChu NVARCHAR(255) NULL,
    CONSTRAINT FK_HopDong_NhanVien FOREIGN KEY (NhanVienID) REFERENCES dbo.NhanVien(NhanVienID)
);
GO

-- Dữ liệu mẫu hợp đồng
INSERT INTO dbo.HopDong (NhanVienID, LoaiHopDong, NgayKy, NgayHetHan, LuongCoBan, GhiChu) VALUES
(1001, N'Hợp đồng xác định thời hạn', '2021-06-01', '2024-06-01', 12000000, N'---'),
(1002, N'Hợp đồng không xác định thời hạn', '2019-03-15', NULL, 22000000, N'Kế toán trưởng'),
(1003, N'Hợp đồng xác định thời hạn', '2022-01-10', '2024-01-10', 9000000, N'---'),
(1004, N'Hợp đồng không xác định thời hạn', '2020-08-20', NULL, 15000000, N'Nhân viên KPI tốt'),
(1005, N'Hợp đồng không xác định thời hạn', '2018-11-01', NULL, 30000000, N'Trưởng phòng'),
(1006, N'Hợp đồng không xác định thời hạn', '2017-07-01', NULL, 14000000, N''),
(1007, N'Hợp đồng không xác định thời hạn', '2015-02-20', NULL, 18000000, N'Kế toán cao cấp'),
(1008, N'Hợp đồng không xác định thời hạn', '2016-10-05', NULL, 11000000, N''),
(1009, N'Hợp đồng xác định thời hạn', '2023-04-01', '2024-04-01', 4500000, N'Thực tập'),
(1010, N'Hợp đồng không xác định thời hạn', '2010-09-15', NULL, 25000000, N'');
GO
