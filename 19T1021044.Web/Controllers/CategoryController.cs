using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021044.Web.Controllers
{
    public class CategoryController : Controller
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
        /// Giao diện để bổ sung một loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create() 
        {
            ViewBag.Title = "Bổ Sung Loại Hàng";
            return View("Edit");
        }
        /// <summary>
        /// Giao diện để cập nhật thông tin loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
		{
            ViewBag.Title = "Cập Nhật Thông Tin Loại Hàng";
            return View();
		}
        /// <summary>
        /// Xoá 1 Loại Hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
		{
            return View();
		}
    }
}