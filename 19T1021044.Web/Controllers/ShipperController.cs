using _19T1021044.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021044.DomainModels;
using _19T1021044.Web.Models;

namespace _19T1021044.Web.Controllers
{
    public class ShipperController : Controller
    {
        private const int PAGE_SIZE = 5;//1 giá trị dùng từ 2 lần trở lên nên dùng hằng
        private const string SHIPPER_SEARCH = "SearchShipperCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[SHIPPER_SEARCH] as PaginationSearchInput;

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
            var data = CommonDataService.ListOfShippers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new ShipperSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[SHIPPER_SEARCH] = condition;
            return View(result);
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