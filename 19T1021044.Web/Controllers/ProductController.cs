using _19T1021044.BusinessLayers;
using _19T1021044.DomainModels;
using _19T1021044.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021044.Web.Controllers
{
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 5;//1 giá trị dùng từ 2 lần trở lên nên dùng hằng
        private const string PRODUCT_SEARCH = "SearchProductCondition";
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationProductSearchInput condition = Session[PRODUCT_SEARCH] as PaginationProductSearchInput;

            if (condition == null)
            {
                condition = new PaginationProductSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    CategoryID = 0,
                    SupplierID = 0
                };
            }


            return View(condition);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationProductSearchInput condition)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page, condition.PageSize, condition.SearchValue, condition.CategoryID, condition.SupplierID, out rowCount);
            var result = new ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                CategoryID = condition.CategoryID,
                SupplierID = condition.SupplierID,
                RowCount = rowCount,
                Data = data
            };

            Session[PRODUCT_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Product()
            {
                ProductID = 0
            };
            ViewBag.Title = "Bổ sung mặt hàng";
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Save(Product data, HttpPostedFileBase UploadPhoto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.ProductName))
                    ModelState.AddModelError("ProductName", "Tên Sản Phẩm Không Được Để Trống");
                if (data.CategoryID == 0)
                    ModelState.AddModelError("CategoryID", "Mã Loại Hàng Không Được Để Trống");
                if (data.SupplierID == 0)
                    ModelState.AddModelError("SupplierID", "Mã Nhà Cung Cấp Không Được Để Trống");
                if (string.IsNullOrWhiteSpace(data.Unit))
                    ModelState.AddModelError("Unit", "Đơn Vị Tính Không Được Để Trống");
                if (data.Price <= 0)
                    ModelState.AddModelError("Price", "Giá Không Hợp Lệ");
                //if (string.IsNullOrWhiteSpace(data.Photo))
                //    ModelState.AddModelError("Photo", "Vui Lòng Chọn Ảnh");



                if (UploadPhoto != null)
                {
                    string path = Server.MapPath("~/Photos"); //mappath: lấy đường dẫn vật lí
                    string fileName = $"{DateTime.Now.Ticks}_{UploadPhoto.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);//cộng chuỗi
                    UploadPhoto.SaveAs(filePath);
                    data.Photo = $"/Photos/{fileName}";
                }
                else if(string.IsNullOrWhiteSpace(data.Photo))
                    ModelState.AddModelError("Photo", "Vui Lòng Chọn Ảnh");

                if (!ModelState.IsValid)
                {
                    return View("Create", data);
                }
                int productID = ProductDataService.AddProduct(data);
                //PaginationProductSearchInput input = Session[PRODUCT_SEARCH] as PaginationProductSearchInput;
                //input.SearchValue = data.ProductName;
                //input.CategoryID = data.CategoryID;
                //input.SupplierID = data.SupplierID;
                //Session[PRODUCT_SEARCH] = input;
                return RedirectToAction($"Edit/{productID}");
            }
            catch
            {
                return Content("Có lỗi xảy ra. Vui lòng thử lại!");
            }
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [Route("edit/{id?}")]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var data = ProductDataService.GetProduct(id);
            var Photos = ProductDataService.ListPhotos(id);
            var Attributes = ProductDataService.ListAttributes(id);
            if (data == null)
                return RedirectToAction("Index");
            ProductEditModel model = new ProductEditModel()
            {
                Product = data,
                Photos = Photos,
                Attributes = Attributes
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult SaveEdit(ProductEditModel data, HttpPostedFileBase UploadPhoto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.Product.ProductName))
                    ModelState.AddModelError("Product.ProductName", "Tên Sản Phẩm Không Được Để Trống");
                if (data.Product.CategoryID == 0)
                    ModelState.AddModelError("Product.CategoryID", "Mã Loại Hàng Không Được Để Trống");
                if (data.Product.SupplierID == 0)
                    ModelState.AddModelError("Product.SupplierID", "Mã Nhà Cung Cấp Không Được Để Trống");
                if (string.IsNullOrWhiteSpace(data.Product.Unit))
                    ModelState.AddModelError("Product.Unit", "Đơn Vị Tính Không Được Để Trống");
                if (data.Product.Price <= 0)
                    ModelState.AddModelError("Product.Price", "Giá Không Hợp Lệ");


                if (UploadPhoto != null)
                {
                    string path = Server.MapPath("~/Photos"); //mappath: lấy đường dẫn vật lí
                    string fileName = $"{DateTime.Now.Ticks}_{UploadPhoto.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);//cộng chuỗi
                    UploadPhoto.SaveAs(filePath);
                    data.Product.Photo = $"/Photos/{fileName}";
                }
                else
                {
                    data.Product.Photo = ProductDataService.GetProduct(data.Product.ProductID).Photo;
                }
                if (!ModelState.IsValid)
                {
                    var productPictures = ProductDataService.ListPhotos(data.Product.ProductID);
                    var productAttributes = ProductDataService.ListAttributes(data.Product.ProductID);
                    var product = ProductDataService.GetProduct(data.Product.ProductID);
                    var data2 = new ProductEditModel()
                    {
                        Product = product,
                        Photos = productPictures,
                        Attributes = productAttributes
                    };
                    return View("Edit", data2);
                }
                ProductDataService.UpdateProduct(data.Product);
                //PaginationProductSearchInput input = Session[PRODUCT_SEARCH] as PaginationProductSearchInput;
                //input.SearchValue = data.Product.ProductName;
                //input.CategoryID = data.Product.CategoryID;
                //input.SupplierID = data.Product.SupplierID;
                //Session[PRODUCT_SEARCH] = input;
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Có lỗi xảy ra. Vui lòng thử lại!");
            }
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Delete(int id = 0)
        {
            var Photos = ProductDataService.ListPhotos(id);
            var Attributes = ProductDataService.ListAttributes(id);
            if (id == 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "GET")
            {
                var data = ProductDataService.GetProduct(id);
                if (data == null)
                    return RedirectToAction("Index");
                return View(data);
            }
            else
            {
                foreach (var item in Photos)
                    ProductDataService.DeletePhoto(item.PhotoID);
                foreach (var item in Attributes)
                    ProductDataService.DeleteAttribute(item.AttributeID);
                ProductDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            switch (method)
            {
                case "add":
                    var data1 = new ProductPhoto()
                    {
                        ProductID = productID,
                        PhotoID = 0,
                    };
                    ViewBag.Title = "Bổ sung ảnh";
                    return View(data1);
                case "edit":
                    var data2 = ProductDataService.GetPhoto(photoID);
                    ViewBag.Title = "Thay đổi ảnh";
                    return View(data2);
                case "delete":
                    ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction($"Edit/{productID}");
                default:
                    return RedirectToAction("Edit");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SavePhoto/{ProductID}")]
        public ActionResult SavePhoto(ProductPhoto model, HttpPostedFileBase UploadPhoto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Description))
                    ModelState.AddModelError("Description", "Mô Tả Không Được Để Trống");
                if (model.DisplayOrder <= 0)
                    ModelState.AddModelError("DisplayOrder", "Thứ Tự Hiển Thị Không Hợp Lệ");



                if (UploadPhoto != null)
                {
                    string path = Server.MapPath("~/Photos"); //mappath: lấy đường dẫn vật lí
                    string fileName = $"{DateTime.Now.Ticks}_{UploadPhoto.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);//cộng chuỗi
                    UploadPhoto.SaveAs(filePath);
                    model.Photo = $"/Photos/{fileName}";
                }
                else if(string.IsNullOrWhiteSpace(model.Photo))
                {
                    ModelState.AddModelError("Photo", "Vui Lòng Chọn Ảnh Hiển Thị");
                }


                if (model.IsHidden.Equals("true"))
                    model.IsHidden = true;

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = model.PhotoID == 0 ? "Bổ sung ảnh" : "Thay đổi ảnh";
                    return View("Photo", model);
                }

                if (model.PhotoID == 0)
                {
                    ProductDataService.AddPhoto(model);
                }
                else
                {
                    ProductDataService.UpdatePhoto(model);
                }

                return RedirectToAction($"Edit/{model.ProductID}");
            }
            catch
            {
                return Content("Có lỗi xảy ra. Vui lòng thử lại!");
            }
        }
        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, int attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    var data1 = new ProductAttribute()
                    {
                        ProductID = productID,
                        AttributeID = 0,
                    };
                    ViewBag.Title = "Bổ sung thuộc tính";
                    return View(data1);
                case "edit":
                    var data2 = ProductDataService.GetAttribute(attributeID);
                    ViewBag.Title = "Thay đổi thuộc tính";
                    return View(data2);
                case "delete":
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SaveAttribute/{ProductID}")]
        public ActionResult SaveAttribute(ProductAttribute model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.AttributeName))
                    ModelState.AddModelError("AttributeName", "Tên Thuộc Tính Không Được Để Trống");
                if (string.IsNullOrWhiteSpace(model.AttributeValue))
                    ModelState.AddModelError("AttributeValue", "Giá Trị Thuộc Tính Không Được Để Trống");
                if (model.DisplayOrder <= 0)
                    ModelState.AddModelError("DisplayOrder", "Thứ Tự Hiển Thị Không Hợp Lệ");


                if (!ModelState.IsValid)
                {
                    ViewBag.Title = model.AttributeID == 0 ? "Bổ sung thuộc tính" : "Thay đổi thuộc tính";
                    return View("Attribute", model);
                }


                if (model.AttributeID == 0)
                {
                    ProductDataService.AddAttribute(model);
                }
                else
                {
                    ProductDataService.UpdateAttribute(model);
                }

                return RedirectToAction($"Edit/{model.ProductID}");
            }
            catch
            {
                return Content("Có lỗi xảy ra. Vui lòng thử lại!");
            }
        }
    }
}