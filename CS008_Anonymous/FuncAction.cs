using System;

namespace CS008_Anonymous
{
    class FuncAction
    {
        static int Sum(int x, int y)
        {
            return x + y;
        }

        // Sử dụng Func
        public static void TestFunc(int x, int y)
        { 
            Func<int,int,int> tinhtong;         // biến tinhtong phù hợp với các hàm trả về kiểu int, có 2 tham số kiểu int
            tinhtong = Sum;                     // Hàm Sum phù hợp nên có thể gán cho biến
            
            var ketqua = tinhtong(x, y);
            Console.WriteLine(ketqua);  
        }

        // Sử dụng Action
        public static void TestAction(string s)
        {
            Action<string> showLog = null;

            showLog += Logs.Warning;         // Nối thêm Warning vào delegate
            showLog += Logs.Info;            // Nối thêm Info vào delegate
            showLog += Logs.Warning;         // Nối thêm Warning vào delegate

            // Một lần gọi thi hành tất cả các phương thức trong chuỗi delegate
            showLog("TestLog");
        }

        // Sử dụng Delegate làm tham số phương thức, truyền callback
        static void TinhTong(int a, int b, Action<string> callback)
        {
            int c = a + b;
            // Gọi callback
            callback(c.ToString());
        }

        public static void TestTinhTong()
        {
            TinhTong(5,6, (x) => Console.WriteLine($"Tổng hai số là: {x}"));
            TinhTong(1,3, Logs.Info);
        }
    }
}
