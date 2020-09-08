using System.ComponentModel.DataAnnotations;

namespace Album.Models
{
    public class SignInInfoModel
    {
        [Required(ErrorMessage="Không để trống")]
        [Display(Name = "Nhập username hoặc email của bạn")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nhập đúng thông tin")]
        public string  UserNameOrEmail {set; get;} 


        [Required]
        [DataType(DataType.Password)]
        public string Password {set;get;}

        [Display(Name = "Lưu thông tin đăng nhập?")]
        public bool RememberMe { get; set; }

    }
}