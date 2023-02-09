using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021044.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép dữ liệu chung cho các dữ liệu đơn giản trên các bảng
    /// </summary>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagesize">Số dòng hiển thị trên mỗi trang(0 tức là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pagesize = 0, string searchValue = "");
        /// <summary>
        /// Đếm số dòng dữ liệu tìm được
        /// </summary>
        /// <param name="searchValue">giá tr cần tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên) </param>
        /// <returns></returns>
        int Count(string searchValue = "");
        /// <summary>
        /// Bổ sung 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID vừa bổ sung</returns>
        int Add(T data);
        /// <summary>
        /// Cập nhật thông tin 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);
        /// <summary>
        /// Xoá 
        /// </summary>
        /// <param name="id">Mã cần xoá</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// Lấy thông tin dựa vào mã
        /// </summary>
        /// <param name="id">Mã</param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Kiểm tra xem hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="id">Mã </param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}
