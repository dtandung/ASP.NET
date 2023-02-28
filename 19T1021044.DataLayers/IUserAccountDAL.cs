using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021044.DomainModels;

namespace _19T1021044.DataLayers
{
    /// <summary>
    /// định nghĩa các phép xử lý liên quan đến tài khoản người dùng
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// kiểm tra tên đăng nhập và mật khẩu có hợp lệ hay không?
        /// nếu hợp lệ thì trả về thông tin của tài khoản, ngược lại trả về null
        /// </summary>
        /// <param name="userName">tên tài khoản</param>
        /// <param name="passWord">mật khẩu</param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string passWord);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
