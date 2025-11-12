USE [QuanLyNhanVien];
GO


/* 2. Thống kê số lượng nhân viên theo phòng ban */
SELECT pb.PhongBanID, pb.TenPhong, COUNT(nv.NhanVienID) AS SoLuongNhanVien
FROM dbo.PhongBan pb
LEFT JOIN dbo.NhanVien nv ON pb.PhongBanID = nv.PhongBanID
GROUP BY pb.PhongBanID, pb.TenPhong
ORDER BY SoLuongNhanVien DESC;
GO
