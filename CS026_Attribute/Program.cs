using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CS026_Attribute {
    class Program {
        public class MyClass {

            [Obsolete ("Phương thức này lỗi thời, hãy  dùng phương thức Abc")]
                public static void Method1 () {
                    Console.WriteLine ("Phương thức chạy");
                }
        }
        public static void checkValidationContext()
        {
            Employer user    = new Employer();
            user.Name        = "AF";
            user.Age         = 6;
            user.PhoneNumber = "1234as";
            user.Email       = "test@re";


            ValidationContext context       = new ValidationContext(user, null, null);
            // results - lưu danh sách ValidationResult, kết quả kiểm tra
            List<ValidationResult> results  = new List<ValidationResult>();
            // thực hiện kiểm tra dữ liệu
            bool valid = Validator.TryValidateObject(user, context, results, true);

            if (!valid)
            {
                // Duyệt qua các lỗi và in ra
                foreach (ValidationResult vr in results)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{vr.MemberNames.First(), 13}");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"    {vr.ErrorMessage}");
                }
            }
        }
        static void Main (string[] args) {
            // MyClass.Method1();
            // TestReadAttribute.test();
            checkValidationContext();
        }
    }
}