using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021044.DomainModels;

namespace _19T1021044.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các phép xử lý dữ liệu của người giao hàng
    /// </summary>
    public class ShipperDAL : _BaseDAL, ICommonDAL<Shipper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ShipperDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Bổ sung thêm 1 người giao hàng 
        /// </summary>
        /// <param name="data">thông tin người giao hàng cần thêm</param>
        /// <returns>ID của nhà cung cấp vừa bổ sung</returns> 
        public int Add(Shipper data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into Shippers(ShipperName,Phone)
                                            values(@ShipperName,@Phone)
                                             Select @@Identity;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);

                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Đếm số người giao hàng tìm được
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm</param>
        /// <returns></returns>
        public int Count(string searchValue = "")
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            int count = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select count(*)
                                    from    Shippers
                                    where  (@searchValue = N'')
                                        or (
                                                (ShipperName like @searchValue)
                                                or
                                                (Phone like @searchValue)
                                            )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return count;
        }
        /// <summary>
        /// Xoá 1 người giao hàng
        /// </summary>
        /// <param name="id">Mã 1 người giao hàng cần xoá</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Delete 
                                    from Shippers
                                     where ShipperID = @ShipperID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@ShipperID", id);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }


            return result;
        }
        /// <summary>
        /// Lấy thông tin của 1 người giao hàng dựa vào mã
        /// </summary>
        /// <param name="id">mã của người giao hàng</param>
        /// <returns></returns>
        public Shipper Get(int id)
        {
            Shipper data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from Shippers
                                     where ShipperID = @ShipperID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@ShipperID", id);


                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (result.Read())
                {
                    data = new Shipper()
                    {
                        ShipperID = Convert.ToInt32(result["ShipperID"]),
                        ShipperName = Convert.ToString(result["ShipperName"]),
                        Phone = Convert.ToString(result["Phone"])
                    };
                }
                result.Close();
                cn.Close();
            }
            return data;
        }
        /// <summary>
        /// kiểm tra xem nhà cung cấp hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="id">mã của người giao hàng</param>
        /// <returns></returns>
        public bool InUsed(int id)
        {
            bool result = false;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Select case when Exists(select * from Orders where ShipperID = @shipperID) then 1 else 0 end";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@shipperID", id);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }


            return result;
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách các người giao hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagesize">Số dòng hiển thị trên mỗi trang(0 tức là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        public IList<Shipper> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            IList<Shipper> data = new List<Shipper>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            //Tạo CSDL
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from
                                        (
                                            select    *,
                                                    row_number() over(order by ShipperName) as RowNumber
                                            from    Shippers
                                            where    (@searchValue = N'')
                                                or (
                                                        (ShipperName like @searchValue)
                                                        or
                                                        (Phone like @searchValue)
                                                    )
                                        ) as t
                                   where (@pageSize = 0) or (t.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                                    order by t.RowNumber;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Shipper()
                    {
                        ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                        ShipperName = Convert.ToString(dbReader["ShipperName"]),
                        Phone = Convert.ToString(dbReader["Phone"])
                    });
                }
                dbReader.Close();
                cn.Close();
            }


            return data;
        }
        /// <summary>
        /// cập nhật thông tin người giao hàng
        /// </summary>
        /// <param name="data">dữ liệu của người giao hàng cần cập nhật</param>
        /// <returns></returns>
        public bool Update(Shipper data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Update Shippers
                                    Set ShipperName = @ShipperName,
                                    Phone = @Phone
                                    WHERE ShipperID = @ShipperID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
