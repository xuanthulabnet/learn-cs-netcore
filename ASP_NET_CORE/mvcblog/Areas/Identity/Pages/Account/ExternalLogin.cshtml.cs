using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using mvcblog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using XTLASPNET;

namespace Album.Areas.Identity.Pages.Account {
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;

        public ExternalLoginModel (
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender) {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel {
            [Required]
            [EmailAddress]
            [Display (Name = "Địa chỉ email")]
            public string Email { get; set; }
        }

        public IActionResult OnGetAsync () {
            return RedirectToPage ("./Login");
        }

        // Post yêu cầu login bằng dịch vụ ngoài
        // Provider = Google, Facebook ... 
        public async Task<IActionResult> OnPost (string provider, string returnUrl = null) {
            // Kiểm tra yêu cầu dịch vụ provider tồn tại
            var listprovider = (await _signInManager.GetExternalAuthenticationSchemesAsync ()).ToList ();
            var provider_process = listprovider.Find ((m) => m.Name == provider);
            if (provider_process == null) {
                return NotFound ("Dịch vụ không chính xác: " + provider);
            }

            // redirectUrl - là Url sẽ chuyển hướng đến - sau khi CallbackPath (/dang-nhap-tu-google) thi hành xong
            // nó bằng identity/account/externallogin?handler=Callback 
            // tức là gọi OnGetCallbackAsync 
            var redirectUrl = Url.Page ("./ExternalLogin", pageHandler: "Callback", values : new { returnUrl });

            // Cấu hình 
            var properties = _signInManager.ConfigureExternalAuthenticationProperties (provider, redirectUrl);

            // Chuyển hướng đến dịch vụ ngoài (Googe, Facebook)
            return new ChallengeResult (provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync (string returnUrl = null, string remoteError = null) {
            returnUrl = returnUrl ?? Url.Content ("~/");
            if (remoteError != null) {
                ErrorMessage = $"Lỗi provider: {remoteError}";
                return RedirectToPage ("./Login", new { ReturnUrl = returnUrl });
            }

            // Lấy thông tin do dịch vụ ngoài chuyển đến
            var info = await _signInManager.GetExternalLoginInfoAsync ();
            if (info == null) {
                ErrorMessage = "Lỗi thông tin từ dịch vụ đăng nhập.";
                return RedirectToPage ("./Login", new { ReturnUrl = returnUrl });
            }

            // Đăng nhập bằng thông tin LoginProvider, ProviderKey từ info cung cấp bởi dịch vụ ngoài
            // User nào có 2 thông tin này sẽ được đăng nhập - thông tin này lưu tại bảng UserLogins của Database
            // Trường LoginProvider và ProviderKey ---> tương ứng UserId 
            var result = await _signInManager.ExternalLoginSignInAsync (info.LoginProvider, info.ProviderKey, isPersistent : false, bypassTwoFactor : true);

            if (result.Succeeded) {
                // User đăng nhập thành công vào hệ thống theo thông tin info
                _logger.LogInformation ("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect (returnUrl);
            }
            if (result.IsLockedOut) {
                // Bị tạm khóa
                return RedirectToPage ("./Lockout");
            } else {

                var userExisted = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (userExisted != null) {
                    // Đã có Acount, đã liên kết với tài khoản ngoài - nhưng không đăng nhập được
                    // có thể do chưa kích hoạt email
                    return RedirectToPage ("./RegisterConfirmation", new { Email = userExisted.Email });

                }

                // Chưa có Account liên kết với tài khoản ngoài
                // Hiện thị form để thực hiện bước tiếp theo ở OnPostConfirmationAsync
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim (c => c.Type == ClaimTypes.Email)) {
                    // Có thông tin về email từ info, lấy email này hiện thị ở Form
                    Input = new InputModel {
                    Email = info.Principal.FindFirstValue (ClaimTypes.Email)
                    };
                }

                return Page ();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync (string returnUrl = null) {
            returnUrl = returnUrl ?? Url.Content ("~/");
            // Lấy lại Info
            var info = await _signInManager.GetExternalLoginInfoAsync ();
            if (info == null) {
                ErrorMessage = "Không có thông tin tài khoản ngoài.";
                return RedirectToPage ("./Login", new { ReturnUrl = returnUrl });
            }

            

            if (ModelState.IsValid) {

                string externalMail = null;
                if (info.Principal.HasClaim (c => c.Type == ClaimTypes.Email)) {
                    externalMail = info.Principal.FindFirstValue (ClaimTypes.Email);
                }
                var userWithexternalMail = (externalMail != null) ? (await _userManager.FindByEmailAsync (externalMail)) : null;

                // Xử lý khi có thông tin về email từ info, đồng thời có user với email đó
                // trường hợp này sẽ thực hiện liên kết tài khoản ngoài + xác thực email luôn     
                if ((userWithexternalMail != null) && (Input.Email == externalMail)) {
                    // xác nhận email luôn nếu chưa xác nhận
                    if (!userWithexternalMail.EmailConfirmed) {
                        var codeactive = await _userManager.GenerateEmailConfirmationTokenAsync (userWithexternalMail);
                        await _userManager.ConfirmEmailAsync (userWithexternalMail, codeactive);
                    }
                    // Thực hiện liên kết info và user
                    var resultAdd = await _userManager.AddLoginAsync (userWithexternalMail, info);
                    if (resultAdd.Succeeded) {
                        // Thực hiện login    
                        await _signInManager.SignInAsync (userWithexternalMail, isPersistent : false);
                        return ViewComponent (MessagePage.COMPONENTNAME, new MessagePage.Message () {
                            title = "LIÊN KẾT TÀI KHOẢN",
                                urlredirect = returnUrl,
                                htmlcontent = $"Liên kết tài khoản {userWithexternalMail.UserName} với {info.ProviderDisplayName} thành công"
                        });
                    } else {
                        return ViewComponent (MessagePage.COMPONENTNAME, new MessagePage.Message () {
                            title = "LIÊN KẾT TÀI KHOẢN",
                                urlredirect = Url.Page ("Index"),
                                htmlcontent = $"Liên kết thất bại"
                        });
                    }
                }

                // Tài khoản chưa có, tạo tài khoản mới
                var user = new AppUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync (user);
                if (result.Succeeded) {

                    // Liên kết tài khoản ngoài với tài khoản vừa tạo
                    result = await _userManager.AddLoginAsync (user, info);
                    if (result.Succeeded) {
                        _logger.LogInformation ("Đã tạo user mới từ thông tin {Name}.", info.LoginProvider);
                        // Email tạo tài khoản và email từ info giống nhau -> xác thực email luôn
                        if (user.Email == externalMail) {
                            var codeactive = await _userManager.GenerateEmailConfirmationTokenAsync (user);
                            await _userManager.ConfirmEmailAsync (user, codeactive);
                            await _signInManager.SignInAsync (user, isPersistent : false, info.LoginProvider);
                            return ViewComponent (MessagePage.COMPONENTNAME, new MessagePage.Message () {
                                title = "TẠO VÀ LIÊN KẾT TÀI KHOẢN",
                                    urlredirect = returnUrl,
                                    htmlcontent = $"Đã tạo và liên kết tài khoản, kích hoạt email thành công"
                            });
                        }

                        // Trường hợp này Email tạo User khác với Email từ info (hoặc info không có email)
                        // sẽ gửi email xác để người dùng xác thực rồi mới có thể đăng nhập
                        var userId = await _userManager.GetUserIdAsync (user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync (user);
                        code = WebEncoders.Base64UrlEncode (Encoding.UTF8.GetBytes (code));
                        var callbackUrl = Url.Page (
                            "/Account/ConfirmEmail",
                            pageHandler : null,
                            values : new { area = "Identity", userId = userId, code = code },
                            protocol : Request.Scheme);

                        // Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.
                        await _emailSender.SendEmailAsync (Input.Email, "Xác nhận địa chỉ email",
                            $"Hãy xác nhận địa chỉ email bằng cách <a href='{callbackUrl}'>bấm vào đây</a>.");

                        // Chuyển đến trang thông báo cần kích hoạt tài khoản
                        if (_userManager.Options.SignIn.RequireConfirmedEmail) {
                            return RedirectToPage ("./RegisterConfirmation", new { Email = Input.Email });
                        }

                        // Đăng nhập ngay do không yêu cầu xác nhận email
                        await _signInManager.SignInAsync (user, isPersistent : false, info.LoginProvider);

                        return LocalRedirect (returnUrl);
                    }
                }
                foreach (var error in result.Errors) {
                    ModelState.AddModelError (string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page ();
        }
    }
}