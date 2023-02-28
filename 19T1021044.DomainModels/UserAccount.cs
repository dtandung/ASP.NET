using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021044.DomainModels
{
    /// <summary>
    /// tài khoản người dùng
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// ID người dùng
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// tên hiển thị người dùng
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Tên Đầy đủ của người dùng
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// email người dùng
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// mật khẩu
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Nhóm tên
        /// </summary>
        public string RoleNames { get; set; }
        /// <summary>
        /// ảnh
        /// </summary>
        public string Photo { get; set; }
    }
}
