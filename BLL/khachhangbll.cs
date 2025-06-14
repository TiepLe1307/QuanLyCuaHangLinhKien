using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;


namespace BLL
{
    public class khachhangbll
    {
        private khachhangdal _khachhangDAL;

        public khachhangbll()
        {
            _khachhangDAL = new khachhangdal();
        }

        public List<khachhangdto> GetAllKhachHang()
        {
            return _khachhangDAL.GetAllKhachHang();
        }

        public bool AddKhachHang(khachhangdto kh)
        {
            kh.MaKH = _khachhangDAL.GenerateMaKH();
            return _khachhangDAL.AddKhachHang(kh);
        }

        public bool UpdateKhachHang(khachhangdto kh)
        {
            return _khachhangDAL.UpdateKhachHang(kh);
        }

        public bool DeleteKhachHang(string maKH)
        {
            return _khachhangDAL.DeleteKhachHang(maKH);
        }

        public List<khachhangdto> SearchKhachHang(string keyword)
        {
            return _khachhangDAL.SearchKhachHang(keyword);
        }
    }
}
