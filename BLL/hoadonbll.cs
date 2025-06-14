using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class hoadonbll
    {
            private hoadondal _hoadonDAL;

            public hoadonbll()
            {
                _hoadonDAL = new hoadondal();
            }

            public List<hoadondto> GetAll()
            {
                return _hoadonDAL.GetAll();
            }

            public void Add(hoadondto hd)
            {
                _hoadonDAL.Add(hd);
            }

            public void Update(hoadondto hd)
            {
                _hoadonDAL.Update(hd);
            }

            public void Delete(string mahd)
            {
                _hoadonDAL.Delete(mahd);
            }

            public hoadondto GetByMaHD(string mahd)
            {
                return _hoadonDAL.GetByMaHD(mahd);
            }
        }
    }