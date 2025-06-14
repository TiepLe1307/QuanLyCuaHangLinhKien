using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class nhanvienbll
    {
        private nhanviendal _nhanvienDAL;

        public nhanvienbll()
        {
            _nhanvienDAL = new nhanviendal();
        }

        public List<nhanviendto> GetAllNhanVien()
        {
            return _nhanvienDAL.GetAllNhanVien();
        }

        public bool AddNhanVien(nhanviendto nv)
        {
            nv.MaNV = _nhanvienDAL.GenerateMaNV();
            return _nhanvienDAL.AddNhanVien(nv);
        }

        public bool UpdateNhanVien(nhanviendto nv)
        {
            return _nhanvienDAL.UpdateNhanVien(nv);
        }

        public bool DeleteNhanVien(string maNV)
        {
            return _nhanvienDAL.DeleteNhanVien(maNV);
        }

        public List<nhanviendto> SearchNhanVien(string keyword)
        {
            return _nhanvienDAL.SearchNhanVien(keyword);
        }

        public string GenerateMaNV()
        {
            return _nhanvienDAL.GenerateMaNV();
        }
    }
}
