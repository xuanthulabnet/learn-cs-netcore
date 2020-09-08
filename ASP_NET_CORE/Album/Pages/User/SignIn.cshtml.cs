using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
            return RedirectToPage ("Index");

        }
        // Đăng nhập theo thông tin Post đến
        public async Task<IActionResult> OnPost () {

            // Đã đăng nhập nên chuyển hướng về Index
            if (_signInManager.IsSignedIn (User)) return Redirect ("Index");

            if (ModelState.IsValid) {
                // Thử login bằng username/password
                var rs = await _signInManager.PasswordSignInAsync (
                    signInInfoModel.UserNameOrEmail,
                    signInInfoModel.Password,
                    signInInfoModel.RememberMe,
                    true
                );

                if (!rs.Succeeded) {
                    // Thất bại username/password -> tìm user theo email, nếu thấy thì thử đăng nhập
                    // bằng user tìm được
                    var user = await _userManager.FindByEmailAsync (signInInfoModel.UserNameOrEmail);
                    if (user != null) {
                        rs = await _signInManager.PasswordSignInAsync (
                            user,
                            signInInfoModel.Password,
                            signInInfoModel.RememberMe,
                            true
                        );
                    }
                }

                // Đăng nhập thành công
                if (rs.Succeeded) return RedirectToPage ("Index");

                // Đăng nhật thất bại, thiết lập một thông báo lỗi
                if (rs.IsLockedOut) {
                    int attempt =  _signInManager.Options.Lockout.MaxFailedAccessAttempts;
                    ModelState.AddModelError(string.Empty, $"Tài khoản bị khóa thất bại trên {attempt} thất bại");

                } else {
                    ModelState.AddModelError (string.Empty, "Thông tin không chính xác hoặc tài khoản không có");
                }

            }
            return Page ();
        }
    }
}