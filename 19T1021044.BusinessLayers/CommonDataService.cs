using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021044.DataLayers;
using _19T1021044.DomainModels;
using System.Configuration;

namespace _19T1021044.BusinessLayers
{
    /// <summary>
    /// Các chức năng nghiệp vụ liên quan đến: nhà cung cấp, khách hàng, người giao hàng, nhân viên, loại hàng
    /// </summary>
    public static class CommonDataService
    {
        private static ICountryDAL countryDB;
        private static ICommonDAL<Supplier> supplierDB;
        private static ICommonDAL<Shipper> shipperDB;
        private static ICommonDAL<Employee> employeeDB;
        private static ICommonDAL<Customer> customerDB;
        private static ICommonDAL<Category> categoryDB;
        /// <summary>
        /// Constructor(của lớp static không được có tham số)
        /// </summary>
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            countryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            employeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
        }
        #region Các nghiệp vụ liên quan đến quốc gia
        /// <summary>
        /// Lấy danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }
        #endregion
        #region Các nghiệp vụ liên quan đến nhà cung cấp

        /// <summary>
        /// tìm kiếm, lấy dữ liệu, lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">số dòng trên mỗi trang(chuỗi rỗng nếu không tìm kiếm)</param>
        /// <param name="searchValue">giá trị tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <param name="rowCount">Output: tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSupplier(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tiềm kiếm và lấy danh sách nhà cung cấp (không phân trang)
        /// </summary>
        /// <param name="searchValue">giá trị tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(string searchValue)
        {
            return supplierDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Bổ sung nhà cung cấp
        /// </summary>
        /// <param name="data">dữ liệu của nhà cung cấp vừa bổ sung</param>
        /// <returns>mã của nhà cung cấp được bổ sung</returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin nhà cung cấp
        /// </summary>
        /// <param name="data">dữ liệu được cập nhật của nhà cung cấp</param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }
        /// <summary>
        /// Xoá nhà cung cấp
        /// </summary>
        /// <param name="supplierID">mã nhà cung cấp cần xoá</param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            if (supplierDB.InUsed(supplierID))
            {
                return false;
            }
            return supplierDB.Delete(supplierID);
        }
        /// <summary>
        /// Lấy thông tin của 1 nhà cung cấp
        /// </summary>
        /// <param name="customerID">mã nhà cung cấp được lấy</param>
        /// <returns>dữ liệu thông tin của 1 nhà cung cấp</returns>
        public static Supplier GetSupplier(int customerID)
        {
            return supplierDB.Get(customerID);
        }
        /// <summary>
        /// kiểm tra xem 1 nhà cung cấp hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="supplierID">mã nhà cung cấp được kiểm tra</param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
        #endregion
        #region Các nghiệp vụ liên quan đến khách hàng
        /// <summary>
        /// tìm kiếm, lấy dữ liệu, lấy danh sách các khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">số dòng trên mỗi trang(chuỗi rỗng nếu không tìm kiếm)</param>
        /// <param name="searchValue">giá trị tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <param name="rowCount">Output: tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomer(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tiềm kiếm và lấy danh sách khách hàng (không phân trang)
        /// </summary>
        /// <param name="searchValue">giá trị tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(string searchValue)
        {
            return customerDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Bổ sung khách hàng
        /// </summary>
        /// <param name="data">dữ liệu của khách hàng vừa bổ sung</param>
        /// <returns>mã của khách hàng vừa được bổ sung</returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="data">dữ liệu được cập nhật của khách hàng</param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// Xoá khách hàng
        /// </summary>
        /// <param name="customerID">mã khách hàng cần xoá</param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            if (customerDB.InUsed(customerID))
            {
                return false;
            }
            return customerDB.Delete(customerID);
        }
        /// <summary>
        /// Lấy thông tin của 1 khách hàng
        /// </summary>
        /// <param name="customerID">mã khách hàng được lấy</param>
        /// <returns>dữ liệu thông tin của 1 khách hàng</returns>
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }
        /// <summary>
        /// kiểm tra xem 1 khách hàng hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="customerID">mã khách hàng được kiểm tra</param>
        /// <returns></returns>
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }
        #endregion
        #region các chức năng liên quan đến nhân viên

        /// <summary>
        /// Tìm kiếm nhân viên(không phân trang)
        /// </summary>
        /// <returns></returns>
        public static List<Employee> ListOfEmployee(string searchValue)
        {
            return employeeDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="pageSize">số nhân viên trong 1 page</param>
        /// <param name="searchValue">Từ khóa tìm kiếm</param>
        /// <param name="rowCount">Tổng số nhân viên sau khi xử lý tìm kiếm</param>
        /// <returns>Danh sách nhân viên sau khi tìm kiếm</returns>
        public static List<Employee> ListOfEmployees(int page
                                                    , int pageSize
                                                    , string searchValue
                                                    , out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 khách hàng
        /// </summary>
        /// <param name="employeeID">mã khách hàng được lấy</param>
        /// <returns>dữ liệu thông tin của 1 khách hàng</returns>
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }

        /// <summary>
        /// xoá nhân viên
        /// </summary>
        /// <param name="employeeID">mã nhân viên được xoá</param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            if (employeeDB.InUsed(employeeID))
            {
                return false;
            }
            return employeeDB.Delete(employeeID);
        }
        /// <summary>
        /// bổ sung nhân viên
        /// </summary>
        /// <param name="data">dữ liệu nhân viên được bổ sung</param>
        /// <returns>mã của nhân viên được bổ sung</returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        /// <summary>
        /// cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="data">dữ liệu được cập nhật của nhân viên</param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        /// kiểm tra xem 1 nhân viên hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="employeeID">mã nhân viên được kiểm tra</param>
        /// <returns></returns>
        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }
        #endregion
        #region các chức năng liên quan đến người giao hàng
        //public static List<Shipper> ListOfShippers() => shipperDB.List().ToList();
        /// <summary>
        /// Tìm kiếm người giao hàng(không phân trang)
        /// </summary>
        /// <returns></returns>
        public static List<Shipper> ListOfShipper(string searchValue)
        {
            return shipperDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm người giao hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="pageSize">số người giao hàng trong 1 page</param>
        /// <param name="searchValue">Từ khóa tìm kiếm</param>
        /// <param name="rowCount">Tổng số ngươi giao hàng sau khi xử lý tìm kiếm</param>
        /// <returns>Danh sách người giao hàng sau khi tìm kiếm</returns>
        /// 
        public static List<Shipper> ListOfShippers(int page
                                                    , int pageSize
                                                    , string searchValue
                                                    , out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// lấy thong tin của người giao hàng 
        /// </summary>
        /// <param name="shipperID">mã của người giao hàng</param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }

        /// <summary>
        /// xoá 1 người giao hàng
        /// </summary>
        /// <param name="shipperID">mã người giao hàng được xoá</param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperID)
        {
            if (shipperDB.InUsed(shipperID))
            {
                return false;
            }
            return shipperDB.Delete(shipperID);
        }
        /// <summary>
        /// bổ sung người giao hàng
        /// </summary>
        /// <param name="data">dữ liệu bổ sung của người giao hàng</param>
        /// <returns>mã của người giao hàng vừa bổ sung</returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }

        /// <summary>
        /// cập nhật thông tin người giao hàng
        /// </summary>
        /// <param name="data">dữ liệu được cập nhật của người giao hàng</param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        /// kiểm tra xem 1 người giao hàng hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="shipperID">mã người giao hàng được kiểm tra</param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }
        #endregion
        #region các chức năng liên quan đến loại hàng
        /// <summary>
        /// tìm kiếm loại hàng(không phân trang)
        /// </summary>
        /// <param name="searchValue">từ khoá tìm kiếm</param>
        /// <returns></returns>
        public static List<Category> ListOfCategory(string searchValue)
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm loại hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="pageSize">số loại hàng trong 1 page</param>
        /// <param name="searchValue">Từ khóa tìm kiếm</param>
        /// <param name="rowCount">Tổng số loại hàng sau khi xử lý tìm kiếm</param>
        /// <returns>Danh sách loại hàng sau khi tìm kiếm</returns>
        /// 
        //public static List<Category> ListOfCategorys() => categoryDB.List().ToList();

        public static List<Category> ListOfCategories(int page
                                                    , int pageSize
                                                    , string searchValue
                                                    , out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// lấy thông tin loại hàng
        /// </summary>
        /// <param name="categoryID">mã loại hàng được lấy thông tin</param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }
        /// <summary>
        /// xoá loại hàng
        /// </summary>
        /// <param name="categoryID">mã loại hàng được xoá</param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            if (categoryDB.InUsed(categoryID))
            {
                return false;
            }
            return categoryDB.Delete(categoryID);
        }
        /// <summary>
        /// bổ sung loại hàng   
        /// </summary>
        /// <param name="data">dữ liệu của loại hàng được bổ sung</param>
        /// <returns>mã loại hàng vừa bổ sung</returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }

        /// <summary>
        /// cập nhật thông tin loại hàng
        /// </summary>
        /// <param name="data">dữ liệu được cập nhật của loại hàng</param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        /// <summary>
        /// kiểm tra xem 1 loại hàng hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="categoryID">mã của loại hàng được kiểm tra</param>
        /// <returns></returns>
        public static bool InUsedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }
        #endregion
    }
}
