using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace di_fileconfig {
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

            // Đọc file config ứng dụng
            var configBuilder = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json");
            var configurationroot = configBuilder.Build ();

            var services = new ServiceCollection ();
            services.AddOptions ();
            // Đưa Option của MyServiceOptions vào, lưu ý phải cài package: Microsoft.Extensions.Options.ConfigurationExtensions
            services.Configure<MyServiceOptions> (configurationroot.GetSection ("MyServiceOptions"));
            // Đăng ký dịch vụ
            services.AddTransient<MyService> ();

            var serviceprovider = services.BuildServiceProvider (); // Tạo serviceprovider

            var myservice = serviceprovider.GetService<MyService> (); // yêu cầu dịch vụ MyService
            myservice.ShowData ();

            // Kết quả chạy: (config đã nạp và inject vào service)
            //  MyService created
            //  ABCDE - 123456

        }
    }
}