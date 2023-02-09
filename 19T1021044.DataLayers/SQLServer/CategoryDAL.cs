using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
        /// <summary>
        /// Đếm số loại hàng tìm được
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm</param>
        /// <returns></returns>
        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Xoá 1 loại hàng
        /// </summary>
        /// <param name="id">Mã 1 loại hàng cần xoá</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Lấy thông tin của 1 loại hàng dựa vào mã
        /// </summary>
        /// <param name="id">mã của loại hàng</param>
        /// <returns></returns>
        public Category Get(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// kiểm tra xem nhà cung cấp hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="id">mã của loại hàng</param>
        /// <returns></returns>
        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách các loại hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagesize">Số dòng hiển thị trên mỗi trang(0 tức là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        public IList<Category> List(int page = 1, int pagesize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// cập nhật thông tin loại hàng
        /// </summary>
        /// <param name="data">dữ liệu của loại hàng cần cập nhật</param>
        /// <returns></returns>
        public bool Update(Category data)
        {
            throw new NotImplementedException();
        }
    }
}
