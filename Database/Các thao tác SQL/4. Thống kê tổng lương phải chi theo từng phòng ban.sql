USE [QuanLyNhanVien]
GO

/* TỔNG CHI LƯƠNG HIỆN TẠI THEO PHÒNG BAN */
WITH LuongHienTai AS (
    -- LẤY LƯƠNG MỚI NHẤT CỦA MỖI NHÂN VIÊN (chỉ tính nhân viên đang làm)
    SELECT 
        l.NhanVienID,
        l.LuongCoBan,
        l.PhuCap,
        l.NgayApDung,
        (l.LuongCoBan + ISNULL(l.PhuCap, 0)) AS TongLuong,
        ROW_NUMBER() OVER (PARTITION BY l.NhanVienID ORDER BY l.NgayApDung DESC) AS RN
    FROM dbo.Luong l
    INNER JOIN dbo.NhanVien nv ON l.NhanVienID = nv.NhanVienID
    WHERE nv.TrangThai = 1  -- Chỉ tính nhân viên đang làm việc
)
SELECT 
    pb.MaPhong,
    pb.TenPhong,
    COUNT(nv.NhanVienID) AS SoNhanVien,
    SUM(lh.TongLuong) AS TongChiLuong,
    FORMAT(SUM(lh.TongLuong), 'N0') + ' ₫' AS TongChiLuong_VND,
    CAST(AVG(lh.TongLuong * 1.0) AS DECIMAL(18,0)) AS LuongTrungBinh,
    FORMAT(AVG(lh.TongLuong * 1.0), 'N0') + ' ₫' AS LuongTrungBinh_VND,
    MIN(lh.TongLuong) AS LuongThapNhat,
    MAX(lh.TongLuong) AS LuongCaoNhat
FROM dbo.PhongBan pb
LEFT JOIN dbo.NhanVien nv ON pb.PhongBanID = nv.PhongBanID AND nv.TrangThai = 1
LEFT JOIN LuongHienTai lh ON nv.NhanVienID = lh.NhanVienID AND lh.RN = 1
GROUP BY pb.PhongBanID, pb.MaPhong, pb.TenPhong
HAVING COUNT(nv.NhanVienID) > 0  -- Chỉ hiển thị phòng có nhân viên
ORDER BY TongChiLuong DESC;
GO