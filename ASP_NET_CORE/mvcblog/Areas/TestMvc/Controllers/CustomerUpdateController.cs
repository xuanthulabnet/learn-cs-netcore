using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace mvcblog.Areas.TestMvc.Controllers {
    // Thiết lập Controller thuộc Area TestMvc
    [Area ("TestMvc")]
    public class CustomerUpdateController : Controller {
        
        // Tạo lớp Model CustomerInfo biểu diễn khách hàng được submit đến từ Form
        public class CustomerInfo {

            [Required (ErrorMessage = "Phải có tên")]
            [StringLength (20, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
            [Display (Name = "TÊN KHÁCH")] // Label hiện thị
            public string Customername { set; get; }

            [Required (ErrorMessage = "Thiếu email")]
            [EmailAddress]
            [Display (Name = "EMAIL")]
            public string Email { set; get; }

            [Required (ErrorMessage = "Thiếu năm sinh")]
            [Display (Name = "NĂM SINH")]
            [Range (1970, 2000, ErrorMessage = "{0} phải trong khoảng {1} đến {2}")]
            public int? YearOfBirth { set; get; }
        }


        //  customerInfo được binding từ dữ liệu gửi đến
        [BindProperty] 
        public CustomerInfo customerInfo { set; get; }

        [TempData]
        public string Message {set;get;}

        public IActionResult Index() {

            // Kiểm tra trạng thái hợp lệ của dữ liệu Model, khi form submit (post)
            if (ModelState.IsValid && Request.Method == HttpMethod.Post.Method)  {
                Message = "Dữ liệu gửi đến hợp lệ";
                Console.WriteLine(Request.Method);
            }
            else {
                Message = "Điền lại thông tin gửi đến";
            }
            ViewData["Message"] = Message;
            // Trả về ViewResult, gửi customerInfo là Model đến view Index.cshtml
            return View (customerInfo);

        }

    }
}