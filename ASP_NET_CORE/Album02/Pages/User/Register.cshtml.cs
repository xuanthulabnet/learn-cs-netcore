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
using XTLASPNET;

namespace MyApp.Namespace
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;

        private readonly ISendMailService _sendMail;

        // Model chứa thông tin để đăng ký
        [BindProperty(SupportsGet=true)]
        public RegisterUserModel registerUserModel {set; get;}      

        // Các dịch vụ Inject vào PageModel bằng phương thức khởi tạo
        public RegisterModel(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ILogger<RegisterModel>  logger, ISendMailService sendMail)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _sendMail = sendMail;
        }

        public IActionResult OnGet()
        {
            

            if (_signInManager.IsSignedIn (User))
                return ViewComponent(MessagePage.COMPONENTNAME,
                    new MessagePage.Message() {
                        title = "Đã đăng nhập",
                        htmlcontent = "Tài khoản " + _userManager.GetUserName(User) + " đã đăng nhập",
                        urlredirect = Url.Page("Index")

                    }
                );

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

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    
                    // Tạo Url xác nhận email và gửi mail kích hoạt
                    // https://localhost:5001/user/sign/confirmemail?userid=userid&code=code
                    var url  = Url.Page("/User/SignIn", "ConfirmEmail", new {userid = user.Id, code = code}, 
                                         Request.Scheme);
                    await _sendMail.SendMail(
                        new MailContent() {
                            To = user.Email,
                            Subject = "Kích hoạt tài khoản",
                            Body = @$"Bạn cần bấm vào để <a href=""{url}"">Kích hoạt tài khoản</a>"
                        }
                    );

                    _logger.LogInformation("Đã tạo user mới");

                    // Thông báo và chuyển hướng về trang đăng nhập
                    return ViewComponent(MessagePage.COMPONENTNAME, new MessagePage.Message() {
                        title = "Tạo tài khoản thành công",
                        htmlcontent = "Một email đã gửi cho bạn, hãy mở email làm theo hướng dẫn để kích hoạt",
                        urlredirect = Url.Page("/User/SignIn")
                    });

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