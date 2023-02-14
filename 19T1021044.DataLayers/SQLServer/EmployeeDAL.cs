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
    /// Cài đặt các phép xử lý dữ liệu của nhân viên
    /// </summary>
    public class EmployeeDAL : _BaseDAL, ICommonDAL<Employee>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Bổ sung thêm 1 nhân viên 
        /// </summary>
        /// <param name="data">thông tin nhân viên cần thêm</param>
        /// <returns>ID của nhà cung cấp vừa bổ sung</returns>
        public int Add(Employee data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {

                String Password = "123456789";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into Employees(LastName,FirstName,BirthDate,Notes,Photo,Email,Password)
                                            values(@LastName,@FirstName,@BirthDate,@Notes,@Photo,@Email,@Password)
                                             Select @@Identity;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Đếm số nhân viên tìm được
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
                                    from    Employees
                                    where  (@searchValue = N'')
                                        or (
                                                (FirstName like @searchValue)
                                                or
                                                (LastName like @searchValue)
                                                or
                                                (Email like @searchValue)
                                                or
                                                (FirstName + ' ' + LastName LIKE @SearchValue)
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
        /// Xoá 1 nhân viên
        /// </summary>
        /// <param name="id">Mã 1 nhân viên cần xoá</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Delete 
                                    from Employees
                                     where EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@EmployeeID", id);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }


            return result;
        }
        /// <summary>
        /// Lấy thông tin của 1 nhân viên dựa vào mã
        /// </summary>
        /// <param name="id">mã của nhân viên</param>
        /// <returns></returns>
        public Employee Get(int id)
        {
            Employee data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from Employees
                                     where EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@EmployeeID", id);


                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (result.Read())
                {
                    data = new Employee()
                    {
                        EmployeeID = Convert.ToInt32(result["EmployeeID"]),
                        LastName = Convert.ToString(result["LastName"]),
                        FirstName = Convert.ToString(result["FirstName"]),
                        BirthDate = Convert.ToDateTime(result["BirthDate"]),
                        Notes = Convert.ToString(result["Notes"]),
                        Photo = Convert.ToString(result["Photo"]),
                        Email = Convert.ToString(result["Email"]),
                        Password = Convert.ToString(result["Password"])

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
        /// <param name="id">mã của nhân viên</param>
        /// <returns></returns>
        public bool InUsed(int id)
        {
            bool result = false;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Select case when Exists(select * from Orders where EmployeeID = @employeeID) then 1 else 0 end";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@employeeID", id);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }


            return result;
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách các nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagesize">Số dòng hiển thị trên mỗi trang(0 tức là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        public IList<Employee> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            IList<Employee> data = new List<Employee>();

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
                                                    row_number() over(order by FirstName) as RowNumber
                                            from    Employees
                                            where    (@searchValue = N'')
                                                or (
                                                        (FirstName like @searchValue)
                                                        or
                                                        (LastName like @searchValue)
                                                        or
                                                        (Email like @searchValue)
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
                    data.Add(new Employee()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        FirstName = Convert.ToString(dbReader["FirstName"]),
                        LastName = Convert.ToString(dbReader["LastName"]),
                        BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                        Notes = Convert.ToString(dbReader["Notes"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Email = Convert.ToString(dbReader["Email"]),
                        Password = Convert.ToString(dbReader["Password"])
                    });
                }
                dbReader.Close();
                cn.Close();
            }


            return data;
        }
        /// <summary>
        /// cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="data">dữ liệu của nhân viên cần cập nhật</param>
        /// <returns></returns>
        public bool Update(Employee data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Update Employees
                                    Set LastName = @LastName,
                                    FirstName = @FirstName,
                                    BirthDate = @BirthDate,
                                    Notes = @Notes,
                                    Photo = @Photo,
                                    Email = @Email
                                    WHERE EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                //Truyền tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
