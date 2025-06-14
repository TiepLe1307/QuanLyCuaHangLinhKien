using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class sanphambll
    {
        private sanphamdal _sanphamDAL;

        public sanphambll()
        {
            _sanphamDAL = new sanphamdal();
        }

        public List<sanphamdto> GetAllSanPham()
        {
            return _sanphamDAL.GetAllSanPham();
        }

        public bool AddSanPham(sanphamdto sp)
        {
            return _sanphamDAL.AddSanPham(sp);
        }

        public string GenerateMaSP()
        {
            return _sanphamDAL.GenerateMaSP();
        }

        public bool UpdateSanPham(sanphamdto sp)
        {
            return _sanphamDAL.UpdateSanPham(sp);
        }

        public List<sanphamdto> SearchSanPham(string keyword)
        {
            return _sanphamDAL.SearchSanPham(keyword);
        }

        public bool DeleteSanPham(string maSP)
        {
            return _sanphamDAL.DeleteSanPham(maSP);
        }

        public bool IsMaKhoHangExists(string maKhoHang)
        {
            return _sanphamDAL.IsMaKhoHangExists(maKhoHang);
        }
    }
}
