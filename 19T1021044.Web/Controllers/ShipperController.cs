using _19T1021044.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021044.DomainModels;

namespace _19T1021044.Web.Controllers
{
    public class ShipperController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, int pageSize = 20, string searchValue = "")
        {
            int rowCount = 0;
            var model = CommonDataService.ListOfShippers(page, pageSize, searchValue, out rowCount);

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
        /// Giao diện bổ sung mới người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
		{
            var data = new Shipper()
            {
                ShipperID = 0
            };
            ViewBag.Title = "Bổ sung người giao hàng";
            return View("Edit", data);
        }
        /// <summary>
        /// Giao diện cập nhật thông tin người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string id)
		{
            int shipperId = Convert.ToInt32(id);

            var data = CommonDataService.GetShipper(shipperId);
            ViewBag.Title = "Cập Nhật Thông Tin Người Giao Hàng";
            return View(data);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Shipper data)
        {
            if (data.ShipperID == 0)
            {
                CommonDataService.AddShipper(data);
            }
            else
            {
                CommonDataService.UpdateShipper(data);
            }

            return RedirectToAction("Index");
        }
        /// <summary>
        /// Giao diện xoá người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
		{
            int shipperID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetShipper(shipperID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteShipper(shipperID);
                return RedirectToAction("Index");
            }
        }
    }
}