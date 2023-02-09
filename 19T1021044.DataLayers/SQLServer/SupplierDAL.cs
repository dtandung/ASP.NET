using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021044.DomainModels;

namespace _19T1021044.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các phép xử lý dữ liệu của nhà cung cấp
    /// </summary>
    public class SupplierDAL : _BaseDAL, ICommonDAL<Supplier>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SupplierDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Bổ sung thêm 1 nhà cung cấp 
        /// </summary>
        /// <param name="data">thông tin nhà cung cấp cần thêm</param>
        /// <returns>ID của nhà cung cấp vừa bổ sung</returns>
        public int Add(Supplier data)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Đếm số nhà cung cấp tìm được
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm</param>
        /// <returns></returns>
        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Xoá 1 nhà cung cấp
        /// </summary>
        /// <param name="id">Mã 1 nhà cung cấp cần xoá</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Lấy thông tin của 1 nhà cung cấp dựa vào mã
        /// </summary>
        /// <param name="id">mã của nhà cung cấp</param>
        /// <returns></returns>
        public Supplier Get(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// kiểm tra xem nhà cung cấp hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="id">mã của nhà cung cấp</param>
        /// <returns></returns>
        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagesize">Số dòng hiển thị trên mỗi trang(0 tức là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        public IList<Supplier> List(int page = 1, int pagesize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// cập nhật thông tin nhà cung cấp
        /// </summary>
        /// <param name="data">dữ liệu của nhà cung cấp cần cập nhật</param>
        /// <returns></returns>
        public bool Update(Supplier data)
        {
            throw new NotImplementedException();
        }
    }
}
