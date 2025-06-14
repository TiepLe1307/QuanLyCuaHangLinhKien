using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class nhacungcapbll
    {
        private nhacungcapdal _dal = new nhacungcapdal();

        public List<nhacungcapdto> GetAll() => _dal.GetAll();

        public void Add(nhacungcapdto ncc) => _dal.Add(ncc);

        public void Update(nhacungcapdto ncc) => _dal.Update(ncc);

        public void Delete(string maNCC) => _dal.Delete(maNCC);

        public List<nhacungcapdto> Search(string keyword) => _dal.Search(keyword);
    }
}
