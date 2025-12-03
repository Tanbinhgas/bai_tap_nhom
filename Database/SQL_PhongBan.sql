USE [QuanLyNhanVien];
GO

-- XÓA BẢNG CŨ
IF OBJECT_ID('dbo.PhongBan', 'U') IS NOT NULL DROP TABLE dbo.PhongBan;
GO

-- TẠO BẢNG PHÒNG BAN ĐƠN GIẢN
CREATE TABLE dbo.PhongBan (
    PhongBanID INT IDENTITY(1,1) PRIMARY KEY,
    MaPhong AS 'PB' + RIGHT('000' + CAST(PhongBanID AS VARCHAR(3)), 3) PERSISTED,
    TenPhong NVARCHAR(100) NOT NULL UNIQUE,
);
GO

-- THÊM 5 PHÒNG BAN
INSERT INTO dbo.PhongBan (TenPhong) VALUES
(N'Phòng Kế Toán'),
(N'Phòng Nhân Sự'),
(N'Phòng Kinh Doanh'),
(N'Phòng Kỹ Thuật'),
(N'Phòng Hành Chính');
GO

-- THÊM CỘT PHÒNG BAN VÀO NHÂN VIÊN
ALTER TABLE dbo.NhanVien ADD PhongBanID INT NULL;
ALTER TABLE dbo.NhanVien 
ADD CONSTRAINT FK_NhanVien_PhongBan 
    FOREIGN KEY (PhongBanID) REFERENCES dbo.PhongBan(PhongBanID);
GO

-- GÁN PHÒNG BAN CHO NHÂN VIÊN
UPDATE dbo.NhanVien SET PhongBanID = 
    CASE MaNV
        WHEN 'NV1002' THEN 1 WHEN 'NV1007' THEN 1 WHEN 'NV1010' THEN 1  -- Kế toán
        WHEN 'NV1006' THEN 2                                           -- Nhân sự
        WHEN 'NV1001' THEN 3 WHEN 'NV1004' THEN 3 WHEN 'NV1005' THEN 3 -- Kinh doanh
        WHEN 'NV1003' THEN 4                                           -- Kỹ thuật
        WHEN 'NV1008' THEN 5 WHEN 'NV1009' THEN 5                      -- Hành chính
    END;
GO

-- XEM KẾT QUẢ
SELECT pb.MaPhong, pb.TenPhong, COUNT(nv.MaNV) AS SoNhanVien
FROM dbo.PhongBan pb
LEFT JOIN dbo.NhanVien nv ON pb.PhongBanID = nv.PhongBanID
GROUP BY pb.MaPhong, pb.TenPhong;