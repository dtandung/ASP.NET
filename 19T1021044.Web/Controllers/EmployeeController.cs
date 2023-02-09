using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021044.Web.Controllers
{
    public class EmployeeController : Controller
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
        /// Giao diện bổ sung nhân viên mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
		{
            ViewBag.Title = "Bổ Sung Nhân Viên";
            return View("Edit");
		}
        /// <summary>
        /// Giao diện cập nhật thông tin nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
		{
            ViewBag.Title = "Cập Nhật Thông Tin Nhân Viên";
            return View();		
        }
        /// <summary>
        /// Giao diện xoá nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
		{
            return View();
		}
    }
}