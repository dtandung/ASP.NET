using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021044.Web.Controllers
{
    public class SupplierController : Controller
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
        /// Giao Diện để bổ sung nhà cung cấp mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
		{
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View("Edit");
		}
        /// <summary>
        /// Giao Diện để chỉnh sửa thông tin nhà cung cấp 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
		{
            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View();
		}

        public ActionResult Delete()
		{
            return View();
		}
    }
}