using System;

namespace CS008_Anonymous
{
    public class Logs
    {
        // Khai báo một delegate
        public delegate void ShowLog(string message);

        // Phương thức tương đồng với ShowLog (tham số, kiểu trả về)
        static public void Info(string s)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Format("Info: {0}", s));
            Console.ResetColor();
        }
        
        // Phương thức tương đồng với ShowLog (tham số, kiểu trả về)
        static public void Warning(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("Waring: {0}", s));
            Console.ResetColor();
        }

        public static void TestShowLog()
        {
            ShowLog showLog;

            showLog = Info;         // showLog gán bằng phương thức Info
            showLog("Thông báo");   // Thi hành delegate chính là thi hành Info

            showLog = Warning;      // showLog gán bằng phương thức Warning
            showLog("Thông báo");   // Thi hành delegate chính là thi hành Info
        }

        // Gán nhiều hàm vào Delegate tạo thành chuỗi
        public static void TestShowLogMulti()
        {
            ShowLog showLog;

            showLog = null;
            showLog += Warning;         // Nối thêm Warning vào delegate
            showLog += Info;            // Nối thêm Info vào delegate
            showLog += Warning;         // Nối thêm Warning vào delegate

            //Một lần gọi thi hành tất cả các phương thức trong chuỗi delegate
            showLog("TestLog");  //Hoặc an toàn: showLog?.Invoke("TestLog");
        }

        // Cộng nhiều Delegate
        public static void TestShowLogPlus()
        {
            ShowLog showLog1 = (x)=> {Console.WriteLine($"-----{x}-----");};
            ShowLog showLog2 = Warning;
            ShowLog showLog3 = Info;

            var all = showLog1 + showLog2 + showLog3 + showLog1;

            all("Xin Chào");
        }
    }
}
