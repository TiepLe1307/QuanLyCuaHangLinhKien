using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class TaiKhoanDAL
    {
        private string connectionString = @"Data Source=LAPTOP-PMLRDEHN\MAY1;Initial Catalog=quanlycuahanglinhkiendienthoai;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public TaiKhoanDTO DangNhap(TaiKhoanDTO taiKhoan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @username AND MatKhau = @password";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", taiKhoan.TenDangNhap);
                        cmd.Parameters.AddWithValue("@password", taiKhoan.MatKhau);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TaiKhoanDTO tk = new TaiKhoanDTO
                                {
                                    TenDangNhap = reader["TenDangNhap"].ToString(),
                                    MatKhau = reader["MatKhau"].ToString(),
                                    LoaiTaiKhoan = reader["LoaiTaiKhoan"].ToString()
                                };
                                return tk;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi DAL DangNhap: " + ex.Message);
            }

            return null;
        }
        public List<TaiKhoanDTO> LayDanhSachTaiKhoan()
        {
            List<TaiKhoanDTO> danhSachTaiKhoan = new List<TaiKhoanDTO>();

            string query = "SELECT TenDangNhap, MatKhau, LoaiTaiKhoan FROM TaiKhoan";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                        {
                            TenDangNhap = reader["TenDangNhap"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            LoaiTaiKhoan = reader["LoaiTaiKhoan"].ToString()
                        };
                        danhSachTaiKhoan.Add(taiKhoan);
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lại thông tin lỗi vào log
                System.Diagnostics.Debug.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                throw new Exception("Lỗi khi truy vấn dữ liệu: " + ex.Message);  // Ném lỗi lên để dễ dàng bắt ở các lớp cao hơn
            }

            return danhSachTaiKhoan;
        }

        public bool ThemTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, LoaiTaiKhoan) VALUES (@TenDangNhap, @MatKhau, @LoaiTaiKhoan)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
                        cmd.Parameters.AddWithValue("@LoaiTaiKhoan", taiKhoan.LoaiTaiKhoan);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi DAL ThemTaiKhoan: " + ex.Message);
                return false;
            }
        }
        public bool SuaTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE TaiKhoan SET MatKhau = @MatKhau, LoaiTaiKhoan = @LoaiTaiKhoan WHERE TenDangNhap = @TenDangNhap";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
                        cmd.Parameters.AddWithValue("@LoaiTaiKhoan", taiKhoan.LoaiTaiKhoan);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi DAL SuaTaiKhoan: " + ex.Message);
                return false;
            }
        }
        public bool XoaTaiKhoan(string tenDangNhap)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi DAL XoaTaiKhoan: " + ex.Message);
                return false;
            }
        }
        public List<TaiKhoanDTO> TimKiemTaiKhoan(string tenDangNhap)
        {
            List<TaiKhoanDTO> taiKhoans = new List<TaiKhoanDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM TaiKhoan WHERE TenDangNhap LIKE @TenDangNhap";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", "%" + tenDangNhap + "%");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                                {
                                    TenDangNhap = reader["TenDangNhap"].ToString(),
                                    MatKhau = reader["MatKhau"].ToString(),
                                    LoaiTaiKhoan = reader["LoaiTaiKhoan"].ToString()
                                };
                                taiKhoans.Add(taiKhoan);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi DAL TimKiemTaiKhoan: " + ex.Message);
            }
            return taiKhoans;
        }




    }
}
