using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor06.form.Binding;
using System;
namespace razor06.form.Pages
{
    public class FormModel : PageModel
    {
        public string Mesage {set; get;}
        [BindProperty]
        // [ModelBinder(BinderType = typeof(MyCheckNameBinding), Name = "customerInfo.Customername")]
        public CustomerInfo customerInfo {set; get;}

        public void OnPost() {
            if (ModelState.IsValid) {
                Mesage = "Dữ liệu Post chính xác";
                ModelState.Clear();

                Console.WriteLine(customerInfo.Customername);
                
                // Xử lý, chuyển hướng ...
            }
            else {
                Mesage = "Lỗi dữ liệu";
            }
        }
    }
}