using System;
using System.ComponentModel.DataAnnotations;

namespace CS026_Attribute {

  public class Employer {
    [Required (ErrorMessage = "Employee {0} is required")]
    [StringLength (100, MinimumLength = 3, ErrorMessage = "Tên từ 3 đến  100 ký tự")]
    [DataType (DataType.Text)]
    public string Name { get; set; }

    [Range (18, 99, ErrorMessage = "Age should be between 18 and 99")]
    public int Age { get; set; }


    [DataType (DataType.PhoneNumber)]
    [Phone]
    public string PhoneNumber { set; get; }

    [DataType (DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }

  }



}