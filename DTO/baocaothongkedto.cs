using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SanPhamThongKeDTO
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int TongSoLuongBan { get; set; }
    }

    public class NhanVienThongKeDTO
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public int TongSoLuongBan { get; set; }
    }


    public class HoaDonThongKeDTO
    {
        public string MaHD { get; set; }
        public string MaKH { get; set; }
        public string MaNV { get; set; }
        public DateTime NgayBan { get; set; }
    }

    public class DoanhThuDTO
    {
        public decimal TongTien { get; set; }
    }

}
