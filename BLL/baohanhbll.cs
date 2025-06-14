using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class baohanhbll
    {
        private baohanhdal _baohanhDAL;

        public baohanhbll()
        {
            _baohanhDAL = new baohanhdal();
        }

        public List<baohanhdto> GetAllBaoHanh()
        {
            return _baohanhDAL.GetAllBaoHanh();
        }

        public bool AddBaoHanh(baohanhdto bh)
        {
            return _baohanhDAL.AddBaoHanh(bh);
        }

        public bool UpdateBaoHanh(baohanhdto bh)
        {
            return _baohanhDAL.UpdateBaoHanh(bh);
        }

        public bool DeleteBaoHanh(string maBH)
        {
            return _baohanhDAL.DeleteBaoHanh(maBH);
        }
    }
}
