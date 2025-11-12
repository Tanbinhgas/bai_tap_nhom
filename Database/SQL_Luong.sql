-- Script generated for database: QuanLyNhanVien
IF DB_ID('QuanLyNhanVien') IS NULL
BEGIN
    CREATE DATABASE [QuanLyNhanVien]
END
GO

USE [QuanLyNhanVien]
GO

/* Tạo bảng Luong (ghi nhận lịch sử lương, phụ cấp, ngày áp dụng) */
IF OBJECT_ID('dbo.Luong', 'U') IS NOT NULL DROP TABLE dbo.Luong
GO

CREATE TABLE dbo.Luong (
    LuongID INT IDENTITY(1,1) PRIMARY KEY,
    NhanVienID INT NOT NULL,
    NgayApDung DATE NOT NULL,
    HeSoLuong DECIMAL(5,2) NOT NULL,
    PhuCap DECIMAL(18,2) NULL,
    GhiChu NVARCHAR(255) NULL,
    CONSTRAINT FK_Luong_NhanVien FOREIGN KEY (NhanVienID) REFERENCES dbo.NhanVien(NhanVienID)
)
GO

-- Dữ liệu mẫu lương (lịch sử)
INSERT INTO dbo.Luong (NhanVienID, NgayApDung, HeSoLuong, PhuCap, GhiChu) VALUES
(1001, '2021-06-01', 3.50, 1000000, N'Ấn định khi tuyển'),
(1001, '2023-01-01', 4.00, 1200000, N'Tăng lương định kỳ')
GO