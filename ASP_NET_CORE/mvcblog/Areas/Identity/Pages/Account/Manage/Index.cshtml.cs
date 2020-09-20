using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
// using Album.Binder;
using mvcblog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Album.Areas.Identity.Pages.Account.Manage {
    [Authorize]
    public partial class IndexModel : PageModel {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IndexModel (
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Display(Name = "Tên tài khoản")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel {
            [Phone]
            [Display (Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }

            [MaxLength (100)]
            [Display(Name = "Họ tên đầy đủ")]
            public string FullName { set; get; }

            [MaxLength (255)]
            [Display(Name = "Địa chỉ")]
            public string Address { set; get; }

            [DataType (DataType.Date)]
            [Display(Name = "Ngày sinh d/m/y")]
            // [ModelBinder(BinderType=typeof(DayMonthYearBinder))]
            [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime? Birthday { set; get; }

        }

        // Nạp thông tin từ User vào Model
        private async Task LoadAsync (AppUser user) {
            var userName = await _userManager.GetUserNameAsync (user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync (user);
            Username = userName;
            Input = new InputModel {
                PhoneNumber = phoneNumber,
                Birthday = user.Birthday,
                Address = user.Address,
                FullName = user.FullName
            };
        }

        public async Task<IActionResult> OnGetAsync () {
            var user = await _userManager.GetUserAsync (User);

            if (user == null) {
                return NotFound ($"Không tải được tài khoản ID = '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync (user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync () {
            var user = await _userManager.GetUserAsync (User);
            
            if (user == null) {
                return NotFound ($"Không có tài khoản ID: '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid) {
                await LoadAsync(user);
                return Page ();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync (user);
            if (Input.PhoneNumber != phoneNumber) {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync (user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded) {
                    StatusMessage = "Lỗi cập nhật số điện thoại.";
                    return RedirectToPage ();
                }
            }

            // Cập nhật các trường bổ sung
            user.Address  = Input.Address;
            user.Birthday = Input.Birthday;
            user.FullName = Input.FullName;
            await _userManager.UpdateAsync(user);

            // Đăng nhập lại để làm mới Cookie (không nhớ thông tin cũ)
            await _signInManager.RefreshSignInAsync (user);
            StatusMessage = "Hồ sơ của bạn đã cập nhật";
            return RedirectToPage ();
        }
    }
}