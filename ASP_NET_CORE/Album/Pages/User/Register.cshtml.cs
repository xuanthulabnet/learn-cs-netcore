using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Album.Models;

namespace MyApp.Namespace
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;

        // Model chứa thông tin để đăng ký
        [BindProperty(SupportsGet=true)]
        public RegisterUserModel registerUserModel {set; get;}      

        // Các dịch vụ Inject vào PageModel bằng phương thức khởi tạo
        public RegisterModel(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ILogger<RegisterModel>  logger )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // Test:
            //  _signInManager.SignInAsync(new AppUser() {UserName = "xuanthulab"}, true).Wait();
            if (_signInManager.IsSignedIn(User)) {
                _logger.LogInformation("Đã đăng nhập username = " +  _userManager.GetUserName(User));
                return RedirectToPage("Index");
            }
            ModelState.Clear();
            return Page();
        }

        public async Task<IActionResult> OnPost() {

            if (ModelState.IsValid)
            {
                // Tạo đối tượng AppUser
                var user = new AppUser { UserName = registerUserModel.UserName, Email = registerUserModel.Email };
                // Tạo User mới với thông tin từ AppUser - trong hệ thống (Đưa vào Db)
                var result = await _userManager.CreateAsync(user, registerUserModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Đã tạo user mới");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    
                    _logger.LogInformation("Đăng nhập: " + _signInManager.IsSignedIn(User).ToString());
                    // Chuyển hướng về trang User/Index sau khi đăng ký, đăng nhập
                    return RedirectToPage("Index");
                }
                else {
                    // Có lỗi tạo User, thông báo lỗi đưa vào ModelState để hiện thị
                    // trong .cshtml - taghelper asp-validation-summary
                    result.Errors.ToList().ForEach((i) => {
                        ModelState.AddModelError(i.Code, i.Description);
                    });
                }
            }
 
            return Page();

        }
    }
}