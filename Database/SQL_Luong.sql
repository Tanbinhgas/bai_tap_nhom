USE [QuanLyNhanVien];
GO

-- 1. XÓA BẢNG LUONG CŨ
IF OBJECT_ID('dbo.Luong', 'U') IS NOT NULL
    DROP TABLE dbo.Luong;
GO

-- TẠO BẢNG LUONG
CREATE TABLE dbo.Luong (
    LuongID INT IDENTITY(1,1) PRIMARY KEY,
    MaNV NVARCHAR(10) NOT NULL,
    LuongCoBan DECIMAL(18,2) NOT NULL,
    PhuCap DECIMAL(18,2) NULL DEFAULT 0,
    NgayApDung DATE NOT NULL DEFAULT GETDATE(),
    GhiChu NVARCHAR(255) NULL,

    CONSTRAINT CK_Luong_CoBan CHECK (LuongCoBan > 0),
    CONSTRAINT CK_Luong_PhuCap CHECK (PhuCap >= 0),
    CONSTRAINT FK_Luong_NhanVien FOREIGN KEY (MaNV) 
        REFERENCES dbo.NhanVien(MaNV)
        ON DELETE CASCADE
);
GO

-- THÊM DỮ LIỆU LƯƠNG
INSERT INTO dbo.Luong (MaNV, LuongCoBan, PhuCap, NgayApDung, GhiChu) VALUES
('NV1001', 15000000, 2000000, '2024-01-01', N'Lương chính thức + phụ cấp xăng xe'),
('NV1002', 25000000, 3000000, '2023-06-01', N'Kế toán trưởng'),
('NV1003', 12000000, 1500000, '2024-01-01', N'Kỹ thuật viên'),
('NV1004', 18000000, 2500000, '2023-01-01', N'Nhân viên kinh doanh xuất sắc'),
('NV1005', 35000000, 5000000, '2022-01-01', N'Trưởng phòng Kinh Doanh'),
('NV1006', 14000000, 1000000, '2024-03-01', N'Chuyên viên nhân sự'),
('NV1007', 20000000, 2000000, '2023-01-01', N'Kế toán cao cấp'),
('NV1008', 11000000, 800000,  '2024-01-01', N'Hành chính'),
('NV1009',  8000000, 500000,  '2024-04-01', N'Thực tập sinh'),
('NV1010', 30000000, 4000000, '2022-01-01', N'Kế toán viên cao cấp');
GO

-- XEM BẢNG LƯƠNG ĐẸP
SELECT 
    nv.MaNV AS [Mã NV],
    nv.HoTen AS [Họ tên],
    FORMAT(l.LuongCoBan, 'N0') + ' ₫' AS [Lương CB],
    FORMAT(ISNULL(l.PhuCap, 0), 'N0') + ' ₫' AS [Phụ cấp],
    FORMAT(l.LuongCoBan + ISNULL(l.PhuCap, 0), 'N0') + ' ₫' AS [Tổng lương],
    FORMAT(l.NgayApDung, 'dd/MM/yyyy') AS [Ngày áp dụng],
    l.GhiChu
FROM dbo.Luong l
INNER JOIN dbo.NhanVien nv ON l.MaNV = nv.MaNV
ORDER BY l.LuongCoBan DESC;