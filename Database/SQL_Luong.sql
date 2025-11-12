USE [QuanLyNhanVien]
GO

-- XÓA BẢNG CŨ
IF OBJECT_ID('dbo.Luong', 'U') IS NOT NULL DROP TABLE dbo.Luong;
GO

-- TẠO BẢNG LƯƠNG MỚI
CREATE TABLE dbo.Luong (
    LuongID INT IDENTITY(1,1) PRIMARY KEY,
    NhanVienID INT NOT NULL,
    LuongCoBan DECIMAL(18,2) NOT NULL,
    PhuCap DECIMAL(18,2) NULL,
    NgayApDung DATE NOT NULL DEFAULT GETDATE(),
    GhiChu NVARCHAR(255) NULL,
    CONSTRAINT FK_Luong_NhanVien FOREIGN KEY (NhanVienID) REFERENCES dbo.NhanVien(NhanVienID)
);
GO

-- THÊM DỮ LIỆU MẪU
INSERT INTO dbo.Luong (NhanVienID, LuongCoBan, PhuCap, NgayApDung, GhiChu) VALUES
(1001, 15000000, 2000000, '2024-01-01', N'Lương chính thức + phụ cấp xăng xe'),
(1002, 25000000, 3000000, '2023-06-01', N'Kế toán trưởng'),
(1003, 12000000, 1500000, '2024-01-01', N'Kỹ thuật viên'),
(1004, 18000000, 2500000, '2023-01-01', N'Nhân viên kinh doanh xuất sắc'),
(1005, 35000000, 5000000, '2022-01-01', N'Trưởng phòng Kinh Doanh'),
(1006, 14000000, 1000000, '2024-03-01', N'Chuyên viên nhân sự'),
(1007, 20000000, 2000000, '2023-01-01', N'Kế toán cao cấp'),
(1008, 11000000, 800000,  '2024-01-01', N'Hành chính'),
(1009,  8000000, 500000,  '2024-04-01', N'Thực tập sinh'),
(1010, 30000000, 4000000, '2022-01-01', N'Kế toán viên cao cấp');
GO

-- KIỂM TRA
SELECT nv.HoTen, l.LuongCoBan, l.PhuCap, l.NgayApDung 
FROM dbo.Luong l
JOIN dbo.NhanVien nv ON l.NhanVienID = nv.NhanVienID;