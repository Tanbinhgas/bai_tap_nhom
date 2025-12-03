USE [QuanLyNhanVien];
GO
/* 7. NHÂN VIÊN CÓ ĐỊA CHỈ CHỨA CHỮ "B" */
SELECT 
    nv.MaNV,
    nv.HoTen,
    nv.GioiTinh,
    nv.DiaChi,
    pb.MaPhong,
    pb.TenPhong,
    nv.DienThoai,
    nv.Email
FROM dbo.NhanVien nv
INNER JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
WHERE UPPER(nv.DiaChi) LIKE N'%B%'  -- Tìm chữ B (hoa hoặc thường)
ORDER BY pb.TenPhong, nv.HoTen;
GO