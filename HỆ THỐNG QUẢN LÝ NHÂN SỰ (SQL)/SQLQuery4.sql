-- Kiểm tra kết quả
SELECT 'PhongBan' AS Bang, COUNT(*) AS SoDong FROM PhongBan
UNION ALL SELECT 'ChucVu', COUNT(*) FROM ChucVu
UNION ALL SELECT 'NhanVien', COUNT(*) FROM NhanVien
UNION ALL SELECT 'BangLuong', COUNT(*) FROM BangLuong;
GO