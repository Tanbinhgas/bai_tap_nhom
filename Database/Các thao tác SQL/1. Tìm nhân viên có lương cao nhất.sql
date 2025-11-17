USE [QuanLyNhanVien];
GO
/* TÌM NHÂN VIÊN CÓ TỔNG LƯƠNG CAO NHẤT */
SELECT TOP 1
    nv.MaNV,
    nv.HoTen,
    FORMAT(l.LuongCoBan, 'N0') + ' VND' AS LuongCoBan,
    FORMAT(ISNULL(l.PhuCap, 0), 'N0') + ' VND' AS PhuCap,
    (l.LuongCoBan + ISNULL(l.PhuCap, 0)) AS TongLuong,
    FORMAT(l.LuongCoBan + ISNULL(l.PhuCap, 0), 'N0') + ' VND' AS TongLuong_VND,
    pb.TenPhong AS PhongBan

FROM dbo.Luong l
INNER JOIN dbo.NhanVien nv ON l.MaNV = nv.MaNV
INNER JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
ORDER BY (l.LuongCoBan + ISNULL(l.PhuCap, 0)) DESC;
GO