using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class khachhangdal
    {
        private string connectionString = @"Data Source=LAPTOP-PMLRDEHN\MAY1;Initial Catalog=quanlycuahanglinhkiendienthoai;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public List<khachhangdto> GetAllKhachHang()
        {
            var list = new List<khachhangdto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM KHACHHANG";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new khachhangdto
                            {
                                MaKH = reader["MaKH"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool AddKhachHang(khachhangdto kh)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Auto-generate MaKH
                kh.MaKH = GenerateMaKH();

                // Insert into KHACHHANG
                SqlCommand insertKH = new SqlCommand(
                    "INSERT INTO KHACHHANG (MaKH, HoTen, SoDienThoai, DiaChi, Email) " +
                    "VALUES (@Ma, @Ten, @SDT, @DC, @Email)", conn);

                insertKH.Parameters.AddWithValue("@Ma", kh.MaKH);
                insertKH.Parameters.AddWithValue("@Ten", kh.HoTen);
                insertKH.Parameters.AddWithValue("@SDT", kh.SoDienThoai);
                insertKH.Parameters.AddWithValue("@DC", kh.DiaChi);
                insertKH.Parameters.AddWithValue("@Email", kh.Email);

                insertKH.ExecuteNonQuery();
                return true;
            }
        }

        public string GenerateMaKH()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(MaKH, 3, LEN(MaKH) - 2) AS INT)), 0) + 1 FROM KHACHHANG";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int nextId = (int)cmd.ExecuteScalar();
                    return "KH" + nextId.ToString("D2");
                }
            }
        }

        public bool UpdateKhachHang(khachhangdto kh)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE KHACHHANG SET HoTen = @Ten, SoDienThoai = @SDT, DiaChi = @DC, Email = @Email WHERE MaKH = @Ma";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", kh.MaKH);
                    cmd.Parameters.AddWithValue("@Ten", kh.HoTen);
                    cmd.Parameters.AddWithValue("@SDT", kh.SoDienThoai);
                    cmd.Parameters.AddWithValue("@DC", kh.DiaChi);
                    cmd.Parameters.AddWithValue("@Email", kh.Email);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteKhachHang(string maKH)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM KHACHHANG WHERE MaKH = @Ma";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", maKH);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<khachhangdto> SearchKhachHang(string keyword)
        {
            var list = new List<khachhangdto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM KHACHHANG WHERE MaKH LIKE @Keyword OR HoTen LIKE @Keyword OR SoDienThoai LIKE @Keyword OR DiaChi LIKE @Keyword OR Email LIKE @Keyword";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new khachhangdto
                            {
                                MaKH = reader["MaKH"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
