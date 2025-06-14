using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class sanphamdal
    {
        private string connectionString = @"Data Source=LAPTOP-PMLRDEHN\MAY1;Initial Catalog=quanlycuahanglinhkiendienthoai;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public List<sanphamdto> GetAllSanPham()
        {
            var list = new List<sanphamdto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM SANPHAM";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new sanphamdto
                            {
                                MaSP = reader["MaSP"].ToString(),
                                MaKhoHang = reader["MaKhoHang"].ToString(),
                                MaNCC = reader["MaNCC"].ToString(),
                                TenSP = reader["TenSP"].ToString(),
                                HangSX = reader["HangSX"].ToString(),
                                XuatXu = reader["XuatXu"].ToString(),
                                SoLuong = Convert.ToInt32(reader["SoLuong"]),
                                GiaBan = Convert.ToDecimal(reader["GiaBan"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool AddSanPham(sanphamdto sp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO SANPHAM (MaSP, MaKhoHang, MaNCC, TenSP, HangSX, XuatXu, SoLuong, GiaBan) VALUES (@MaSP, @MaKhoHang, @MaNCC, @TenSP, @HangSX, @XuatXu, @SoLuong, @GiaBan)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                    cmd.Parameters.AddWithValue("@MaKhoHang", sp.MaKhoHang);
                    cmd.Parameters.AddWithValue("@MaNCC", sp.MaNCC);
                    cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                    cmd.Parameters.AddWithValue("@HangSX", sp.HangSX);
                    cmd.Parameters.AddWithValue("@XuatXu", sp.XuatXu);
                    cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                    cmd.Parameters.AddWithValue("@GiaBan", sp.GiaBan);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public string GenerateMaSP()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(MaSP, 3, LEN(MaSP) - 2) AS INT)), 0) + 1 FROM SANPHAM";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int nextId = (int)cmd.ExecuteScalar();
                    return "S" + nextId.ToString("D2");
                }
            }
        }

        // Thêm phương thức UpdateSanPham
        public bool UpdateSanPham(sanphamdto sp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE SANPHAM SET MaKhoHang = @MaKhoHang, MaNCC = @MaNCC, TenSP = @TenSP, HangSX = @HangSX, XuatXu = @XuatXu, SoLuong = @SoLuong, GiaBan = @GiaBan WHERE MaSP = @MaSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                    cmd.Parameters.AddWithValue("@MaKhoHang", sp.MaKhoHang);
                    cmd.Parameters.AddWithValue("@MaNCC", sp.MaNCC);
                    cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                    cmd.Parameters.AddWithValue("@HangSX", sp.HangSX);
                    cmd.Parameters.AddWithValue("@XuatXu", sp.XuatXu);
                    cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                    cmd.Parameters.AddWithValue("@GiaBan", sp.GiaBan);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Thêm phương thức SearchSanPham
        public List<sanphamdto> SearchSanPham(string keyword)
        {
            var list = new List<sanphamdto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM SANPHAM WHERE MaSP LIKE @Keyword OR MaKhoHang LIKE @Keyword OR MaNCC LIKE @Keyword OR TenSP LIKE @Keyword OR HangSX LIKE @Keyword OR XuatXu LIKE @Keyword";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new sanphamdto
                            {
                                MaSP = reader["MaSP"].ToString(),
                                MaKhoHang = reader["MaKhoHang"].ToString(),
                                MaNCC = reader["MaNCC"].ToString(),
                                TenSP = reader["TenSP"].ToString(),
                                HangSX = reader["HangSX"].ToString(),
                                XuatXu = reader["XuatXu"].ToString(),
                                SoLuong = Convert.ToInt32(reader["SoLuong"]),
                                GiaBan = Convert.ToDecimal(reader["GiaBan"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        // Thêm phương thức DeleteSanPham
        public bool DeleteSanPham(string maSP)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM SANPHAM WHERE MaSP = @MaSP";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Thêm phương thức IsMaKhoHangExists
        public bool IsMaKhoHangExists(string maKhoHang)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM SANPHAM WHERE MaKhoHang = @MaKhoHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKhoHang", maKhoHang);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
