using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mvcblog.Controllers {
    public class LearnAspController : Controller {

        public class Student {
            public int ID { set; get; }
            public string StudentName { set; get; }
            public string Email { set; get; }

        }

        public Student firststudent { set; get; }

        [FromQuery (Name = "name")]
        [StringLength(255, MinimumLength=5)]
        public string StudentName { set; get; }

        [HttpGet]
        [Route ("testviewpost/{Postid?}/")]
        public JsonResult testviewpost (int postid, bool viewall) {

            return Json (new {
                postid = postid,
                    viewall = viewall,
                    student = StudentName
            });
        }

        [AcceptVerbs ("GET", "POST", "PUT")]
        [Route ("abc-[controller]-xyz[action]")] // Url phù hợp = /abc-learnasp-xyztest
        public IActionResult Test () {
            return Content ("Kiểm tra route");
        }
        // [HttpGet("hoc-lap-trinh-asp/{id:int?}/", Name = "routeabc")]
        public IActionResult Index () {
            return View ();
        }

    }
}