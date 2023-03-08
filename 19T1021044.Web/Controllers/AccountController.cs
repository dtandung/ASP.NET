using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using _19T1021044.BusinessLayers;
using _19T1021044.DomainModels;

namespace _19T1021044.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [AllowAnonymous]//sử dụng mà kh cần đăng nhập
        [HttpPost]
        public ActionResult Login(string userName = "", string passWord = "")
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }
            ViewBag.UserName = userName;
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(passWord))
            {
                ModelState.AddModelError("", "Vui lòng nhập đủ thông tin");
                return View();
            }

            var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, passWord);

            if (userAccount == null)
            {
                ModelState.AddModelError("", "Đăng nhập thất bại");
                return View();
            }
            string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
            FormsAuthentication.SetAuthCookie(cookieValue, false);
            return RedirectToAction("Index", "Home");

        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword, string confirmNewPassWord)
        {
            var model = Converter.CookieToUserAccount(User.Identity.Name);
            userName = model.Email;

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                ModelState.AddModelError("", "Vui lòng nhập đủ thông tin");
                return View();
            }

            if (oldPassword.Equals(model.Password))
            {
                if (newPassword.Equals(confirmNewPassWord))
                {
                    UserAccountService.ChangePassword(AccountTypes.Employee, userName, oldPassword, newPassword);
                    var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, newPassword);
                    string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
                    FormsAuthentication.SetAuthCookie(cookieValue, false);
                    ModelState.AddModelError("", "Đổi mật Khẩu Thành Công");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Mật Khẩu Không Trùng Khớp");
                }
            }
            else
            {
                ModelState.AddModelError("", "Mật Khẩu Cũ Không Đúng");
            }


            return View();




            //string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            //FormsAuthentication.SetAuthCookie(cookieValue, false);
            //return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}