using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class nhacungcapdal
    {
        private string connectionString = @"Data Source=LAPTOP-PMLRDEHN\MAY1;Initial Catalog=quanlycuahanglinhkiendienthoai;Integrated Security=True";

        public List<nhacungcapdto> GetAll()
        {
            var list = new List<nhacungcapdto>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM NHACUNGCAP", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new nhacungcapdto
                        {
                            MaNCC = reader["MaNCC"].ToString(),
                            TenNCC = reader["TenNCC"].ToString(),
                            Email = reader["Email"].ToString(),
                            SoDienThoai = reader["SoDienThoai"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public void Add(nhacungcapdto ncc)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if MaNCC already exists
                var checkCmd = new SqlCommand("SELECT COUNT(*) FROM NHACUNGCAP WHERE MaNCC = @MaNCC", conn);
                checkCmd.Parameters.AddWithValue("@MaNCC", ncc.MaNCC);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    throw new Exception("MaNCC already exists in the database.");
                }

                // Insert new record
                var cmd = new SqlCommand("INSERT INTO NHACUNGCAP (MaNCC, TenNCC, Email, SoDienThoai) VALUES (@MaNCC, @TenNCC, @Email, @SoDienThoai)", conn);
                cmd.Parameters.AddWithValue("@MaNCC", ncc.MaNCC);
                cmd.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(ncc.Email) ? (object)DBNull.Value : ncc.Email);
                cmd.Parameters.AddWithValue("@SoDienThoai", string.IsNullOrEmpty(ncc.SoDienThoai) ? (object)DBNull.Value : ncc.SoDienThoai);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(nhacungcapdto ncc)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE NHACUNGCAP SET TenNCC = @TenNCC, Email = @Email, SoDienThoai = @SoDienThoai WHERE MaNCC = @MaNCC", conn);
                cmd.Parameters.AddWithValue("@MaNCC", ncc.MaNCC);
                cmd.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(ncc.Email) ? (object)DBNull.Value : ncc.Email);
                cmd.Parameters.AddWithValue("@SoDienThoai", string.IsNullOrEmpty(ncc.SoDienThoai) ? (object)DBNull.Value : ncc.SoDienThoai);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string maNCC)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM NHACUNGCAP WHERE MaNCC = @MaNCC", conn);
                cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                cmd.ExecuteNonQuery();
            }
        }

        public List<nhacungcapdto> Search(string keyword)
        {
            var list = new List<nhacungcapdto>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM NHACUNGCAP WHERE MaNCC LIKE @Keyword OR TenNCC LIKE @Keyword OR Email LIKE @Keyword OR SoDienThoai LIKE @Keyword", conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new nhacungcapdto
                        {
                            MaNCC = reader["MaNCC"].ToString(),
                            TenNCC = reader["TenNCC"].ToString(),
                            Email = reader["Email"].ToString(),
                            SoDienThoai = reader["SoDienThoai"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}
