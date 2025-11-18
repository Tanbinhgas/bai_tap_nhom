USE [QuanLyNhanVien];
GO
/* 4. TOP LƯƠNG CAO NHẤT TỪNG PHÒNG */
CREATE OR ALTER FUNCTION dbo.fn_TopLuongTungPhong ()
RETURNS TABLE
AS
RETURN
(
    SELECT
        pb.TenPhong,
        nv.MaNV,
        nv.HoTen,
        l.LuongCoBan + ISNULL(l.PhuCap, 0) AS TongLuong_So,
        FORMAT(l.LuongCoBan + ISNULL(l.PhuCap, 0), 'N0') + ' VND' AS TongLuong_VND,
        ROW_NUMBER() OVER (PARTITION BY nv.PhongBanID 
                           ORDER BY l.LuongCoBan + ISNULL(l.PhuCap, 0) DESC) AS XepHang
    FROM dbo.NhanVien nv
    JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
    JOIN dbo.Luong l ON nv.MaNV = l.MaNV
)
GO

-- XEM KẾT QUẢ
SELECT 
    TenPhong,
    MaNV,
    HoTen,
    TongLuong_VND
FROM dbo.fn_TopLuongTungPhong() 
WHERE XepHang = 1
ORDER BY TongLuong_So DESC;