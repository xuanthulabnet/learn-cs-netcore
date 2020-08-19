using System;
using System.Collections.Generic;
using System.Linq; // Nạp thư viện LINQ

namespace CS020_ExtensionMethod {

    // Lớp tĩnh, nơi có thể khai báo phương thức mở rộng
    public static class MyExtensionMethods {
        // Mở rộng cho string, có thêm phương thức print
        public static void Print (this string s, ConsoleColor color = ConsoleColor.Yellow) {
            ConsoleColor lastColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine (s);
            Console.ForegroundColor = lastColor;
        }
    }

    class Program {

        // In ra màn hình chuỗi với màu color
        public static void Print (string s, ConsoleColor color = ConsoleColor.Yellow) {
            ConsoleColor lastColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine (s);
            Console.ForegroundColor = lastColor;
        }

        static void Main (string[] args) {
            /* Ví dụ dùng phương thức mở rộng */
            // // Linq đã mở rộng thêm vào List phương thức Where
            // List<int> ls = new List<int>() {1,2,3,4};
            // var ps = ls.Where(i => i >= 3);      

            string s = "Chuỗi kiểm tra";
            s.Print();     
            "Xin chào các bạn!".Print(ConsoleColor.Red);

        }
    }
}