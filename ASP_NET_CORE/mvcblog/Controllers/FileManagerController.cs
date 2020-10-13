using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvcblog.Models;

namespace mvcblog.Controllers
{
    [Route("/file-manager")]
    public class FileManagerController : Controller
    {
      public IActionResult Index()
      {
        return View();
      }   
    }
}
