using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021044.DomainModels;
using _19T1021044.BusinessLayers;
using _19T1021044.Web.Models;

namespace _19T1021044.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private const int PAGE_SIZE = 10;//1 giá trị dùng từ 2 lần trở lên nên dùng hằng
        private const string CUSTOMER_SEARCH = "SearchCustomerCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[CUSTOMER_SEARCH] as PaginationSearchInput;

            if (condition == null)
            {
                condition = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }


            return View(condition);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomer(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new CustomerSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[CUSTOMER_SEARCH] = condition;
            return View(result);
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
        public ActionResult Edit(int id = 0)
		{
            //int customerId = Convert.ToInt32(id);
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetCustomer(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập Nhật Thông Tin Khách Hàng";
            return View(data);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer data)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", "Tên Không Được Để Trống");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên Giao Dịch Không Được Để Trống");
                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Vui Lòng Chọn Quốc Gia");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Vui Lòng Điền Địa Chỉ");
                if (string.IsNullOrWhiteSpace(data.City))
                    ModelState.AddModelError("City", "Vui Lòng Điền Thành Phố");
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    ModelState.AddModelError("PostalCode", "Vui Lòng Nhập Mã Bưu Chính");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CustomerID == 0 ? "Bổ sung nhà khách hàng" : "Cập nhật khách hàng";
                    return View("Edit", data);
                }

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
            catch
            {
                return Content("Có lỗi xảy ra. Vui lòng thử lại!");
            }
            
        }
        /// <summary>
        /// Giao diện xoá 1 khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
		{
            //int customerID = Convert.ToInt32(id);
            if (id == 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCustomer(id);
                if (data == null)
                    return RedirectToAction("Index");
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
        }
    }
}