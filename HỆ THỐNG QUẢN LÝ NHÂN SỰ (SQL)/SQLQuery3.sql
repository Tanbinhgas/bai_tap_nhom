-- 3. Chèn dữ liệu mẫu
INSERT INTO PhongBan VALUES 
('PB01', N'Phòng Kỹ thuật', N'Tầng 5 - Tòa A'),
('PB02', N'Phòng Hành chính', N'Tầng 3 - Tòa A'),
('PB03', N'Phòng Marketing', N'Tầng 4 - Tòa B');

INSERT INTO ChucVu VALUES 
('CV01', N'Nhân viên', 1.0),
('CV02', N'Trưởng phòng', 2.5),
('CV03', N'Giám đốc', 5.0);

INSERT INTO NhanVien VALUES 
('NV2025001', N'Nguyễn Văn A', '1990-05-15', 1, N'Hà Nội', '0901234567', 'nva@company.com', 'PB01', 'CV02'),
('NV2025002', N'Trần Thị B', '1995-08-20', 0, N'TP.HCM', '0912345678', 'ttb@company.com', 'PB02', 'CV01'),
('NV2025003', N'Lê Văn C', '1988-03-10', 1, N'Đà Nẵng', '0923456789', 'lvc@company.com', 'PB03', 'CV02'),
('NV2025004', N'Phạm Thị D', '1997-12-25', 0, N'Cần Thơ', '0934567890', 'ptd@company.com', 'PB01', 'CV01');

INSERT INTO BangLuong (MaNV, ThangNam, LuongCoBan, PhuCap, TamUng) VALUES 
('NV2025001', '2025-11-01', 20000000, 5000000, 3000000),
('NV2025002', '2025-11-01', 12000000, 2000000, 1000000),
('NV2025003', '2025-11-01', 22000000, 6000000, 4000000),
('NV2025004', '2025-11-01', 11000000, 1500000, 0),
('NV2025001', '2025-10-01', 19000000, 5000000, 2000000),
('NV2025004', '2025-10-01', 10000000, 1500000, 500000);
GO