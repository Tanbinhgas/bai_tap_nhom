USE [QuanLyNhanVien];
GO
/* 5. TOP 3 NHÂN VIÊN LỚN TUỔI NHẤT */
SELECT TOP 3
    nv.MaNV,
    nv.HoTen,
    FORMAT(nv.NgaySinh, 'dd/MM/yyyy') AS NgaySinh,
    DATEDIFF(YEAR, nv.NgaySinh, GETDATE()) - 
        CASE 
            WHEN DATEADD(YEAR, DATEDIFF(YEAR, nv.NgaySinh, GETDATE()), nv.NgaySinh) > GETDATE() 
            THEN 1 ELSE 0 
        END AS Tuoi,
    pb.TenPhong,
    FORMAT(l.LuongCoBan + ISNULL(l.PhuCap, 0), 'N0') + ' ₫' AS TongLuong
FROM dbo.NhanVien nv
INNER JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
LEFT JOIN dbo.Luong l ON nv.MaNV = l.MaNV
WHERE nv.NgaySinh IS NOT NULL
ORDER BY Tuoi DESC, nv.MaNV;
GO