using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _01.helloworld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Build -  tạo các dịch vụ đã đăng ký trả về WebHost
            // Run - chạy ứng dụng web
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // webBuilder đối tượng lớp WebHostBuilder để cấu hình, đăng ký các dịch vụ ứng dụng Web
                    // UseStartup chỉ ra lớp khởi chạy ứng dụng (đăng ký dịch vụ)
                    webBuilder.UseStartup<Startup>();
                });
    }
}
