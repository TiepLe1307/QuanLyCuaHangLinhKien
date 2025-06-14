using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class chitiethoadonbll
    {
        private chitiethoadondal _dal = new chitiethoadondal();

        public List<chitiethoadondto> GetAll()
        {
            return _dal.GetAll();
        }

        public void Add(chitiethoadondto cthd)
        {
            _dal.Add(cthd);
        }

        public void Update(chitiethoadondto cthd)
        {
            _dal.Update(cthd);
        }

        public void Delete(string mahd, string masp)
        {
            _dal.Delete(mahd, masp);
        }
    }
}

