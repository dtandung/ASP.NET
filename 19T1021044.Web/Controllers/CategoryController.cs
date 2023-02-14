using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021044.DomainModels;
using _19T1021044.BusinessLayers;

namespace _19T1021044.Web.Controllers
{
    public class CategoryController : Controller
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
            var model = CommonDataService.ListOfCategorys(page, pageSize, searchValue, out rowCount);

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
        /// Giao diện để bổ sung một loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create() 
        {
            var data = new Category()
            {
                CategoryID = 0
            };
            ViewBag.Title = "Bổ sung loại hàng";
            return View("Edit", data);
        }
        /// <summary>
        /// Giao diện để cập nhật thông tin loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string id)
		{
            int categoryId = Convert.ToInt32(id);

            var data = CommonDataService.GetCategory(categoryId);
            ViewBag.Title = "Cập Nhật Thông Tin Loại Hàng";
            return View(data);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Category data)
        {
            if (data.CategoryID == 0)
            {
                CommonDataService.AddCategory(data);
            }
            else
            {
                CommonDataService.UpdateCategory(data);
            }

            return RedirectToAction("Index");
        }
        /// <summary>
        /// Xoá 1 Loại Hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
		{
            int categoryID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCategory(categoryID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCategory(categoryID);
                return RedirectToAction("Index");
            }
        }
    }
}