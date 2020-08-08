using System;

namespace CS001_HelloWorld {
    class Program // Đây là một ghi chú 1 dòng, đặt ngay sau lệnh C#
    {
        static void Main (string[] args) {
            // Đây là một dòng ghi chú 1 dòng riêng biệt - dòng này không ảnh hưởng đến code

            Console.WriteLine ("Xin chào C# NET CORE!");
        }

        /// <summary>
        /// Tính tổng hai số nguyên
        /// </summary>
        /// <param name="a">số thứ nhất</param>
        /// <param name="b">số thứ hai</param>
        /// <returns>giá trị a + b</returns>
        static int TongHaiSo (int a, int b) {
            return a + b;
        }
    }
}