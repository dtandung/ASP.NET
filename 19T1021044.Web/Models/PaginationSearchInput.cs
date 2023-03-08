using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021044.Web.Models
{
    /// <summary>
    /// biểu diễn dữ liệu đầu vào để tìm kiếm, phân trang chung
    /// </summary>
    public class PaginationSearchInput
    {
        /// <summary>
        /// trang cần hiển thị
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// số dòng cần hiển thị trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// giá trị cần tìm
        /// </summary>
        public string SearchValue { get; set; }
    }

    public class PaginationProductSearchInput : PaginationSearchInput
    {
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
    }

    public class PaginationOrderSearchInput : PaginationSearchInput
    {
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public int Status { get; set; }
    }
}