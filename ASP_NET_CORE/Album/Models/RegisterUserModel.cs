using System.ComponentModel.DataAnnotations;

namespace Album.Models {
    public class RegisterUserModel {
        [Required]
        [EmailAddress]
        [Display (Name = "Địa chỉ email")]
        public string Email { get; set; }

        [Required]
        [StringLength (100, ErrorMessage = "{0} phải dài {2} tới {1} ký tự.", MinimumLength = 3)]
        [DataType (DataType.Password)]
        [Display (Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType (DataType.Password)]
        [Display (Name = "Xác nhận mật khẩu")]
        [Compare ("Password", ErrorMessage = "Phải giống với password đã nhập.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Tên User - Viết liền không dấu")]
        [DataType(DataType.Text)]
        public string UserName {set; get;}
    }
}