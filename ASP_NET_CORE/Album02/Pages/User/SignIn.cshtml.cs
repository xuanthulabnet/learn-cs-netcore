using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using XTLASPNET;

namespace MyApp.Namespace {
    public class SignInModel : PageModel {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<SignInModel> _logger;

        public SignInModel (SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ILogger<SignInModel> logger) {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public SignInInfoModel signInInfoModel { set; get; }
        public IActionResult OnGet () {

            if (_signInManager.IsSignedIn (User)) {
                _logger.LogInformation ("Đã đăng nhập username = " + _userManager.GetUserName (User));
                return RedirectToPage ("Index");
            }
            return Page ();

        }

        // Thực hiện đăng xuất khi truy cập
        // https://localhost:5001/user/sign/signout
        public async Task<IActionResult> OnGetSignOut () {
            if (_signInManager.IsSignedIn (User)) {
                _logger.LogInformation (User.Identity.Name + " sign out");
                await _signInManager.SignOutAsync ();
            }
            
            return ViewComponent(MessagePage.COMPONENTNAME,
                new MessagePage.Message() {
                    title = "Đã đăng xuất",
                    htmlcontent = "Tài khoản đã đăng xuất khỏi hệ thống",
                    urlredirect = Url.Page("Index")
                }
            );

        }
        // Đăng nhập theo thông tin Post đến
        public async Task<IActionResult> OnPost () {

            // Đã đăng nhập nên chuyển hướng về Index
            if (_signInManager.IsSignedIn (User)) return Redirect ("Index");

            if (ModelState.IsValid) {

                // Tìm user theo tên đăng nhập hoặc email
                IdentityUser user = await _userManager.FindByNameAsync(signInInfoModel.UserNameOrEmail);
                if (user == null) {
                    user = await _userManager.FindByEmailAsync(signInInfoModel.UserNameOrEmail);
                }
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại");
                    return Page();
                }
                if (!user.EmailConfirmed) 
                {
                    ModelState.AddModelError(string.Empty, "Bạn cần xác nhận email trước khi đăng nhập");
                    return Page();
                }

                var rs = await _signInManager.PasswordSignInAsync (
                    user.UserName,
                    signInInfoModel.Password,
                    signInInfoModel.RememberMe,
                    true
                );
 
                // Đăng nhập thành công
                if (rs.Succeeded) 
                    return ViewComponent(MessagePage.COMPONENTNAME, 
                        new MessagePage.Message() {
                            title = "Đăng nhập thành công",
                            htmlcontent = $"Xin chào, {user.UserName}",
                            urlredirect = Url.Page("Index")
                        }
                    ); 


                // Đăng nhật thất bại, thiết lập một thông báo lỗi
                if (rs.IsLockedOut) {
                    int attempt =  _signInManager.Options.Lockout.MaxFailedAccessAttempts;
                    return ViewComponent(MessagePage.COMPONENTNAME, new MessagePage.Message() {
                        title = "TẠM KHÓA",
                        htmlcontent = $"Tài khoản bị khóa {attempt} đăng nhập thất bại"
                    });

                } 
                else if (rs.IsNotAllowed) {
                    ModelState.AddModelError (string.Empty, "Tài khoản không được phép đăng nhập");
                }
                else {
                    ModelState.AddModelError (string.Empty, "Thông tin không chính xác hoặc tài khoản không có");
                }

            }
            return Page ();
        }


        public async Task<IActionResult> OnGetConfirmEmail(string userid, string code) {
            
            var user = (userid == null) ? null : await _userManager.FindByIdAsync(userid);
            if (user == null || code == null)
            {
                return ViewComponent(MessagePage.COMPONENTNAME, new MessagePage.Message() {
                    htmlcontent = "Lỗi, không có dữ liệu"
                });
            }
            Console.WriteLine(code);
            // Cập nhật vào Db user đã được xác nhận email
            var result = await _userManager.ConfirmEmailAsync(user, code);
            string thongbao = "Đã xác nhận email thành công";
            if (!result.Succeeded)
            {
                thongbao = "Không xác nhận được email";
                foreach (var err in result.Errors)
                {
                    thongbao += err.Description;
                }
            }

            return ViewComponent(MessagePage.COMPONENTNAME, new MessagePage.Message() {
                title = "Xác nhận email",
                htmlcontent = thongbao,
                urlredirect = Url.Page("Index"),
                secondwait = 10
            });

        }
    }
}