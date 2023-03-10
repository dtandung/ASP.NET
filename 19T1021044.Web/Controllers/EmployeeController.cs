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
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 3;//1 giá trị dùng từ 2 lần trở lên nên dùng hằng
        private const string EMPLOYEE_SEARCH = "SearchEmployeeCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as PaginationSearchInput;

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
            var data = CommonDataService.ListOfEmployees(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[EMPLOYEE_SEARCH] = condition;
            return View(result);
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
        public ActionResult Edit(int id = 0)
        {
            //int employeeId = Convert.ToInt32(id);
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetEmployee(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập Nhật Thông Tin Nhân Viên";
            return View(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Employee data, string birthday, HttpPostedFileBase UploadPhoto)//Employee data
        {

            try
            {
                DateTime? d = Converter.DMYStringToDateTime(birthday);
                if (d == null)
                    ModelState.AddModelError("BirthDate", $"Ngày Sinh {birthday} Chưa Đúng Định Dạng");
                else
                {
                    DateTime startday = new DateTime(1753, 1, 1);
                    DateTime enday = new DateTime(9999, 12, 31);
                    if (d > startday && d < enday)
                        data.BirthDate = d.Value;
                    else
                    {
                        ModelState.AddModelError("BirthDate", "Ngày Sinh Chưa Đúng Định Dạng");
                    }
                }



                if (string.IsNullOrWhiteSpace(data.LastName))
                    ModelState.AddModelError("LastName", "Họ Đệm Không Được Để Trống");
                if (string.IsNullOrWhiteSpace(data.FirstName))
                    ModelState.AddModelError("FirstName", "Tên Không Được Để Trống");
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("Email", "Email Không Được Để Trống");
                if (string.IsNullOrWhiteSpace(data.Notes))
                    ModelState.AddModelError("Notes","Ghi Chú Không Được Để Trống");
               

                


                if (UploadPhoto != null)
                {
                    string path = Server.MapPath("~/Photos"); //mappath: lấy đường dẫn vật lí
                    string fileName = $"{DateTime.Now.Ticks}_{UploadPhoto.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);//cộng chuỗi
                    UploadPhoto.SaveAs(filePath);
                    data.Photo = $"/Photos/{fileName}";
                }
                else
                {
                    if(data.EmployeeID != 0)
                        data.Photo = CommonDataService.GetEmployee(data.EmployeeID).Photo;
                    else if(string.IsNullOrWhiteSpace(data.Photo))
                        ModelState.AddModelError("Photo", "Vui Lòng Chọn Ảnh Hiển Thị");
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
                    return View("Edit", data);
                }

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
            catch
            {
                return Content("Có lỗi xảy ra. Vui lòng thử lại!");
            }
        }
        /// <summary>
        /// Giao diện xoá nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            //int employeeId = Convert.ToInt32(id);
            if (id == 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetEmployee(id);
                if (data == null)
                    return RedirectToAction("Index");
                return View(data);
            }
            else
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
        }
    }
}