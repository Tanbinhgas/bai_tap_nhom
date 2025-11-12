USE [QuanLyNhanVien];
GO


/* 7. Tổng số nhân viên tuyển mới theo tháng/năm (theo NgayVaoLam) */
-- Theo tháng trong năm hiện tại
SELECT YEAR(NgayVaoLam) AS Nam, MONTH(NgayVaoLam) AS Thang, COUNT(*) AS SoNhanVienTuyenMoi
FROM dbo.NhanVien
GROUP BY YEAR(NgayVaoLam), MONTH(NgayVaoLam)
ORDER BY Nam DESC, Thang DESC;
GO
