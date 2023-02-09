using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021044.Web.Controllers
{
    public class ShipperController : Controller
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
        /// Giao diện bổ sung mới người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
		{
            ViewBag.Title = "Bổ Sung Người Giao Hàng Mới";
            return View("Edit");
		}
        /// <summary>
        /// Giao diện cập nhật thông tin người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
		{
            ViewBag.Title = "Cập Nhật Thông Tin Người Giao Hàng";
            return View();
		}
        /// <summary>
        /// Giao diện xoá người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
		{
            return View();
		}
    }
}