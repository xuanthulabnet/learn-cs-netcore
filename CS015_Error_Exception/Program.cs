using System;

namespace CS015_Error_Exception
{
    class Program
    { 
        public static void UserInput(string  s) {
            if (s.Length > 10)
            {
                Exception e = new DataTooLongExeption();
                throw e;    // lỗi văng ra
            }
            //Other code - no exeption
        } 
        static void Main(string[] args)
        { 
            /* Phát sinh Exception */
            // int[] mynumbers = new int[] {1,2,3};
            // int i = mynumbers[10];     // chỗ này phát sinh lỗi
            

            /* Thực hành bắt lỗi Exception */
            // try {
            //     // khối này được giám sát để bắt lỗi - khi nó phát sinh
            //     int[] mynumbers = new int[] {1,2,3};
            //     int i = mynumbers[10];                  // dòng này phát sinh lỗi
            //     Console.WriteLine(i);                   // dòng này không được thực thi vì lỗi trên
            // }
            // catch (Exception loi)
            // {
            //     // khối này thực thi khi bắt được lỗi
            //     Console.WriteLine("Có lỗi rồi");
            //     Console.WriteLine(loi.Message);
            // }

             try {
                 UserInput("Đây là một chuỗi rất dài ...");
             }
             catch (DataTooLongExeption e) {
                 Console.WriteLine(e.Message);
             }
             catch (Exception otherExeption) {
                 Console.WriteLine(otherExeption.Message);
             }
        }
    }
}
