using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class baohanhdal
    {
        private string connectionString = @"Data Source=LAPTOP-PMLRDEHN\MAY1;Initial Catalog=quanlycuahanglinhkiendienthoai;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public List<baohanhdto> GetAllBaoHanh()
        {
            var list = new List<baohanhdto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM BAOHANH";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new baohanhdto
                            {
                                MaBH = reader["MaBH"].ToString(),
                                MaSP = reader["MaSP"].ToString(),
                                ThoiGianBaoHanh = Convert.ToInt32(reader["ThoiGianBaoHanh"]),
                                NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                                NgayKetThuc = Convert.ToDateTime(reader["NgayKetThuc"]),
                                LyDoBaoHanh = reader["LyDoBaoHanh"].ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                ChiPhiBaoHanh = reader["ChiPhiBaoHanh"] as decimal?,
                                MoTaLoi = reader["MoTaLoi"].ToString(),
                                MaKH = reader["MaKH"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool AddBaoHanh(baohanhdto bh)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO BAOHANH (MaBH, MaSP, ThoiGianBaoHanh, NgayBatDau, NgayKetThuc, LyDoBaoHanh, TrangThai, ChiPhiBaoHanh, MoTaLoi, MaKH) VALUES (@MaBH, @MaSP, @ThoiGianBaoHanh, @NgayBatDau, @NgayKetThuc, @LyDoBaoHanh, @TrangThai, @ChiPhiBaoHanh, @MoTaLoi, @MaKH)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaBH", bh.MaBH);
                    cmd.Parameters.AddWithValue("@MaSP", bh.MaSP);
                    cmd.Parameters.AddWithValue("@ThoiGianBaoHanh", bh.ThoiGianBaoHanh);
                    cmd.Parameters.AddWithValue("@NgayBatDau", bh.NgayBatDau);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", bh.NgayKetThuc);
                    cmd.Parameters.AddWithValue("@LyDoBaoHanh", bh.LyDoBaoHanh);
                    cmd.Parameters.AddWithValue("@TrangThai", bh.TrangThai);
                    cmd.Parameters.AddWithValue("@ChiPhiBaoHanh", bh.ChiPhiBaoHanh ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MoTaLoi", bh.MoTaLoi);
                    cmd.Parameters.AddWithValue("@MaKH", bh.MaKH);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateBaoHanh(baohanhdto bh)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE BAOHANH SET MaSP = @MaSP, ThoiGianBaoHanh = @ThoiGianBaoHanh, NgayBatDau = @NgayBatDau, NgayKetThuc = @NgayKetThuc, LyDoBaoHanh = @LyDoBaoHanh, TrangThai = @TrangThai, ChiPhiBaoHanh = @ChiPhiBaoHanh, MoTaLoi = @MoTaLoi, MaKH = @MaKH WHERE MaBH = @MaBH";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaBH", bh.MaBH);
                    cmd.Parameters.AddWithValue("@MaSP", bh.MaSP);
                    cmd.Parameters.AddWithValue("@ThoiGianBaoHanh", bh.ThoiGianBaoHanh);
                    cmd.Parameters.AddWithValue("@NgayBatDau", bh.NgayBatDau);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", bh.NgayKetThuc);
                    cmd.Parameters.AddWithValue("@LyDoBaoHanh", bh.LyDoBaoHanh);
                    cmd.Parameters.AddWithValue("@TrangThai", bh.TrangThai);
                    cmd.Parameters.AddWithValue("@ChiPhiBaoHanh", bh.ChiPhiBaoHanh ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MoTaLoi", bh.MoTaLoi);
                    cmd.Parameters.AddWithValue("@MaKH", bh.MaKH);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteBaoHanh(string maBH)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM BAOHANH WHERE MaBH = @MaBH";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaBH", maBH);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
