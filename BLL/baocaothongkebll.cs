using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data.SqlClient;

namespace BLL
{
    public class BaoCaoThongKeBLL
    {
        BaoCaoThongKeDAL dal = new BaoCaoThongKeDAL();

        public List<SanPhamThongKeDTO> GetTop3SanPhamBanChay(DateTime tuNgay, DateTime denNgay)
        {
            return dal.GetTop3SanPhamBanChay(tuNgay, denNgay);
        }

        public List<SanPhamThongKeDTO> GetTop3SanPhamBanIt(DateTime tuNgay, DateTime denNgay)
        {
            return dal.GetTop3SanPhamBanIt(tuNgay, denNgay);
        }

        public List<NhanVienThongKeDTO> GetTop3NhanVienBanChay(DateTime tuNgay, DateTime denNgay)
        {
            return dal.GetTop3NhanVienBanChay(tuNgay, denNgay);
        }

        public List<HoaDonThongKeDTO> GetHoaDon(DateTime tuNgay, DateTime denNgay)
        {
            return dal.GetHoaDon(tuNgay, denNgay);
        }

        public DoanhThuDTO GetTongDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            return dal.GetTongDoanhThu(tuNgay, denNgay);
        }
    }

}
