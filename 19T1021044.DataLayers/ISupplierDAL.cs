using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021044.DomainModels;

namespace _19T1021044.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu trên nhà cung cấp
    /// (sử dụng cách này dẫn đến viết lặp đi lặp lại các kiểu code giống nhau cho các đối tượng
    /// dữ liệu tương tự....)
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagesize">Số dòng hiển thị trên mỗi trang(0 tức là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        IList<Supplier> List(int page = 1, int pagesize = 0, string searchValue = "");
        /// <summary>
        /// Đếm số nhà cung cấp tìm được
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên) </param>
        /// <returns></returns>
        int Count(string searchValue = "");
        /// <summary>
        /// Bổ sung thêm 1 nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của nhà cung cấp vừa bổ sung</returns>
        int Add(Supplier data);
        /// <summary>
        /// Cập nhật thông tin của nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Supplier data);
        /// <summary>
        /// Xoá 1 nhà cung cấp dựa vào mã
        /// </summary>
        /// <param name="supplierID">Mã của 1 nhà cung cấp cần xoá</param>
        /// <returns></returns>
        bool Delete(int supplierID);
        /// <summary>
        /// Lấy thông tin của 1 nhà cung cấp dựa vào mã
        /// </summary>
        /// <param name="supplierID">Mã của nhà cung cấp</param>
        /// <returns></returns>
        Supplier Get(int supplierID);
        /// <summary>
        /// Kiểm tra xem nhà cung cấp hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="supplierID">Mã của nhà cung cấp</param>
        /// <returns></returns>
        bool InUsed(int supplierID);
    }
}
