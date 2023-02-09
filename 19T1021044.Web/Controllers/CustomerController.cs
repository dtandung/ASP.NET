using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021044.Web.Controllers
{
    public class CustomerController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Giao diện bổ sung khách hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
		{
            ViewBag.Title = "Bổ Sung Khách Hàng";
            return View("Edit");
		}
        /// <summary>
        /// Giao diện cập nhật thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
		{
            ViewBag.Title = "Cập Nhật Thông Tin Khách Hàng";
            return View();
		}
        /// <summary>
        /// Giao diện xoá 1 khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
		{
            return View();
		}
    }
}