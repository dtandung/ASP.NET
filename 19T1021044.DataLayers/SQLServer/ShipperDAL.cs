using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
        /// <summary>
        /// Đếm số người giao hàng tìm được
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm</param>
        /// <returns></returns>
        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Xoá 1 người giao hàng
        /// </summary>
        /// <param name="id">Mã 1 người giao hàng cần xoá</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Lấy thông tin của 1 người giao hàng dựa vào mã
        /// </summary>
        /// <param name="id">mã của người giao hàng</param>
        /// <returns></returns>
        public Shipper Get(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// kiểm tra xem nhà cung cấp hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="id">mã của người giao hàng</param>
        /// <returns></returns>
        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách các người giao hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagesize">Số dòng hiển thị trên mỗi trang(0 tức là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm(chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        public IList<Shipper> List(int page = 1, int pagesize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// cập nhật thông tin người giao hàng
        /// </summary>
        /// <param name="data">dữ liệu của người giao hàng cần cập nhật</param>
        /// <returns></returns>
        public bool Update(Shipper data)
        {
            throw new NotImplementedException();
        }
    }
}
