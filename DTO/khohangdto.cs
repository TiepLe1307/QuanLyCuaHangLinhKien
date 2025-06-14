using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class khohangdto
    {
        public string MaKhoHang { get; set; }
        public string TenSP { get; set; } // Đổi từ TenSanPham thành TenSP
        public string HangSX { get; set; } // Đổi từ HangSanXuat thành HangSX
        public string XuatXu { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public string MaNCC { get; set; }
        public DateTime NgayNhap { get; set; }
    }
}
