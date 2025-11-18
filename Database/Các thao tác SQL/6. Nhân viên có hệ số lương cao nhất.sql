USE [QuanLyNhanVien];
GO
/* TOP 3 NHÂN VIÊN CÓ TỔNG LƯƠNG CAO NHẤT */
SELECT TOP 3 WITH TIES
    nv.MaNV,
    nv.HoTen,
    pb.MaPhong,
    pb.TenPhong AS PhongBan,
    FORMAT(l.LuongCoBan, 'N0') + ' VND' AS LuongCoBan,
    FORMAT(ISNULL(l.PhuCap, 0), 'N0') + ' VND' AS PhuCap,
    FORMAT(l.LuongCoBan + ISNULL(l.PhuCap, 0), 'N0') + ' VND' AS TongLuong
FROM dbo.Luong l
INNER JOIN dbo.NhanVien nv ON l.MaNV = nv.MaNV
INNER JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
ORDER BY (l.LuongCoBan + ISNULL(l.PhuCap, 0)) DESC, nv.MaNV;
GO
