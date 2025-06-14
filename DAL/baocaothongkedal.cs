using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class BaoCaoThongKeDAL
    {
        // Khai báo chuỗi kết nối trực tiếp trong lớp DAL
        private string connectionString = @"Data Source=LAPTOP-PMLRDEHN\MAY1;Initial Catalog=quanlycuahanglinhkiendienthoai;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"; // Cập nhật chuỗi kết nối của bạn ở đây

        // Lấy Top 3 sản phẩm bán chạy nhất trong khoảng thời gian
        public List<SanPhamThongKeDTO> GetTop3SanPhamBanChay(DateTime tuNgay, DateTime denNgay)
        {
            string query = "SELECT TOP 3 sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TongSoLuongBan " +
                           "FROM CHITIETHOADON ct " +
                           "JOIN HOADON hd ON ct.MaHD = hd.MaHD " +
                           "JOIN SANPHAM sp ON ct.MaSP = sp.MaSP " +
                           "WHERE hd.NgayLap BETWEEN @TuNgay AND @DenNgay " +
                           "GROUP BY sp.MaSP, sp.TenSP " +
                           "ORDER BY TongSoLuongBan DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay)
            };

            return ExecuteQuery<SanPhamThongKeDTO>(query, parameters);
        }

        // Lấy Top 3 sản phẩm bán ít nhất trong khoảng thời gian
        public List<SanPhamThongKeDTO> GetTop3SanPhamBanIt(DateTime tuNgay, DateTime denNgay)
        {
            string query = "SELECT TOP 3 sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TongSoLuongBan " +
                           "FROM CHITIETHOADON ct " +
                           "JOIN HOADON hd ON ct.MaHD = hd.MaHD " +
                           "JOIN SANPHAM sp ON ct.MaSP = sp.MaSP " +
                           "WHERE hd.NgayLap BETWEEN @TuNgay AND @DenNgay " +
                           "GROUP BY sp.MaSP, sp.TenSP " +
                           "ORDER BY TongSoLuongBan ASC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay)
            };

            return ExecuteQuery<SanPhamThongKeDTO>(query, parameters);
        }

        // Lấy Top 3 nhân viên bán được nhiều nhất trong khoảng thời gian
        public List<NhanVienThongKeDTO> GetTop3NhanVienBanChay(DateTime tuNgay, DateTime denNgay)
        {
            List<NhanVienThongKeDTO> list = new List<NhanVienThongKeDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
    SELECT nv.MaNV, nv.HoTen AS TenNV, SUM(cthd.SoLuong) AS TongSoLuongBan
    FROM HOADON hd
    JOIN CHITIETHOADON cthd ON hd.MaHD = cthd.MaHD
    JOIN NHANVIEN nv ON nv.MaNV = hd.MaNV
    WHERE hd.Ngaylap BETWEEN @TuNgay AND @DenNgay
    GROUP BY nv.MaNV, nv.HoTen
    ORDER BY TongSoLuongBan DESC
";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new NhanVienThongKeDTO
                    {
                        MaNV = reader["MaNV"].ToString(),
                        TenNV = reader["TenNV"].ToString(),
                        TongSoLuongBan = Convert.ToInt32(reader["TongSoLuongBan"])
                    });
                }
                conn.Close();
            }

            return list;
        }


        // Lấy danh sách hóa đơn trong khoảng thời gian
        public List<HoaDonThongKeDTO> GetHoaDon(DateTime tuNgay, DateTime denNgay)
        {
            List<HoaDonThongKeDTO> list = new List<HoaDonThongKeDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT MaHD, MaKH, MaNV, NgayLap
            FROM HOADON
            WHERE NgayLap BETWEEN @TuNgay AND @DenNgay";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new HoaDonThongKeDTO
                    {
                        MaHD = reader["MaHD"].ToString(),
                        MaKH = reader["MaKH"].ToString(),
                        MaNV = reader["MaNV"].ToString(),
                        NgayBan = Convert.ToDateTime(reader["NgayLap"])
                    });
                }
                conn.Close();
            }

            return list;
        }


        // Lấy tổng doanh thu trong khoảng thời gian
        public DoanhThuDTO GetTongDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            DoanhThuDTO dto = new DoanhThuDTO();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT SUM(CTHD.SoLuong * CTHD.DonGia) AS TongTien
            FROM HOADON HD
            JOIN CHITIETHOADON CTHD ON HD.MaHD = CTHD.MaHD
            WHERE HD.NgayLap BETWEEN @TuNgay AND @DenNgay";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                object result = cmd.ExecuteScalar();
                dto.TongTien = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

                conn.Close();
            }

            return dto;
        }


        // Phương thức thực thi truy vấn và chuyển dữ liệu thành danh sách DTO
        private List<T> ExecuteQuery<T>(string query, SqlParameter[] parameters)
        {
            List<T> results = new List<T>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T item = Activator.CreateInstance<T>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var prop = item.GetType().GetProperty(reader.GetName(i));
                                if (prop != null && reader[i] != DBNull.Value)
                                {
                                    prop.SetValue(item, reader[i]);
                                }
                            }
                            results.Add(item);
                        }
                    }
                }
            }

            return results;
        }
    }

}
