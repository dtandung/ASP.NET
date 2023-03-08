using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021044.Web { 
    public class ApiResult
    {
        /// <summary>
        /// 1: Thành công, 0: lỗi
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Thông báo lỗi (nếu có)
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Dữ liệu trả về (nếu có)
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiResult CreateFailResult(string msg)
        {
            return new ApiResult()
            {
                Code = 0,
                Message = msg,
                Data = null
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResult CreateSuccessResult(object data = null)
        {
            return new ApiResult()
            {
                Code = 1,
                Message = "",
                Data = data
            };
        }
    }
}
