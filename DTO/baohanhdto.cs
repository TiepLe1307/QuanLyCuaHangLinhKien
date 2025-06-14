using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class baohanhdto
    {
        public string MaBH { get; set; }
        public string MaSP { get; set; }
        public int ThoiGianBaoHanh { get; set; }  // Thời gian bảo hành tính bằng tháng
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string LyDoBaoHanh { get; set; }
        public string TrangThai { get; set; }
        public decimal? ChiPhiBaoHanh { get; set; }
        public string MoTaLoi { get; set; }
        public string MaKH { get; set; }
    }
}
