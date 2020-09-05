using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor06.form.Pages
{
    public class FormModel : PageModel
    {
        public string Mesage {set; get;}
        [BindProperty]
        public CustomerInfo customerInfo {set; get;}

        public void OnPost() {
            if (ModelState.IsValid) {
                Mesage = "Dữ liệu Post chính xác";
                // Xử lý, chuyển hướng ...
            }
            else {
                Mesage = "Lỗi dữ liệu";
            }
        }
    }
}