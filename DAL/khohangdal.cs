using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class khohangdal
    {
        private string connectionString = @"Data Source=LAPTOP-PMLRDEHN\MAY1;Initial Catalog=quanlycuahanglinhkiendienthoai;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public List<khohangdto> GetAll()
        {
            List<khohangdto> danhSachKho = new List<khohangdto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaKhoHang, TenSP, HangSX, XuatXu, SoLuong, GiaNhap, MaNCC, NgayNhap FROM KHOHANG";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        khohangdto kho = new khohangdto
                        {
                            MaKhoHang = reader["MaKhoHang"].ToString(),
                            TenSP = reader["TenSP"].ToString(),
                            HangSX = reader["HangSX"].ToString(),
                            XuatXu = reader["XuatXu"].ToString(),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            GiaNhap = Convert.ToDecimal(reader["GiaNhap"]),
                            MaNCC = reader["MaNCC"].ToString(),
                            NgayNhap = Convert.ToDateTime(reader["NgayNhap"])
                        };
                        danhSachKho.Add(kho);
                    }
                }
            }
            return danhSachKho;
        }

        public List<khohangdto> GetAllKhoHang()
        {
            var list = new List<khohangdto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM KHOHANG";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new khohangdto
                            {
                                MaKhoHang = reader["MaKhoHang"].ToString(),
                                TenSP = reader["TenSP"].ToString(),
                                HangSX = reader["HangSX"].ToString(),
                                XuatXu = reader["XuatXu"].ToString(),
                                SoLuong = int.Parse(reader["SoLuong"].ToString()),
                                GiaNhap = decimal.Parse(reader["GiaNhap"].ToString()),
                                NgayNhap = DateTime.Parse(reader["NgayNhap"].ToString())
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool IsMaKhoHangExists(string maKhoHang)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM KHOHANG WHERE MaKhoHang = @MaKhoHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKhoHang", maKhoHang);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public void Add(khohangdto kho)
        {
            if (IsMaKhoHangExists(kho.MaKhoHang))
            {
                throw new Exception("Mã Kho Hàng đã tồn tại. Vui lòng sử dụng mã khác.");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO KHOHANG (MaKhoHang, TenSP, HangSX, XuatXu, SoLuong, GiaNhap, MaNCC, NgayNhap) VALUES (@MaKhoHang, @TenSP, @HangSX, @XuatXu, @SoLuong, @GiaNhap, @MaNCC, @NgayNhap)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKhoHang", kho.MaKhoHang);
                cmd.Parameters.AddWithValue("@TenSP", kho.TenSP);
                cmd.Parameters.AddWithValue("@HangSX", kho.HangSX);
                cmd.Parameters.AddWithValue("@XuatXu", kho.XuatXu);
                cmd.Parameters.AddWithValue("@SoLuong", kho.SoLuong);
                cmd.Parameters.AddWithValue("@GiaNhap", kho.GiaNhap);
                cmd.Parameters.AddWithValue("@MaNCC", kho.MaNCC);
                cmd.Parameters.AddWithValue("@NgayNhap", kho.NgayNhap);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(khohangdto kho)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE KHOHANG SET TenSP = @TenSP, HangSX = @HangSX, XuatXu = @XuatXu, SoLuong = @SoLuong, GiaNhap = @GiaNhap, MaNCC = @MaNCC, NgayNhap = @NgayNhap WHERE MaKhoHang = @MaKhoHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKhoHang", kho.MaKhoHang);
                cmd.Parameters.AddWithValue("@TenSP", kho.TenSP);
                cmd.Parameters.AddWithValue("@HangSX", kho.HangSX);
                cmd.Parameters.AddWithValue("@XuatXu", kho.XuatXu);
                cmd.Parameters.AddWithValue("@SoLuong", kho.SoLuong);
                cmd.Parameters.AddWithValue("@GiaNhap", kho.GiaNhap);
                cmd.Parameters.AddWithValue("@MaNCC", kho.MaNCC);
                cmd.Parameters.AddWithValue("@NgayNhap", kho.NgayNhap);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string maKhoHang)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM KHOHANG WHERE MaKhoHang = @MaKhoHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKhoHang", maKhoHang);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Thêm phương thức lấy kho hàng theo mã
        public khohangdto GetKhoHangByMa(string maKhoHang)
        {
            khohangdto kho = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaKhoHang, TenSP, HangSX, XuatXu, SoLuong, GiaNhap, MaNCC, NgayNhap FROM KHOHANG WHERE MaKhoHang = @MaKhoHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKhoHang", maKhoHang);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // Chỉ đọc một dòng nếu tìm thấy
                    {
                        kho = new khohangdto
                        {
                            MaKhoHang = reader["MaKhoHang"].ToString(),
                            TenSP = reader["TenSP"].ToString(),
                            HangSX = reader["HangSX"].ToString(),
                            XuatXu = reader["XuatXu"].ToString(),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            GiaNhap = Convert.ToDecimal(reader["GiaNhap"]),
                            MaNCC = reader["MaNCC"]?.ToString(), // Cho phép null nếu MaNCC có thể null
                            NgayNhap = Convert.ToDateTime(reader["NgayNhap"])
                        };
                    }
                }
            }
            return kho; // Trả về null nếu không tìm thấy
        }
    }
}
