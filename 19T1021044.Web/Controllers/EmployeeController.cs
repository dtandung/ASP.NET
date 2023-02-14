using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021044.DomainModels;
using _19T1021044.BusinessLayers;

namespace _19T1021044.Web.Controllers
{
    public class EmployeeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, int pageSize = 20, string searchValue = "")
        {
            int rowCount = 0;
            var model = CommonDataService.ListOfEmployees(page, pageSize, searchValue, out rowCount);

            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
                pageCount += 1;
            ViewBag.Page = page;
            ViewBag.PageCount = pageCount;
            ViewBag.RowCount = rowCount;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchValue = searchValue;

            return View(model);
        }
        /// <summary>
        /// Giao diện bổ sung nhân viên mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Employee()
            {
                EmployeeID = 0
            };
            ViewBag.Title = "Bổ sung nhân viên";
            return View("Edit", data);
        }
        /// <summary>
        /// Giao diện cập nhật thông tin nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            int employeeId = Convert.ToInt32(id);

            var data = CommonDataService.GetEmployee(employeeId);
            ViewBag.Title = "Cập Nhật Thông Tin Nhân Viên";
            return View(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Employee data)
        {

            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
            }

            return RedirectToAction("Index");
        }
        /// <summary>
        /// Giao diện xoá nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int employeeId = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetEmployee(employeeId);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteEmployee(employeeId);
                return RedirectToAction("Index");
            }
        }
    }
}