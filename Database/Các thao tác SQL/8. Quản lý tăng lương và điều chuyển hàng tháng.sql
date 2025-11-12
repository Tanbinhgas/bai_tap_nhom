-- SỬA LẠI TOÀN BỘ FILE
IF OBJECT_ID('dbo.sp_QuanLyTangLuongVaDieuChuyen', 'P') IS NOT NULL 
    DROP PROCEDURE dbo.sp_QuanLyTangLuongVaDieuChuyen;
GO

CREATE PROCEDURE dbo.sp_QuanLyTangLuongVaDieuChuyen
    @PhongBanID INT = NULL,  -- THAM SỐ ĐẦU VÀO
    @HeSoLuongMoi DECIMAL(4,2) = NULL,
    @PhongBanMoiID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tham số
    IF @PhongBanID IS NULL AND @HeSoLuongMoi IS NULL AND @PhongBanMoiID IS NULL
    BEGIN
        PRINT 'Vui lòng cung cấp ít nhất một tham số!';
        RETURN;
    END

    -- Cập nhật lương mới cho nhân viên trong phòng ban
    IF @HeSoLuongMoi IS NOT NULL AND @PhongBanID IS NOT NULL
    BEGIN
        WITH LatestLuong AS (
            SELECT l1.*
            FROM dbo.Luong l1
            INNER JOIN (
                SELECT NhanVienID, MAX(NgayApDung) AS MaxDate
                FROM dbo.Luong
                GROUP BY NhanVienID
            ) l2 ON l1.NhanVienID = l2.NhanVienID AND l1.NgayApDung = l2.MaxDate
        )
        INSERT INTO dbo.Luong (NhanVienID, NgayApDung, HeSoLuong, PhuCap, GhiChu)
        SELECT nv.NhanVienID, GETDATE(), @HeSoLuongMoi, NULL, N'Tăng lương tự động'
        FROM dbo.NhanVien nv
        INNER JOIN LatestLuong ll ON nv.NhanVienID = ll.NhanVienID
        WHERE nv.PhongBanID = @PhongBanID;
    END

    -- Điều chuyển nhân viên sang phòng ban mới
    IF @PhongBanMoiID IS NOT NULL AND @PhongBanID IS NOT NULL
    BEGIN
        UPDATE dbo.NhanVien
        SET PhongBanID = @PhongBanMoiID
        WHERE PhongBanID = @PhongBanID;
    END

    -- Trả về danh sách nhân viên sau khi cập nhật
    SELECT nv.NhanVienID, nv.HoTen, nv.PhongBanID, pb.TenPhong, 
           ll.HeSoLuong, ll.NgayApDung
    FROM dbo.NhanVien nv
    INNER JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
    LEFT JOIN (
        SELECT l1.*
        FROM dbo.Luong l1
        INNER JOIN (
            SELECT NhanVienID, MAX(NgayApDung) AS MaxDate
            FROM dbo.Luong
            GROUP BY NhanVienID
        ) l2 ON l1.NhanVienID = l2.NhanVienID AND l1.NgayApDung = l2.MaxDate
    ) ll ON nv.NhanVienID = ll.NhanVienID
    WHERE nv.PhongBanID = ISNULL(@PhongBanID, nv.PhongBanID);
END
GO