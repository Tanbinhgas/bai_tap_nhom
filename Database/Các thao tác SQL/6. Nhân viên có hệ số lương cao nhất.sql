USE [QuanLyNhanVien]
GO

/* NHÂN VIÊN CÓ TỔNG LƯƠNG CAO NHẤT (dựa trên bản ghi lương mới nhất) */
WITH LuongMoiNhat AS (
    -- LẤY LƯƠNG MỚI NHẤT CỦA MỖI NHÂN VIÊN (chỉ đang làm việc)
    SELECT 
        l.NhanVienID,
        l.LuongCoBan,
        l.PhuCap,
        l.NgayApDung,
        (l.LuongCoBan + ISNULL(l.PhuCap, 0)) AS TongLuong,
        ROW_NUMBER() OVER (PARTITION BY l.NhanVienID ORDER BY l.NgayApDung DESC) AS RN
    FROM dbo.Luong l
    INNER JOIN dbo.NhanVien nv ON l.NhanVienID = nv.NhanVienID
    WHERE nv.TrangThai = 1
),
TongLuongCaoNhat AS (
    -- TÌM TỔNG LƯƠNG CAO NHẤT TRONG CÁC BẢN GHI MỚI NHẤT
    SELECT MAX(TongLuong) AS MaxTongLuong
    FROM LuongMoiNhat
    WHERE RN = 1
)
SELECT 
    nv.MaNV,
    nv.HoTen,
    pb.TenPhong AS PhongBan,
    nv.ChucVu,
    lmn.LuongCoBan,
    ISNULL(lmn.PhuCap, 0) AS PhuCap,
    lmn.TongLuong,
    FORMAT(lmn.TongLuong, 'N0') + ' ₫' AS TongLuong_VND,
    FORMAT(lmn.NgayApDung, 'dd/MM/yyyy') AS NgayApDung
FROM LuongMoiNhat lmn
JOIN dbo.NhanVien nv ON lmn.NhanVienID = nv.NhanVienID
JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
CROSS JOIN TongLuongCaoNhat tlc
WHERE lmn.RN = 1 
  AND lmn.TongLuong = tlc.MaxTongLuong  -- Chỉ lấy người có tổng lương cao nhất
ORDER BY lmn.TongLuong DESC;
GO