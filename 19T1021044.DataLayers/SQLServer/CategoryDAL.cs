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
    /// Cài đặt các phép xử lý dữ liệu trên loại hàng
    /// </summary>
    public class CategoryDAL : _BaseDAL, ICommonDAL<Category>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CategoryDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Bổ sung thêm 1 loại hàng 
        /// </summary>
        /// <param name="data">thông tin loại hàng cần thêm</param>
        /// <returns>ID của nhà cung cấp vừa bổ sung</returns>
        public int Add(Category data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into Categories(CategoryName,Description)
                                            values(@CategoryName,@Description)
                                             Select @@Identity;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Đếm số loại hàng tìm được
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
                                    from    Categories
                                    where  (@searchValue = N'')
                                        or (
                                                (CategoryName like @searchValue)
                                                or
                                                (Description like @searchValue)
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
        /// Xoá 1 loại hàng
        /// </summary>
        /// <param name="id">Mã 1 loại hàng cần xoá</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Delete 
                                    from Categories
                                     where CategoryID = @CategoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@CategoryID", id);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }


            return result;
        }
        /// <summary>
        /// Lấy thông tin của 1 loại hàng dựa vào mã
        /// </summary>
        /// <param name="id">mã của loại hàng</param>
        /// <returns></returns>
        public Category Get(int id)
        {
            Category data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from Categories
                                     where CategoryID = @CategoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@CategoryID", id);


                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (result.Read())
                {
                    data = new Category()
                    {
                        CategoryID = Convert.ToInt32(result["CategoryID"]),
                        CategoryName = Convert.ToString(result["CategoryName"]),
                        Description = Convert.ToString(result["Description"])
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
        /// <param name="id">mã của loại hàng</param>
        /// <returns></returns>
        public bool InUsed(int id)
        {
            bool result = false;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Select case when Exists(select * from Products where CategoryID = @categoryID) then 1 else 0 end";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@categoryID", id);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }


            return result;
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách các loại hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagesize">Số dòng hiển thị trên mỗi trang(0 tức là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        public IList<Category> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            IList<Category> data = new List<Category>();

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
                                                    row_number() over(order by CategoryName) as RowNumber
                                            from    Categories
                                            where    (@searchValue = N'')
                                                or (
                                                        (CategoryName like @searchValue)
                                                        or
                                                        (Description like @searchValue)
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
                    data.Add(new Category()
                    {
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        CategoryName = Convert.ToString(dbReader["CategoryName"]),
                        Description = Convert.ToString(dbReader["Description"])
                    });
                }
                dbReader.Close();
                cn.Close();
            }


            return data;
        }
        /// <summary>
        /// cập nhật thông tin loại hàng
        /// </summary>
        /// <param name="data">dữ liệu của loại hàng cần cập nhật</param>
        /// <returns></returns>
        public bool Update(Category data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Update Categories
                                    Set CategoryName = @CategoryName, Description = @Description
                                    WHERE CategoryID = @CategoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
