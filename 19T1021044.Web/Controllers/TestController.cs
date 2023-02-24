using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021044.Web.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public ActionResult Input()
        {
            Person p = new Person() 
            { 
                BirthDate = new DateTime(2001, 11,24)
            };
            return View(p);
        }
        [HttpPost]
        public ActionResult Input(Person p)
        {
            var data = new
            {
                Name = p.Name,
                BirhtDate = string.Format("{0:yyyy/MM/dd}", p.BirthDate),
                Salary = p.Salary
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public string TestDate(DateTime value)
        {
            DateTime d = value;
            return string.Format("{0:yyyy/MM/dd}", d);
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; } = new DateTime(2001,11, 24);
        public float Salary { get; set; } = -1;
    }
}