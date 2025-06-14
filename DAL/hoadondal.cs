using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class hoadondal
    {
        private string connectionString = @"Data Source=LAPTOP-PMLRDEHN\MAY1;Initial Catalog=quanlycuahanglinhkiendienthoai;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public List<hoadondto> GetAll()
        {
            List<hoadondto> list = new List<hoadondto>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM HOADON";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        hoadondto hd = new hoadondto
                        {
                            MaHD = reader["MaHD"].ToString(),
                            MaKH = reader["MaKH"].ToString(),
                            MaNV = reader["MaNV"].ToString(),
                            NgayLap = Convert.ToDateTime(reader["NgayLap"])
                        };
                        list.Add(hd);
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw; // ném lỗi ra để UI xử lý
            }

            return list;
        }

        public void Add(hoadondto hd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO HOADON (MaHD, MaKH, MaNV, NgayLap) VALUES (@MaHD, @MaKH, @MaNV, @NgayLap)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", hd.MaHD);
                cmd.Parameters.AddWithValue("@MaKH", hd.MaKH);
                cmd.Parameters.AddWithValue("@MaNV", hd.MaNV);
                cmd.Parameters.AddWithValue("@NgayLap", hd.NgayLap);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(hoadondto hd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE HOADON SET MaKH=@MaKH, MaNV=@MaNV, NgayLap=@NgayLap WHERE MaHD=@MaHD";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKH", hd.MaKH);
                cmd.Parameters.AddWithValue("@MaNV", hd.MaNV);
                cmd.Parameters.AddWithValue("@NgayLap", hd.NgayLap);
                cmd.Parameters.AddWithValue("@MaHD", hd.MaHD);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string mahd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM HOADON WHERE MaHD = @MaHD";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", mahd);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public hoadondto GetByMaHD(string mahd)
        {
            hoadondto hd = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM HOADON WHERE MaHD = @MaHD";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", mahd);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    hd = new hoadondto
                    {
                        MaHD = reader["MaHD"].ToString(),
                        MaKH = reader["MaKH"].ToString(),
                        MaNV = reader["MaNV"].ToString(),
                        NgayLap = Convert.ToDateTime(reader["NgayLap"])
                    };
                }
                reader.Close();
            }
            return hd;
        }
    }
}
