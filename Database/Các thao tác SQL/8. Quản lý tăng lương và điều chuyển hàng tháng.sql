USE [QuanLyNhanVien];
GO

-- XÓA SP CŨ
IF OBJECT_ID('dbo.sp_QuanLyTangLuongVaDieuChuyen', 'P') IS NOT NULL 
    DROP PROCEDURE dbo.sp_QuanLyTangLuongVaDieuChuyen;
GO

-- TẠO SP MỚI – SIÊU CHUYÊN NGHIỆP
CREATE PROCEDURE dbo.sp_QuanLyTangLuongVaDieuChuyen
    @PhongBanID_Cu      INT = NULL,           -- Phòng ban hiện tại (bắt buộc nếu tăng lương hoặc điều chuyển)
    @LuongCoBanMoi      DECIMAL(18,2) = NULL,  -- Lương cơ bản mới (thay cho HeSoLuong)
    @PhuCapMoi          DECIMAL(18,2) = NULL,  -- Phụ cấp mới (tùy chọn)
    @PhongBanID_Moi     INT = NULL,           -- Phòng ban mới (để điều chuyển)
    @GhiChu             NVARCHAR(255) = N'Tăng lương & điều chuyển tự động',
    @NguoiThucHien      NVARCHAR(100) = NULL  -- Người thực hiện (audit)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON; -- Tự động rollback nếu lỗi

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. KIỂM TRA THAM SỐ
        IF @PhongBanID_Cu IS NULL
        BEGIN
            RAISERROR(N'Thiếu tham số @PhongBanID_Cu (phòng ban hiện tại)!', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM dbo.PhongBan WHERE PhongBanID = @PhongBanID_Cu)
        BEGIN
            RAISERROR(N'Phòng ban ID %d không tồn tại!', 16, 1, @PhongBanID_Cu);
            RETURN;
        END

        IF @PhongBanID_Moi IS NOT NULL 
           AND NOT EXISTS (SELECT 1 FROM dbo.PhongBan WHERE PhongBanID = @PhongBanID_Moi)
        BEGIN
            RAISERROR(N'Phòng ban mới ID %d không tồn tại!', 16, 1, @PhongBanID_Moi);
            RETURN;
        END

        IF @LuongCoBanMoi IS NULL AND @PhongBanID_Moi IS NULL
        BEGIN
            RAISERROR(N'Phải cung cấp ít nhất @LuongCoBanMoi hoặc @PhongBanID_Moi!', 16, 1);
            RETURN;
        END

        -- 2. TĂNG LƯƠNG (nếu có)
        IF @LuongCoBanMoi IS NOT NULL
        BEGIN
            -- Lấy danh sách nhân viên đang làm việc trong phòng ban cũ
            WITH NhanVienPhong AS (
                SELECT NhanVienID
                FROM dbo.NhanVien
                WHERE PhongBanID = @PhongBanID_Cu AND TrangThai = 1
            )
            INSERT INTO dbo.Luong (NhanVienID, NgayApDung, LuongCoBan, PhuCap, GhiChu)
            SELECT 
                nv.NhanVienID,
                GETDATE(),
                @LuongCoBanMoi,
                ISNULL(@PhuCapMoi, 0),
                ISNULL(@GhiChu, N'Tăng lương tự động')
            FROM NhanVienPhong nv;

            PRINT N'Đã tăng lương cho ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + N' nhân viên.';
        END

        -- 3. ĐIỀU CHUYỂN PHÒNG BAN (nếu có)
        IF @PhongBanID_Moi IS NOT NULL
        BEGIN
            UPDATE dbo.NhanVien
            SET PhongBanID = @PhongBanID_Moi
            WHERE PhongBanID = @PhongBanID_Cu AND TrangThai = 1;

            PRINT N'Đã điều chuyển ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + N' nhân viên sang phòng mới.';
        END

        -- 4. TRẢ VỀ KẾT QUẢ SAU KHI CẬP NHẬT
        SELECT 
            nv.MaNV,
            nv.HoTen,
            pb.TenPhong AS PhongBanHienTai,
            nv.ChucVu,
            l.LuongCoBan,
            ISNULL(l.PhuCap, 0) AS PhuCap,
            (l.LuongCoBan + ISNULL(l.PhuCap, 0)) AS TongLuong,
            FORMAT(l.LuongCoBan + ISNULL(l.PhuCap, 0), 'N0') + ' ₫' AS TongLuong_VND,
            FORMAT(l.NgayApDung, 'dd/MM/yyyy HH:mm') AS NgayApDung,
            @NguoiThucHien AS NguoiThucHien
        FROM dbo.NhanVien nv
        INNER JOIN dbo.PhongBan pb ON nv.PhongBanID = pb.PhongBanID
        LEFT JOIN (
            -- Lương mới nhất
            SELECT l1.*
            FROM dbo.Luong l1
            INNER JOIN (
                SELECT NhanVienID, MAX(NgayApDung) AS MaxDate
                FROM dbo.Luong
                GROUP BY NhanVienID
            ) l2 ON l1.NhanVienID = l2.NhanVienID AND l1.NgayApDung = l2.MaxDate
        ) l ON nv.NhanVienID = l.NhanVienID
        WHERE nv.PhongBanID = ISNULL(@PhongBanID_Moi, @PhongBanID_Cu)
          AND nv.TrangThai = 1
        ORDER BY l.NgayApDung DESC, nv.HoTen;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO

-- Ví dụ: Tăng lương cho Phòng Kỹ Thuật (ID = 4) lên 25 triệu + phụ cấp 3 triệu
EXEC dbo.sp_QuanLyTangLuongVaDieuChuyen
    @PhongBanID_Cu = 4,           -- Phòng hiện tại
    @LuongCoBanMoi = 25000000,    -- Lương mới
    @PhuCapMoi = 3000000,         -- Phụ cấp mới
    @NguoiThucHien = N'HR - Admin';