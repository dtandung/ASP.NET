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
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 5;//1 giá trị dùng từ 2 lần trở lên nên dùng hằng
        private const string CATEGORY_SEARCH = "SearchCategoryCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[CATEGORY_SEARCH] as PaginationSearchInput;

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
            var data = CommonDataService.ListOfCategories(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new CategorySearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[CATEGORY_SEARCH] = condition;
            return View(result);
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
        public ActionResult Edit(int id = 0)
		{
            //int categoryId = Convert.ToInt32(id);
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetCategory(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập Nhật Thông Tin Loại Hàng";
            return View(data);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError("CategoryName", "Tên Không Được Để Trống");
                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError("Description", "Chi Tiết Không Được Để Trống");
                

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CategoryID == 0 ? "Bổ sung loại hàng" : "Cập nhật loại hàng";
                    return View("Edit", data);
                }

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
            catch
            {
                return Content("Có lỗi xảy ra. Vui lòng thử lại!");
            }
            
        }
        /// <summary>
        /// Xoá 1 Loại Hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
		{
            //int categoryID = Convert.ToInt32(id);
            if (id == 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCategory(id);
                if (data == null)
                    return RedirectToAction("Index");
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
        }
    }
}