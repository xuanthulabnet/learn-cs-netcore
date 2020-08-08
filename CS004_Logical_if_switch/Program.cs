using System;

namespace CS004_Logical_if_switch
{
    class Program
    {
        static void Main(string[] args)
        {
            // // VÍ DỤ SO SÁNH
            // int a = 5;
            // int b = 6;
            // // var c = (a == b);
            // var c = (a > b);
            // // var c = (a >= b);
            // // var c = (a < b);
            // // var c = (a <= b);
            // // var c = (a != b);


            //  // VÍ DỤ LOGIC   
            //  bool a = false;
            //  bool b = true;
            //  bool c = a && b;
            //  bool d = !a;
            //  bool e = a || b;



            // //In ra mà hình nếu là số chẵn
            // int number = 1990;
            // if ((number % 2) == 0)
            //     Console.WriteLine($"{number} là số chẵn");


            // int a = 5;
            // int b = 10;
            // if (a >= b) 
            // {
            //     Console.WriteLine("Số a lớn hơn hoặc bằng số b");
            // }
            // else
            // {
            //     Console.WriteLine("Số a nhỏ hơn số b");
            // }



            // int a = 10;
            // int b = 10;
            // if (a > b) 
            // {
            //     Console.WriteLine("Số a lớn hơn hoặc bằng số b");
            // }
            // else if (a < b)
            // {
            //     Console.WriteLine("Số a nhỏ hơn số b");
            // }
            // else 
            // {
            //     Console.WriteLine("Hai số a, b bằng nhau");
            // }
 
            // int age = 18;
            // var mgs = (age >= 18) ? "Đủ điều kiện" : "Không đủ điều kiện";    
            // Console.WriteLine(mgs);


            // var a = 2;
            // var b = 3.5;
            // var c = 2;
            // var max = a >= b ?  a >= c ? a : c : b >=c ? b : c;
            // // Viết tường minh hơn
            // // max = (a >= b) ?  (a >= c ? a : c) : (b >=c ? b : c);
            // Console.WriteLine(max);


            // int number = 2;
            // switch (number)
            // {
            //     case 1:
            //         Console.WriteLine("number có giá trị một");
            //     break;
            //     case 2:
            //         Console.WriteLine("number có giá trị hai");
            //     break;
            //     default:
            //         Console.WriteLine("number khác một và hai");
            //     break;
            // }

            int number = 2;
            if (number == 1)
            {
                Console.WriteLine("number có giá trị một");   
            }
            else if (number == 2)
            {
                Console.WriteLine("number có giá trị hai");
            }
            else 
            {
                Console.WriteLine("number khác một và hai");
            }
        }
    }
}
