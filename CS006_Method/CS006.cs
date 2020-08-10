using System;
namespace MyLib
{
    public class CS006
    { 
        static public void XinChao() 
        {
            Console.WriteLine("Xin chào C# \n");
        }

        // Khai báo hàm có 2 tham số kiểu int, trả trà số kiểu in
        public static int SoLon(int num1, int num2)
        {
            int max = (num1 > num2) ? num1 : num2;
            return max;
        }

        // Phương thức khai báo có 3 tham số, hai tham số cuối mặc định
        // Nếu gọi hàm không có chỉ ra tham số cuỗi thì nó lấy giá trị mặc định này
        public static double TheTich(double cao, double dai = 1, double rong = 1)
        {
            return cao * dai * rong;
        }

        
        public static string FullName(string ho, string ten, string tendem ="")
        {
            return  ho + (tendem != "" ? " " + tendem :"")  + " " + ten; 
        }
    }
}