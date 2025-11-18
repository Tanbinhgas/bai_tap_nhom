USE [QuanLyNhanVien];
GO
/* TÌM NHÂN VIÊN SINH NĂM CHẴN */
CREATE OR ALTER FUNCTION dbo.fn_NhanVienSinhNamChan()
RETURNS TABLE
AS
RETURN
(
    WITH DuLieu AS (
        SELECT
            nv.MaNV,
            nv.HoTen,
            nv.NgaySinh,
            YEAR(nv.NgaySinh) AS NamSinh,
            pb.TenPhong,
            l.LuongCoBan + ISNULL(l.PhuCap, 0) AS TongLuong,
            ROW_NUMBER() OVER (ORDER BY YEAR(nv.NgaySinh)) AS STT
        FROM dbo.NhanVien nv
        JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
        LEFT JOIN dbo.Luong l ON nv.MaNV = l.MaNV
        WHERE nv.NgaySinh IS NOT NULL
          AND YEAR(nv.NgaySinh) % 2 = 0
    )
    SELECT
        MaNV,
        HoTen,
        FORMAT(NgaySinh, 'dd/MM/yyyy') AS NgaySinh,
        NamSinh,
        TenPhong AS PhongBan,
        FORMAT(TongLuong, 'N0') + ' VND' AS TongLuong,
        N'Sinh năm chẵn' AS GhiChu
    FROM DuLieu
)
GO

-- XEM KẾT QUẢ SIÊU ĐẸP
SELECT * 
FROM dbo.fn_NhanVienSinhNamChan()
ORDER BY NamSinh DESC, HoTen;