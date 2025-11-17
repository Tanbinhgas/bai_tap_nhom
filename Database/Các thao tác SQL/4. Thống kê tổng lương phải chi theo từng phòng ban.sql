USE [QuanLyNhanVien];
GO
/* TỔNG CHI LƯƠNG HIỆN TẠI THEO PHÒNG BAN */
SELECT
    pb.MaPhong,
    pb.TenPhong,
    COUNT(nv.MaNV) AS SoNhanVien,
    SUM(l.LuongCoBan + ISNULL(l.PhuCap, 0)) AS TongChiLuong,
    FORMAT(SUM(l.LuongCoBan + ISNULL(l.PhuCap, 0)), 'N0') + ' VND' AS TongChiLuong_VND,
    CAST(AVG(l.LuongCoBan + ISNULL(l.PhuCap, 0) * 1.0) AS DECIMAL(18,0)) AS LuongTrungBinh,
    FORMAT(AVG(l.LuongCoBan + ISNULL(l.PhuCap, 0) * 1.0), 'N0') + ' VND' AS LuongTrungBinh_VND,
    MIN(l.LuongCoBan + ISNULL(l.PhuCap, 0)) AS LuongThapNhat,
    MAX(l.LuongCoBan + ISNULL(l.PhuCap, 0)) AS LuongCaoNhat
FROM dbo.PhongBan pb
LEFT JOIN dbo.NhanVien nv ON pb.PhongBanID = nv.PhongBanID
LEFT JOIN dbo.Luong l ON nv.MaNV = l.MaNV
WHERE nv.PhongBanID IS NOT NULL  -- Chỉ tính nhân viên có phòng ban
GROUP BY pb.PhongBanID, pb.MaPhong, pb.TenPhong
HAVING COUNT(nv.MaNV) > 0  -- Chỉ hiển thị phòng có ít nhất 1 nhân viên
ORDER BY TongChiLuong DESC;
GO