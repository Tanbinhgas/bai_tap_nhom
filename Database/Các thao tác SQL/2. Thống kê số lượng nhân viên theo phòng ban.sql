USE [QuanLyNhanVien];
GO
/* 2. Thống kê số lượng nhân viên theo phòng ban */
SELECT 
    pb.MaPhong,
    pb.TenPhong,
    COUNT(nv.MaNV) AS SoLuongNhanVien
FROM dbo.PhongBan pb
LEFT JOIN dbo.NhanVien nv ON pb.PhongBanID = nv.PhongBanID
GROUP BY pb.MaPhong, pb.TenPhong
ORDER BY SoLuongNhanVien DESC;
GO