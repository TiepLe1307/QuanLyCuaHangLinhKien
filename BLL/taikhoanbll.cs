    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL;
    using DTO;

    namespace BLL
    {
        public class TaiKhoanBLL
        {
            private TaiKhoanDAL taiKhoanDAL;

            public TaiKhoanBLL()
            {
                taiKhoanDAL = new TaiKhoanDAL();
            }

            public TaiKhoanDTO DangNhap(string tenDangNhap, string matKhau)
            {
                if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
                    return null;

                TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                {
                    TenDangNhap = tenDangNhap,
                    MatKhau = matKhau
                };

                return taiKhoanDAL.DangNhap(taiKhoan);
            }
        public List<TaiKhoanDTO> LayDanhSachTaiKhoan()
        {
            return new TaiKhoanDAL().LayDanhSachTaiKhoan();
        }
        public bool ThemTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            return taiKhoanDAL.ThemTaiKhoan(taiKhoan);
        }
        public bool SuaTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            return taiKhoanDAL.SuaTaiKhoan(taiKhoan);
        }

        public bool XoaTaiKhoan(string tenDangNhap)
        {
            return taiKhoanDAL.XoaTaiKhoan(tenDangNhap);
        }

        public List<TaiKhoanDTO> TimKiemTaiKhoan(string tenDangNhap)
        {
            return taiKhoanDAL.TimKiemTaiKhoan(tenDangNhap);
        }

    }
}
