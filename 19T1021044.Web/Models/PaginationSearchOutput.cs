using _19T1021044.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021044.Web.Models
{
    /// <summary>
    /// lớp cơ sở cho các lớp dùng để lưu trữ kết quả tìm kiếm dưới dạng phân trạng
    /// </summary>
    public abstract class PaginationSearchOutput
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
        /// <summary>
        /// số dòng tìm được
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// tổng số trang
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    return 1;

                int p = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    p += 1;
                return p;
            }
        }
    }
    public class PaginationProductSearchOutput : PaginationSearchOutput
    {
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        
    }
    public class PaginationOrderSearchOutput : PaginationSearchOutput
    {
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public int Status { get; set; }
    }
}