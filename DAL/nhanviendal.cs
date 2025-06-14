using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class nhanviendal
    {
        private string connectionString = @"Data Source=LAPTOP-PMLRDEHN\MAY1;Initial Catalog=quanlycuahanglinhkiendienthoai;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public List<nhanviendto> GetAllNhanVien()
        {
            var list = new List<nhanviendto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM NHANVIEN";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new nhanviendto
                            {
                                MaNV = reader["MaNV"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                QueQuan = reader["QueQuan"].ToString(),
                                Email = reader["Email"].ToString(),
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhau = reader["MatKhau"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool AddNhanVien(nhanviendto nv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if account exists
                SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM TAIKHOAN WHERE TenDangNhap = @tk", conn);
                check.Parameters.AddWithValue("@tk", nv.TenDangNhap);
                int exist = (int)check.ExecuteScalar();

                if (exist == 0)
                {
                    // Insert into TAIKHOAN
                    SqlCommand insertTK = new SqlCommand("INSERT INTO TAIKHOAN VALUES (@tk, @mk, 'nhanvien')", conn);
                    insertTK.Parameters.AddWithValue("@tk", nv.TenDangNhap);
                    insertTK.Parameters.AddWithValue("@mk", nv.MatKhau);
                    insertTK.ExecuteNonQuery();
                }
                else
                {
                    return false; // Account already exists
                }

                // Auto-generate MaNV
                nv.MaNV = GenerateMaNV();

                // Insert into NHANVIEN
                SqlCommand insertNV = new SqlCommand(
                    "INSERT INTO NHANVIEN (MaNV, HoTen, SoDienThoai, QueQuan, Email, TenDangNhap, MatKhau) " +
                    "VALUES (@Ma, @Ten, @SDT, @QQ, @Email, @TK, @MK)", conn);

                insertNV.Parameters.AddWithValue("@Ma", nv.MaNV);
                insertNV.Parameters.AddWithValue("@Ten", nv.HoTen);
                insertNV.Parameters.AddWithValue("@SDT", nv.SoDienThoai);
                insertNV.Parameters.AddWithValue("@QQ", nv.QueQuan);
                insertNV.Parameters.AddWithValue("@Email", nv.Email);
                insertNV.Parameters.AddWithValue("@TK", nv.TenDangNhap);
                insertNV.Parameters.AddWithValue("@MK", nv.MatKhau);

                insertNV.ExecuteNonQuery();
                return true;
            }
        }

        public string GenerateMaNV()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(MaNV, 3, LEN(MaNV) - 2) AS INT)), 0) + 1 FROM NHANVIEN";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int nextId = (int)cmd.ExecuteScalar();
                    return "NV" + nextId.ToString("D2");
                }
            }
        }

        public bool UpdateNhanVien(nhanviendto nv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE NHANVIEN SET HoTen = @Ten, SoDienThoai = @SDT, QueQuan = @QQ, Email = @Email, TenDangNhap = @TK, MatKhau = @MK WHERE MaNV = @Ma";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", nv.MaNV);
                    cmd.Parameters.AddWithValue("@Ten", nv.HoTen);
                    cmd.Parameters.AddWithValue("@SDT", nv.SoDienThoai);
                    cmd.Parameters.AddWithValue("@QQ", nv.QueQuan);
                    cmd.Parameters.AddWithValue("@Email", nv.Email);
                    cmd.Parameters.AddWithValue("@TK", nv.TenDangNhap);
                    cmd.Parameters.AddWithValue("@MK", nv.MatKhau);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteNhanVien(string maNV)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM NHANVIEN WHERE MaNV = @Ma";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", maNV);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<nhanviendto> SearchNhanVien(string keyword)
        {
            var list = new List<nhanviendto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM NHANVIEN WHERE MaNV LIKE @Keyword OR HoTen LIKE @Keyword OR SoDienThoai LIKE @Keyword OR QueQuan LIKE @Keyword OR Email LIKE @Keyword OR TenDangNhap LIKE @Keyword";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new nhanviendto
                            {
                                MaNV = reader["MaNV"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                QueQuan = reader["QueQuan"].ToString(),
                                Email = reader["Email"].ToString(),
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhau = reader["MatKhau"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
