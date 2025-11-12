USE [QuanLyNhanVien]
GO

/* TÌM NHÂN VIÊN CÓ LƯƠNG CAO NHẤT */
WITH LuongMoiNhat AS (
    -- LẤY BẢN GHI LƯƠNG MỚI NHẤT CỦA MỖI NHÂN VIÊN
    SELECT 
        NhanVienID,
        LuongID,
        LuongCoBan,
        PhuCap,
        NgayApDung,
        ROW_NUMBER() OVER (PARTITION BY NhanVienID ORDER BY NgayApDung DESC) AS RN
    FROM dbo.Luong
)
SELECT TOP 1
    nv.MaNV,
    nv.HoTen,
    FORMAT(lmn.LuongCoBan, 'N0') + ' ₫' AS LuongCoBan,
    FORMAT(lmn.PhuCap, 'N0') + ' ₫' AS PhuCap,
    (lmn.LuongCoBan + ISNULL(lmn.PhuCap, 0)) AS TongLuong,
    FORMAT(lmn.LuongCoBan + ISNULL(lmn.PhuCap, 0), 'N0') + ' ₫' AS TongLuong_VND,
    lmn.NgayApDung,
    pb.TenPhong AS PhongBan,
    nv.ChucVu
FROM LuongMoiNhat lmn
JOIN dbo.NhanVien nv ON lmn.NhanVienID = nv.NhanVienID
JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
WHERE lmn.RN = 1  -- Chỉ lấy bản ghi mới nhất
ORDER BY (lmn.LuongCoBan + ISNULL(lmn.PhuCap, 0)) DESC;
GO