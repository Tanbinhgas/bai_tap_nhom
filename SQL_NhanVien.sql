-- Script generated for database: QuanLyNhanVien
IF DB_ID(N'QuanLyNhanVien') IS NULL
BEGIN
    CREATE DATABASE [QuanLyNhanVien];
END
GO
USE [QuanLyNhanVien];
GO


/* Tạo bảng NhanVien */
IF OBJECT_ID('dbo.NhanVien', 'U') IS NOT NULL DROP TABLE dbo.NhanVien;
GO
CREATE TABLE dbo.NhanVien (
    NhanVienID INT IDENTITY(1001,1) PRIMARY KEY,
    HoTen NVARCHAR(150) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(10) NULL,
    DienThoai VARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    DiaChi NVARCHAR(255) NULL,
    NgayVaoLam DATE NULL,
    PhongBanID INT NULL,
    ChucVu NVARCHAR(100) NULL,
    CONSTRAINT FK_NhanVien_PhongBan FOREIGN KEY (PhongBanID) REFERENCES dbo.PhongBan(PhongBanID)
);
GO

-- Dữ liệu mẫu nhân viên (giả lập "khét")
INSERT INTO dbo.NhanVien (HoTen, NgaySinh, GioiTinh, DienThoai, Email, DiaChi, NgayVaoLam, PhongBanID, ChucVu) VALUES
(N'Nguyễn Đức Khiêm', '2001-04-12', N'Nam', '0988123456', N'khiem.nguyen@example.com', N'Quận Hoàn Kiếm, Hà Nội', '2021-06-01', 4, N'Kỹ sư phần mềm'),
(N'Phạm Thị Lan', '1995-09-21', N'Nữ', '0912345678', N'lan.pham@example.com', N'Quận Đống Đa, Hà Nội', '2019-03-15', 1, N'Kế toán trưởng'),
(N'Lê Xuân Tân', '1998-12-02', N'Nam', '0977123456', N'tan.le@example.com', N'Quận Cầu Giấy, Hà Nội', '2022-01-10', 4, N'Kỹ thuật viên'),
(N'Bùi Quốc Việt', '1997-07-10', N'Nam', '0909123456', N'viet.bui@example.com', N'Quận Hai Bà Trưng, Hà Nội', '2020-08-20', 3, N'Nhân viên kinh doanh'),
(N'Đỗ Hoài Nam', '1996-05-05', N'Nam', '0966123456', N'nam.do@example.com', N'Quận Ba Đình, Hà Nội', '2018-11-01', 3, N'Trưởng phòng Kinh Doanh'),
(N'Huỳnh Thị Mai', '1992-02-14', N'Nữ', '0933123456', N'mai.huynh@example.com', N'Quận Thanh Xuân, Hà Nội', '2017-07-01', 2, N'Chuyên viên nhân sự'),
(N'Vũ Thị Hồng', '1989-11-30', N'Nữ', '0944123456', N'hong.vu@example.com', N'Quận Long Biên, Hà Nội', '2015-02-20', 1, N'Nhân viên kế toán'),
(N'Phan Văn An', '1990-06-18', N'Nam', '0922123456', N'an.phan@example.com', N'Quận Bắc Từ Liêm, Hà Nội', '2016-10-05', 5, N'Nhân viên hành chính'),
(N'Ngô Thị Hương', '1994-08-08', N'Nữ', '0955123456', N'huong.ngo@example.com', N'Quận Hà Đông, Hà Nội', '2023-04-01', 4, N'Thực tập sinh'),
(N'Trần Minh Phát', '1985-01-20', N'Nam', '0899123456', N'phat.tran@example.com', N'Quận Hoàng Mai, Hà Nội', '2010-09-15', 1, N'Kế toán viên cao cấp');
GO
