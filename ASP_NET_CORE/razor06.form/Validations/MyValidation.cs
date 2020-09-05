using System.ComponentModel.DataAnnotations;
using System;
namespace razor06.form.Validations
{
    // Kiểm tra các số chẵn là phù hợp
    public class MyValidation: ValidationAttribute
    {
        public MyValidation() {
            ErrorMessage = "Không phải số chẵn";
        }
        public override bool IsValid(object value) {
            if (value == null) return false;
            int number = Int32.Parse(value.ToString());
            bool chiahetcho2 = number % 2 == 0;
            return chiahetcho2;

        }
    }
}