USE [QuanLyNhanVien];
GO
/* TOP 3 NHÂN VIÊN CÓ TỔNG LƯƠNG CAO NHẤT */
SELECT TOP 3 WITH TIES
    nv.MaNV,
    nv.HoTen,
    pb.MaPhong,
    pb.TenPhong AS PhongBan,
    FORMAT(l.LuongCoBan, 'N0') + ' ₫' AS LuongCoBan,
    FORMAT(ISNULL(l.PhuCap, 0), 'N0') + ' ₫' AS PhuCap,
    (l.LuongCoBan + ISNULL(l.PhuCap, 0)) AS TongLuong,
    FORMAT(l.LuongCoBan + ISNULL(l.PhuCap, 0), 'N0') + ' ₫' AS TongLuong_VND
FROM dbo.Luong l
INNER JOIN dbo.NhanVien nv ON l.MaNV = nv.MaNV
INNER JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
ORDER BY (l.LuongCoBan + ISNULL(l.PhuCap, 0)) DESC, nv.MaNV;
GO