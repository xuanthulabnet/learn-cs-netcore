using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mvcblog.Controllers
{
    public class LearnAspController : Controller
    {

        [Route("abc-[controller]-xyz[action]")] // Url phù hợp = /abc-learnasp-xyztest
        public IActionResult Test()
        {
            return Content("Kiểm tra route");
        }
        // [HttpGet("hoc-lap-trinh-asp/{id:int?}/", Name = "routeabc")]
        public IActionResult Index()
        {
            return View();
        }

    }
}