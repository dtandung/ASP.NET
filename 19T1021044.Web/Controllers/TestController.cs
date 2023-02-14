using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021044.Web.Controllers
{
    [RoutePrefix("thu-nghiem")]
    public class TestController : Controller
    {
        // GET: Test
        [Route("xin-chao/{name}/{age?}")]
        public string SayHello(string name, int age = 22)
        {
            return $"Hello {name}, {age} year old";
        }
    }
}