using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razor05.modelbinding.Model;

namespace razor05.modelbinding.Pages {
    public class IndexModel : PageModel {


        [BindProperty(SupportsGet=true)]
        public Customer customer {set; get;}

        // Binding Email từ dữ liệu từ nguồn tới có tên Email, email, emaIL ...
        [BindProperty]
        public string Email { get; set; }

        // Binding cho UserId từ nguồn gửi đến, dữ liệu nguồn có tên username

        [BindProperty (Name = "username")]
        public string UserId { set; get; }

        // Binding ProductID - thiết lập BINDING ngay cả khi truy cập là HTTP GÉT

        
        [BindProperty(SupportsGet=true)]
        public int ProductID { set; get; }

        // Binding Color

        [BindProperty]
        public string Color { set; get; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel (ILogger<IndexModel> logger) {
            _logger = logger;

        }

        public void OnGet (int? productID, string color) {
            Console.WriteLine ($"ProductID: {productID}; color: {color}");
        }
        public void OnPost () {
         
            // Microsoft.AspNetCore.Http.Extensions -> GetDisplayUrl
            Console.WriteLine(Request.GetDisplayUrl());
            var req = Request; 
        }
        
    }
}