-- Script generated for database: QuanLyNhanVien
IF DB_ID(N'QuanLyNhanVien') IS NULL
BEGIN
    CREATE DATABASE [QuanLyNhanVien];
END
GO
USE [QuanLyNhanVien];
GO


/* Tạo bảng PhongBan */
IF OBJECT_ID('dbo.PhongBan', 'U') IS NOT NULL DROP TABLE dbo.PhongBan;
GO
CREATE TABLE dbo.PhongBan (
    PhongBanID INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255) NULL,
    DiaChi NVARCHAR(200) NULL
);
GO

-- Dữ liệu mẫu cho phòng ban
INSERT INTO dbo.PhongBan (TenPhong, MoTa, DiaChi) VALUES
(N'Phòng Kế Toán', N'Quản lý sổ sách, thanh toán', N'Tầng 2 - Tòa nhà A'),
(N'Phòng Nhân Sự', N'Tuyển dụng, đào tạo', N'Tầng 3 - Tòa nhà A'),
(N'Phòng Kinh Doanh', N'Bán hàng và chăm sóc khách hàng', N'Tầng 4 - Tòa nhà B'),
(N'Phòng Kỹ Thuật', N'Phát triển sản phẩm, bảo trì', N'Tầng 5 - Tòa nhà B'),
(N'Phòng Hành Chính', N'Quản trị văn phòng', N'Tầng 1 - Tòa nhà A');
GO
