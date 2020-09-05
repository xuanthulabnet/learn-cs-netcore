using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using razor06.form.Validations;

public class CustomerInfo {

    [Required(ErrorMessage="Phải có tên")]
    [StringLength(20, MinimumLength = 3, ErrorMessage="Chiều dài không chính xác")]
    [Display(Name = "TÊN KHÁCH")] // Label hiện thị
    public string Customername {set; get;}

    [Required]
    [EmailAddress]
    [Display(Name = "EMAIL")]
    public string Email {set; get;}

    [Required(ErrorMessage="Thiếu năm sinh")]
    [Display(Name ="NĂM SINH")]
    [Range(1970, 2000, ErrorMessage = "Khoảng năm sinh sai")]
    [MyValidation]
    public int? YearOfBirth {set; get;}
}