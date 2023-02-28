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
    [Authorize]//đặt trước controller, action đều được
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 5;//1 giá trị dùng từ 2 lần trở lên nên dùng hằng
        private const string SUPPLIER_SEARCH = "SearchSupplierCondition";
        /// <summary>
        /// trang giao diện
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as PaginationSearchInput;

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
        /// kết quả tìm kiếm
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSupplier(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new SupplierSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[SUPPLIER_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// Giao Diện để bổ sung nhà cung cấp mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Supplier()
            {
                SupplierID = 0
            };
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View("Edit", data);
        }
        /// <summary>
        /// Giao Diện để chỉnh sửa thông tin nhà cung cấp 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0) //nếu kh cho phép null thì đầu vào sẽ lỗi ngay lời gọi hàm kh vào được code bên trong
        {
            //int supplierId = Convert.ToInt32(id);
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetSupplier(id);
            if (data == null)
                return RedirectToAction("Index");
            //return Json(data, JsonRequestBehavior.AllowGet);
            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]//tránh những sự tấn công từ bên ngoài
        public ActionResult Save(Supplier data)
        {
            try
            {
                //kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.SupplierName))
                    ModelState.AddModelError("SupplierName", "Tên Không Được Để Trống");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên Giao Dịch Không Được Để Trống");
                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Vui Lòng Chọn Quốc Gia");
                if (string.IsNullOrWhiteSpace(data.Address))
                    data.Address = "";
                if (string.IsNullOrWhiteSpace(data.City))
                    data.City = "";
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    data.PostalCode = "";

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                    return View("Edit", data);
                }

                if (data.SupplierID == 0)
                {
                    CommonDataService.AddSupplier(data);
                }
                else
                {
                    CommonDataService.UpdateSupplier(data);
                }
                return RedirectToAction("Index");
            }
            catch 
            {
                return Content("Có lỗi xảy ra. Vui lòng thử lại!");
            }

        }
        /// <summary>
        /// xoá nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            // int supplierID = Convert.ToInt32(id);
            if (id == 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetSupplier(id);
                if (data == null)
                    return RedirectToAction("Index");
                return View(data);
            }
            else
            {
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
        }
    }
}