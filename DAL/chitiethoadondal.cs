using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class chitiethoadondal
    {
        private string connectionString = @"Data Source=LAPTOP-PMLRDEHN\MAY1;Initial Catalog=quanlycuahanglinhkiendienthoai;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public List<chitiethoadondto> GetAll()
        {
            List<chitiethoadondto> list = new List<chitiethoadondto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CHITIETHOADON";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    chitiethoadondto cthd = new chitiethoadondto
                    {
                        MaHD = reader["MaHD"].ToString(),
                        MaSP = reader["MaSP"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        DonGia = Convert.ToDecimal(reader["DonGia"]),
                    };
                    list.Add(cthd);
                }
            }
            return list;
        }

        public void Add(chitiethoadondto cthd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO CHITIETHOADON (MaHD, MaSP, SoLuong, DonGia) VALUES (@MaHD, @MaSP, @SoLuong, @DonGia)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", cthd.MaHD);
                cmd.Parameters.AddWithValue("@MaSP", cthd.MaSP);
                cmd.Parameters.AddWithValue("@SoLuong", cthd.SoLuong);
                cmd.Parameters.AddWithValue("@DonGia", cthd.DonGia);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(chitiethoadondto cthd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE CHITIETHOADON SET SoLuong=@SoLuong, DonGia=@DonGia WHERE MaHD=@MaHD AND MaSP=@MaSP";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SoLuong", cthd.SoLuong);
                cmd.Parameters.AddWithValue("@DonGia", cthd.DonGia);
                cmd.Parameters.AddWithValue("@MaHD", cthd.MaHD);
                cmd.Parameters.AddWithValue("@MaSP", cthd.MaSP);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string mahd, string masp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM CHITIETHOADON WHERE MaHD = @MaHD AND MaSP = @MaSP";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", mahd);
                cmd.Parameters.AddWithValue("@MaSP", masp);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}