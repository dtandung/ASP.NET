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
    public class SupplierController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, int pageSize = 5, string searchValue = "")
        {
            int rowCount = 0;
            var model = CommonDataService.ListOfSupplier(page, pageSize, searchValue, out rowCount);

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
        public ActionResult Edit(string id)
        {
            int supplierId = Convert.ToInt32(id);

            var data = CommonDataService.GetSupplier(supplierId);

            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Supplier data)
        {
            if(data.SupplierID == 0)
            {
                CommonDataService.AddSupplier(data);
            }
            else
            {
                CommonDataService.UpdateSupplier(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// xoá nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int supplierID = Convert.ToInt32(id);
            if(Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetSupplier(supplierID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteSupplier(supplierID);
                return RedirectToAction("Index");
            }
        }
    }
}