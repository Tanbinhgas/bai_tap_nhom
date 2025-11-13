USE [QuanLyNhanVien];
GO

-- XÓA BẢNG CŨ NẾU TỒN TẠI
IF OBJECT_ID('dbo.NhanVien', 'U') IS NOT NULL 
    DROP TABLE dbo.NhanVien;
GO

-- TẠO BẢNG NHANVIEN MỚI
CREATE TABLE dbo.NhanVien (
    MaNV NVARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(150) NOT NULL,
    GioiTinh NVARCHAR(10) NULL CHECK (GioiTinh IN (N'Nam', N'Nữ')),
    NgaySinh DATE NULL,
    DienThoai VARCHAR(15) NULL,
    Email NVARCHAR(100) NULL,
    DiaChi NVARCHAR(255) NULL,

    CONSTRAINT CK_NhanVien_SDT CHECK (DienThoai LIKE '[0-9]%' AND LEN(DienThoai) >= 10 OR DienThoai IS NULL),
    CONSTRAINT CK_NhanVien_Email CHECK (Email LIKE '%@%.%' OR Email IS NULL),
    CONSTRAINT CK_MaNV_Format CHECK (MaNV LIKE 'NV1[0-9][0-9][0-9]')  -- Bắt buộc NV1001 - NV9999
);
GO

-- THÊM NHÂN VIÊN MẪU
INSERT INTO dbo.NhanVien (MaNV, HoTen, GioiTinh, NgaySinh, DienThoai, Email, DiaChi)
VALUES
('NV1001', N'Nguyễn Đức Khiêm', N'Nam', '2001-04-12', '0988123456', 'khiem.nguyen@congty.vn', N'123 Hoàn Kiếm, Hà Nội'),
('NV1002', N'Phạm Thị Lan', N'Nữ', '1995-09-21', '0912345678', 'lan.pham@congty.vn', N'456 Đống Đa, Hà Nội'),
('NV1003', N'Lê Xuân Tân', N'Nam', '1998-12-02', '0977123456', 'tan.le@congty.vn', N'789 Cầu Giấy, Hà Nội'),
('NV1004', N'Bùi Quốc Việt', N'Nam', '1997-07-10', '0909123456', 'viet.bui@congty.vn', N'101 Hai Bà Trưng, Hà Nội'),
('NV1005', N'Đỗ Hoài Nam', N'Nam', '1996-05-05', '0966123456', 'nam.do@congty.vn', N'202 Ba Đình, Hà Nội'),
('NV1006', N'Huỳnh Thị Mai', N'Nữ', '1992-02-14', '0933123456', 'mai.huynh@congty.vn', N'303 Thanh Xuân, Hà Nội'),
('NV1007', N'Vũ Thị Hồng', N'Nữ', '1989-11-30', '0944123456', 'hong.vu@congty.vn', N'404 Long Biên, Hà Nội'),
('NV1008', N'Phan Văn An', N'Nam', '1990-06-18', '0922123456', 'an.phan@congty.vn', N'505 Bắc Từ Liêm, Hà Nội'),
('NV1009', N'Ngô Thị Hương', N'Nữ', '1994-08-08', '0955123456', 'huong.ngo@congty.vn', N'606 Hà Đông, Hà Nội'),
('NV1010', N'Trần Minh Phát', N'Nam', '1985-01-20', '0899123456', 'phat.tran@congty.vn', N'707 Hoàng Mai, Hà Nội');
GO

-- XEM KẾT QUẢ
SELECT 
    MaNV,
    HoTen,
    GioiTinh,
    NgaySinh,
    DienThoai,
    Email,
    DiaChi
FROM dbo.NhanVien
ORDER BY MaNV;
GO