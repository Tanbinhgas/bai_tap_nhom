USE [QuanLyNhanVien]
GO

-- XÓA BẢNG CŨ
IF OBJECT_ID('dbo.NhanVien', 'U') IS NOT NULL DROP TABLE dbo.NhanVien;
GO

-- TẠO BẢNG NHÂN VIÊN MỚI
CREATE TABLE dbo.NhanVien (
    NhanVienID      INT IDENTITY(1001,1) PRIMARY KEY,
    MaNV            AS 'NV' + RIGHT('0000' + CAST(NhanVienID AS VARCHAR(5)), 5) PERSISTED, -- NV01001
    HoTen           NVARCHAR(150) NOT NULL,
    NgaySinh        DATE NULL,
    GioiTinh        NVARCHAR(10) NULL CHECK (GioiTinh IN (N'Nam', N'Nữ')),
    CCCD            VARCHAR(12) NULL,
    DienThoai       VARCHAR(15) NULL,
    Email           NVARCHAR(100) NULL,
    DiaChi          NVARCHAR(255) NULL,
    NgayVaoLam      DATE NULL DEFAULT GETDATE(),
    PhongBanID      INT NULL,
    ChucVu          NVARCHAR(100) NULL,
    TrangThai       BIT NOT NULL DEFAULT 1, -- 1: Đang làm, 0: Nghỉ việc
    AnhDaiDien      VARBINARY(MAX) NULL,
    GhiChu          NVARCHAR(500) NULL,

    -- RÀNG BUỘC
    CONSTRAINT UK_NhanVien_CCCD UNIQUE (CCCD),
    CONSTRAINT UK_NhanVien_Email UNIQUE (Email),
    CONSTRAINT FK_NhanVien_PhongBan FOREIGN KEY (PhongBanID) REFERENCES dbo.PhongBan(PhongBanID),
    CONSTRAINT CK_NhanVien_CCCD CHECK (CCCD NOT LIKE '%[^0-9]%' AND LEN(CCCD) IN (9,12) OR CCCD IS NULL),
    CONSTRAINT CK_NhanVien_Email CHECK (Email LIKE '%@%.%' OR Email IS NULL),
    CONSTRAINT CK_NhanVien_SDT CHECK (DienThoai LIKE '[0-9]%' AND LEN(DienThoai) >= 10 OR DienThoai IS NULL)
);
GO

-- DỮ LIỆU MẪU
INSERT INTO dbo.NhanVien (HoTen, NgaySinh, GioiTinh, CCCD, DienThoai, Email, DiaChi, NgayVaoLam, PhongBanID, ChucVu, GhiChu) VALUES
(N'Nguyễn Đức Khiêm', '2001-04-12', N'Nam', '012345678901', '0988123456', 'khiem.nguyen@congty.vn', N'123 Hoàn Kiếm, Hà Nội', '2021-06-01', 4, N'Kỹ sư phần mềm', N'Nhân viên xuất sắc 2023'),
(N'Phạm Thị Lan', '1995-09-21', N'Nữ', '012345678902', '0912345678', 'lan.pham@congty.vn', N'456 Đống Đa, Hà Nội', '2019-03-15', 1, N'Kế toán trưởng', N'Quản lý tài chính'),
(N'Lê Xuân Tân', '1998-12-02', N'Nam', '012345678903', '0977123456', 'tan.le@congty.vn', N'789 Cầu Giấy, Hà Nội', '2022-01-10', 4, N'Kỹ thuật viên', N'Hỗ trợ 24/7'),
(N'Bùi Quốc Việt', '1997-07-10', N'Nam', '012345678904', '0909123456', 'viet.bui@congty.vn', N'101 Hai Bà Trưng, Hà Nội', '2020-08-20', 3, N'Nhân viên kinh doanh', N'Doanh số cao nhất Q4'),
(N'Đỗ Hoài Nam', '1996-05-05', N'Nam', '012345678905', '0966123456', 'nam.do@congty.vn', N'202 Ba Đình, Hà Nội', '2018-11-01', 3, N'Trưởng phòng Kinh Doanh', N'Quản lý đội KD'),
(N'Huỳnh Thị Mai', '1992-02-14', N'Nữ', '012345678906', '0933123456', 'mai.huynh@congty.vn', N'303 Thanh Xuân, Hà Nội', '2017-07-01', 2, N'Chuyên viên nhân sự', N'Tuyển dụng & đào tạo'),
(N'Vũ Thị Hồng', '1989-11-30', N'Nữ', '012345678907', '0944123456', 'hong.vu@congty.vn', N'404 Long Biên, Hà Nội', '2015-02-20', 1, N'Nhân viên kế toán', N'Kiểm toán nội bộ'),
(N'Phan Văn An', '1990-06-18', N'Nam', '012345678908', '0922123456', 'an.phan@congty.vn', N'505 Bắc Từ Liêm, Hà Nội', '2016-10-05', 5, N'Nhân viên hành chính', N'Quản lý tài sản'),
(N'Ngô Thị Hương', '1994-08-08', N'Nữ', '012345678909', '0955123456', 'huong.ngo@congty.vn', N'606 Hà Đông, Hà Nội', '2023-04-01', 4, N'Thực tập sinh', N'Học việc 6 tháng'),
(N'Trần Minh Phát', '1985-01-20', N'Nam', '012345678910', '0899123456', 'phat.tran@congty.vn', N'707 Hoàng Mai, Hà Nội', '2010-09-15', 1, N'Kế toán viên cao cấp', N'15 năm kinh nghiệm');
GO

-- XEM KẾT QUẢ
SELECT 
    MaNV,
    HoTen,
    GioiTinh,
    DienThoai,
    Email,
	DiaChi,
    ChucVu,
    CASE WHEN TrangThai = 1 THEN N'Đang làm' ELSE N'Nghỉ việc' END AS TrangThai
FROM dbo.NhanVien
ORDER BY NhanVienID;