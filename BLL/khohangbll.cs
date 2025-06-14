using System;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class khohangbll
    {
        private khohangdal _khohangDAL;

        public khohangbll()
        {
            _khohangDAL = new khohangdal();
        }

        public List<khohangdto> GetAll()
        {
            return _khohangDAL.GetAll();
        }

        public void Add(khohangdto kho)
        {
            _khohangDAL.Add(kho);
        }

        public void Update(khohangdto kho)
        {
            _khohangDAL.Update(kho);
        }

        public void Delete(string maKhoHang)
        {
            _khohangDAL.Delete(maKhoHang);
        }

        public List<khohangdto> GetAllKhoHang()
        {
            return _khohangDAL.GetAllKhoHang();
        }

        // Thêm phương thức lấy kho hàng theo mã
        public khohangdto GetKhoHangByMa(string maKhoHang)
        {
            return _khohangDAL.GetKhoHangByMa(maKhoHang);
        }
    }
}
