using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace di_options {
    public class MyServiceOptions {
        public string data1 { set; get; }
        public int data2 { set; get; }
    }

    public class MyService {
        readonly string data1;
        readonly int data2;
        public MyService (IOptions<MyServiceOptions> options) // IOption làm tham số khởi tạo, nó tự động được Inject
        {
            data1 = options.Value.data1; // Truy cập đến đối tượng lớp MyServiceOption qua  Value
            data2 = options.Value.data2;
            Console.WriteLine ("MyService created");
        }
        public void ShowData () => Console.WriteLine ($"{data1} - {data2}");
    }
    class Program {
        static void Main (string[] args) {

            var services = new ServiceCollection ();
            services.AddOptions ();
            // Đăng ký Options vào Config ServiceCollection
            services.Configure<MyServiceOptions> (
                (MyServiceOptions options) => {
                    options.data1 = "DATA1";
                    options.data2 = 56789;
                }
            );
            // Đăng ký dịch vụ
            services.AddTransient<MyService> ();

            var serviceprovider = services.BuildServiceProvider (); // Tạo serviceprovider

            var myservice = serviceprovider.GetService<MyService> (); // yêu cầu dịch vụ MyService
            myservice.ShowData ();

            // Kết quả chạy
            // MyService created
            // DATA1 - 56789

        }
    }
}