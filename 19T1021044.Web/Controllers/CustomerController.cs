using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021044.DomainModels;
using _19T1021044.BusinessLayers;

namespace _19T1021044.Web.Controllers
{
    public class CustomerController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, int pageSize = 10, string searchValue = "")
        {
            int rowCount = 0;
            var model = CommonDataService.ListOfCustomer(page, pageSize, searchValue, out rowCount);

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
        /// Giao diện bổ sung khách hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
		{
            var data = new Customer()
            {
                CustomerID = 0
            };
            ViewBag.Title = "Bổ Sung Khách Hàng";
            return View("Edit", data);
		}
        /// <summary>
        /// Giao diện cập nhật thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string id)
		{
            int customerId = Convert.ToInt32(id);

            var data = CommonDataService.GetCustomer(customerId);
            ViewBag.Title = "Cập Nhật Thông Tin Khách Hàng";
            return View(data);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Customer data)
        {
            if (data.CustomerID == 0)
            {
                CommonDataService.AddCustomer(data);
            }
            else
            {
                CommonDataService.UpdateCustomer(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Giao diện xoá 1 khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
		{
            int customerID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCustomer(customerID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCustomer(customerID);
                return RedirectToAction("Index");
            }
        }
    }
}