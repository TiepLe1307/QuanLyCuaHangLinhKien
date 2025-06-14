CREATE DATABASE quanlycuahanglinhkiendienthoai;
GO

USE quanlycuahanglinhkiendienthoai;
GO

-- 1. Bảng tài khoản
CREATE TABLE TAIKHOAN (
    TenDangNhap NVARCHAR(50) PRIMARY KEY,
    MatKhau NVARCHAR(50) NOT NULL,
    LoaiTaiKhoan NVARCHAR(10) CHECK (LoaiTaiKhoan IN ('admin', 'nhanvien'))
);

-- 2. Bảng Admin
CREATE TABLE ADMIN (
    TenDangNhap NVARCHAR(50) PRIMARY KEY,
    FOREIGN KEY (TenDangNhap) REFERENCES TAIKHOAN(TenDangNhap)
);
-- Bước 1: Thêm tài khoản admin vào bảng TAIKHOAN
INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, LoaiTaiKhoan)
VALUES ('tiep', '1', 'admin');
INSERT INTO ADMIN (TenDangNhap)
VALUES ('tiep');
-- Bước 2: Thêm vào

-- 3. Bảng Nhân viên (đã đổi từ TaiKhoan → TenDangNhap)
	CREATE TABLE  NHANVIEN (
    MaNV NVARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100),
    SoDienThoai NVARCHAR(20),
    QueQuan NVARCHAR(100),
    Email NVARCHAR(100),
    TenDangNhap NVARCHAR(50) UNIQUE,
    MatKhau NVARCHAR(50),
    FOREIGN KEY (TenDangNhap) REFERENCES TAIKHOAN(TenDangNhap)
);

-- 4. Bảng Nhà cung cấp
CREATE TABLE NHACUNGCAP (
    MaNCC NVARCHAR(10) PRIMARY KEY,
    TenNCC NVARCHAR(100),
    SoDienThoai NVARCHAR(20),
    Email NVARCHAR(100)
);

-- 5. Bảng Kho hàng
CREATE TABLE KHOHANG (
    MaKhoHang NVARCHAR(10) PRIMARY KEY,
    TenSP NVARCHAR(100) NOT NULL,
    HangSX NVARCHAR(100),
    XuatXu NVARCHAR(100),
    SoLuong INT NOT NULL DEFAULT 0,
    GiaNhap DECIMAL(18, 2),
    NgayNhap DATE NOT NULL,
    MaNCC NVARCHAR(10),
    FOREIGN KEY (MaNCC) REFERENCES NHACUNGCAP(MaNCC)
);

-- 6. Bảng Sản phẩm
CREATE TABLE SANPHAM (
    MaSP NVARCHAR(10) PRIMARY KEY,
    MaKhoHang NVARCHAR(10),
    TenSP NVARCHAR(100) NOT NULL,
    HangSX NVARCHAR(100),
    XuatXu NVARCHAR(100),
    SoLuong INT NOT NULL DEFAULT 0,
    GiaBan DECIMAL(18, 2),
    MaNCC NVARCHAR(10),
    FOREIGN KEY (MaKhoHang) REFERENCES KHOHANG(MaKhoHang),
    FOREIGN KEY (MaNCC) REFERENCES NHACUNGCAP(MaNCC)
);

-- Tạo bảng KHACHHANG (nếu chưa có)

    CREATE TABLE KHACHHANG (
        MaKH NVARCHAR(10) PRIMARY KEY, -- Mã khách hàng, khóa chính
        HoTen NVARCHAR(100), -- Họ tên khách hàng
        SoDienThoai NVARCHAR(20), -- Số điện thoại
        DiaChi NVARCHAR(200), -- Địa chỉ
        Email NVARCHAR(100) -- Email
    );

-- Tạo bảng HOADON (Hóa đơn)
CREATE TABLE HOADON (
    MaHoaDon NVARCHAR(10) PRIMARY KEY, -- Mã hóa đơn, khóa chính
    MaKH NVARCHAR(10), -- Mã khách hàng
    MaNV NVARCHAR(10), -- Mã nhân viên bán hàng
    NgayBan DATE NOT NULL, -- Ngày bán
    TongTien DECIMAL(18, 2) DEFAULT 0, -- Tổng tiền hóa đơn
    FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH), -- Ràng buộc khóa ngoại với KHACHHANG
    FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV) -- Ràng buộc khóa ngoại với NHANVIEN
);
GO

