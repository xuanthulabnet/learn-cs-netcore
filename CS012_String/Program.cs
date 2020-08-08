using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
namespace CS12_String {
    class Program {

        /// <summary>
        /// Ví dụ khai báo chuỗi
        /// </summary>
        static void khaibaochuoi() {
            string sExample = "Xin chào";   // Khai báo và khởi tạo chuỗi
            sExample += " các bạn";         // Nối chuỗi +=, trả về "Xin chào các bạn"
            sExample = sExample + "!";      // Nối chuỗi +, trả về "Xin chào các bạn!"
            char c = sExample[1];           // c= 'i'

        }

        /// <summary>
        /// Ví dụ định dạng với $
        /// </summary>
        static void vidu1()
        {
            Console.WriteLine($"{"VòngLặp",10} {"Chẵn/Lẻ", -5}");
            for (int i = 8; i < 15; i++)
            {
                string chanle = (i%2 == 0) ? "Chẵn" : "Lẻ";
                Console.WriteLine($"{i,10} {chanle, -5}");
            }
        }

        /// <summary>
        /// Một số phép toán trên chuỗi
        /// </summary>
        static void vidu2()
        {
            string stringA =  "Xin chào,";
            string stringB = "các bạn!";

            // Nối chuỗi
            string s = String.Concat(stringA, stringB);
            Console.WriteLine(s);

            // Tạo chuỗi format
            s = String.Format("Chào {0}, {0} ơi, hôm nay ngày {1} rồi!", "Nam", DateTime.Now.Day);
            Console.WriteLine(s);

            // Chèn vào chuỗi
            s = stringA.Insert(8, " tất cả");
            Console.WriteLine(s);

            // Pad
            string s1 = "Abc";
            string s2 = s1.PadLeft(6);        //  "   Abc"
            string s3 = s1.PadLeft(6, '*');   //  "***Abc"
            Console.WriteLine(s2);
            Console.WriteLine(s2);

            // Thay thế chuỗi
            s = stringA.Replace("chào", "CHÀO");   // "Xin CHÀO,"
            Console.WriteLine(s);

            // Chia nhỏ chuỗi
            var split = "Nguyễn Văn A".Split(' '); 
            foreach (var item in split)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Dùng StringBuilder
        /// </summary>
        static void vidu3()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Xin chào các bạn - xuanthulab.net");
            stringBuilder.Replace("Xin chào", "XIN CHÀO");
            Console.WriteLine(stringBuilder); // Out: XIN CHÀO các bạn - xuanthulab.net
        }

        static void vidu4()
        {
            // https://xuanthulab.net/ham-preg-match-preg-match-all-trong-php.html#example
            String text = @"Đây là địa chỉ 
                email userabcguest@xuanthulab.net.vn và 
                xyz@gmail.com cần trích xuất";
            String pattern = @"(([^\s.]+)@((.[^\s]+)(\..[^\s]+)))";
        
            Regex rx = new Regex(pattern);

            // Tìm kiếm.
            MatchCollection matches = rx.Matches(text);
            // In thông báo tìm kiếm.
            Console.WriteLine("Tìm thấy {0} email trong:\n\n  {1}\n\n",
                            matches.Count,
                            text);
            // Xuất kết quả email tìm được
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                Console.WriteLine("{0}", groups[0].Value);
            }
        }


        static void Main (string[] args) {
            // khaibaochuoi();
            // vidu1();
            // vidu2();
            // vidu3();
            vidu4();
        }
    }
}