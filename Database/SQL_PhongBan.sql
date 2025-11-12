USE [QuanLyNhanVien]
GO

-- XÓA BẢNG CŨ
IF OBJECT_ID('dbo.PhongBan', 'U') IS NOT NULL DROP TABLE dbo.PhongBan;
GO

-- TẠO BẢNG PHÒNG BAN HOÀN HẢO
CREATE TABLE dbo.PhongBan (
    PhongBanID      INT IDENTITY(1,1) PRIMARY KEY,
    MaPhong         AS 'PB' + RIGHT('000' + CAST(PhongBanID AS VARCHAR(3)), 3) PERSISTED, -- PB001, PB002...
    TenPhong        NVARCHAR(100) NOT NULL,
    MoTa            NVARCHAR(500) NULL,
    DiaChi          NVARCHAR(255) NULL,
    NgayThanhLap    DATE NULL DEFAULT GETDATE(),
    TrangThai       BIT NOT NULL DEFAULT 1, -- 1: Hoạt động, 0: Ngừng
    SoDienThoai     VARCHAR(15) NULL,
    EmailPhong      NVARCHAR(100) NULL,
    
    -- RÀNG BUỘC
    CONSTRAINT UK_PhongBan_TenPhong UNIQUE (TenPhong),
    CONSTRAINT CK_PhongBan_Email CHECK (EmailPhong LIKE '%@%.%' OR EmailPhong IS NULL),
    CONSTRAINT CK_PhongBan_SDT CHECK (SoDienThoai LIKE '[0-9][0-9][0-9]%' OR SoDienThoai IS NULL)
);
GO

-- DỮ LIỆU MẪU
INSERT INTO dbo.PhongBan (TenPhong, MoTa, DiaChi, NgayThanhLap, SoDienThoai, EmailPhong) VALUES
(N'Phòng Kế Toán', 
 N'Quản lý tài chính, lập báo cáo, thanh toán lương, kiểm toán nội bộ', 
 N'Tầng 2 - Tòa nhà A', '2010-01-01', '024-8888-1001', 'ketoan@congty.vn'),

(N'Phòng Nhân Sự', 
 N'Tuyển dụng, đào tạo, chấm công, quản lý hồ sơ nhân viên, phúc lợi', 
 N'Tầng 3 - Tòa nhà A', '2010-01-01', '024-8888-1002', 'nhansu@congty.vn'),

(N'Phòng Kinh Doanh', 
 N'Tìm kiếm khách hàng, đàm phán hợp đồng, chăm sóc sau bán hàng', 
 N'Tầng 4 - Tòa nhà B', '2012-06-15', '024-8888-1003', 'kinhdoanh@congty.vn'),

(N'Phòng Kỹ Thuật', 
 N'Phát triển phần mềm, bảo trì hệ thống, hỗ trợ kỹ thuật', 
 N'Tầng 5 - Tòa nhà B', '2015-03-01', '024-8888-1004', 'kythuat@congty.vn'),

(N'Phòng Hành Chính', 
 N'Quản lý văn phòng, tài sản, xe công ty, tổ chức sự kiện nội bộ', 
 N'Tầng 1 - Tòa nhà A', '2010-01-01', '024-8888-1005', 'hanhchinh@congty.vn');
GO

-- XEM KẾT QUẢ
SELECT 
    MaPhong,
    TenPhong,
    MoTa,
    DiaChi,
    FORMAT(NgayThanhLap, 'dd/MM/yyyy') AS NgayThanhLap,
    SoDienThoai,
    EmailPhong,
    CASE WHEN TrangThai = 1 THEN N'Hoạt động' ELSE N'Ngừng' END AS TrangThai
FROM dbo.PhongBan
ORDER BY PhongBanID;