-- Tạo bảng CHITIETHOADON (Chi tiết hóa đơn)
CREATE TABLE CHITIETHOADON (
    MaHoaDon NVARCHAR(10), -- Mã hóa đơn
    MaSP NVARCHAR(10), -- Mã sản phẩm
    SoLuong INT NOT NULL, -- Số lượng mua
    DonGia DECIMAL(18, 2), -- Đơn giá tại thời điểm mua
    ThanhTien DECIMAL(18, 2), -- Thành tiền (SoLuong * DonGia)
    PRIMARY KEY (MaHoaDon, MaSP), -- Khóa chính là tổ hợp MaHoaDon và MaSP
    FOREIGN KEY (MaHoaDon) REFERENCES HOADON(MaHoaDon), -- Ràng buộc khóa ngoại với HOADON
    FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP) -- Ràng buộc khóa ngoại với SANPHAM
);
GO
INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, LoaiTaiKhoan) VALUES
('nv01', '123456', 'nhanvien'),
('nv02', '123456', 'nhanvien'),
('nv03', '123456', 'nhanvien'),
('nv04', '123456', 'nhanvien'),
('nv05', '123456', 'nhanvien'),
('nv06', '123456', 'nhanvien'),
('nv07', '123456', 'nhanvien'),
('nv08', '123456', 'nhanvien'),
('nv09', '123456', 'nhanvien'),
('nv10', '123456', 'nhanvien'),
('nv11', '123456', 'nhanvien'),
('nv12', '123456', 'nhanvien');

INSERT INTO NHANVIEN (MaNV, HoTen, SoDienThoai, QueQuan, Email, TenDangNhap, MatKhau) VALUES
('NV01', N'Nguyễn Văn A', '0901111222', N'Hà Nội', 'nva1@example.com', 'nv01', '123456'),
('NV02', N'Lê Thị B', '0902222333', N'Đà Nẵng', 'ltb2@example.com', 'nv02', '123456'),
('NV03', N'Trần Văn C', '0903333444', N'Hồ Chí Minh', 'tvc3@example.com', 'nv03', '123456'),
('NV04', N'Phạm Thị D', '0904444555', N'Hải Phòng', 'ptd4@example.com', 'nv04', '123456'),
('NV05', N'Đỗ Văn E', '0905555666', N'Cần Thơ', 'dve5@example.com', 'nv05', '123456'),
('NV06', N'Bùi Thị F', '0906666777', N'Bình Dương', 'btf6@example.com', 'nv06', '123456'),
('NV07', N'Hoàng Văn G', '0907777888', N'Bắc Giang', 'hvg7@example.com', 'nv07', '123456'),
('NV08', N'Vũ Thị H', '0908888999', N'Nam Định', 'vth8@example.com', 'nv08', '123456'),
('NV09', N'Ngô Văn I', '0909999000', N'Nghệ An', 'nvi9@example.com', 'nv09', '123456'),
('NV10', N'Tạ Thị K', '0910000111', N'Thái Bình', 'ttk10@example.com', 'nv10', '123456'),
('NV11', N'Dương Văn L', '0911111222', N'Lào Cai', 'dvl11@example.com', 'nv11', '123456'),
('NV12', N'Phan Thị M', '0912222333', N'Tuyên Quang', 'ptm12@example.com', 'nv12', '123456');

INSERT INTO NHACUNGCAP (MaNCC, TenNCC, SoDienThoai, Email) VALUES
('NCC01', N'Công ty Thiên Long', '0901111222', 'thienlong@ncc.vn'),
('NCC02', N'Công ty An Phát', '0902222333', 'anphat@ncc.vn'),
('NCC03', N'Công ty Bút Bi', '0903333444', 'butbi@ncc.vn'),
('NCC04', N'Công ty Văn Phòng Xanh', '0904444555', 'vpgreen@ncc.vn'),
('NCC05', N'Công ty Thành Đạt', '0905555666', 'thanhdat@ncc.vn'),
('NCC06', N'Công ty Minh Quân', '0906666777', 'minhquan@ncc.vn'),
('NCC07', N'Công ty Đỉnh Cao', '0907777888', 'dinhcao@ncc.vn'),
('NCC08', N'Công ty Đại Dương', '0908888999', 'daiduong@ncc.vn'),
('NCC09', N'Công ty Ánh Dương', '0909999000', 'anhduong@ncc.vn'),
('NCC10', N'Công ty Bình Minh', '0910000111', 'binhminh@ncc.vn'),
('NCC11', N'Công ty Sao Mai', '0911111222', 'saomai@ncc.vn'),
('NCC12', N'Công ty Phương Nam', '0912222333', 'phuongnam@ncc.vn'),
('NCC13', N'Công ty Bạch Hổ', '0913333444', 'bachho@ncc.vn'),
('NCC14', N'Công ty Trí Tuệ', '0914444555', 'tritue@ncc.vn'),
('NCC15', N'Công ty Hoàng Gia', '0915555666', 'hoanggia@ncc.vn'),
('NCC16', N'Công ty Hưng Thịnh', '0916666777', 'hungthinh@ncc.vn'),
('NCC17', N'Công ty Đông Nam', '0917777888', 'dongnam@ncc.vn'),
('NCC18', N'Công ty Long Phát', '0918888999', 'longphat@ncc.vn'),
('NCC19', N'Công ty Tiến Lên', '0919999000', 'tienlen@ncc.vn'),
('NCC20', N'Công ty Việt Nhật', '0920000111', 'vietnhat@ncc.vn');

USE master;
GO

SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'KHOHANG';

ALTER DATABASE quanlycuahanglinhkiendienthoai 
SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE quanlycuahanglinhkiendienthoai;
GO

SELECT * FROM KHOHANG;