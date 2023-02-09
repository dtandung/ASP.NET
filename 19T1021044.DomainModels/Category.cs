using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021044.DomainModels
{
    /// <summary>
    /// 
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Mã loại
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// Tên loại
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Chi tiết
        /// </summary>
        public string Description { get; set; }
    }
}
