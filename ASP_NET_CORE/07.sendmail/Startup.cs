using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _07.sendmail {
    public class Startup {

        // Thêm khởi tạo để lấy IConfiguration của ứng dụng
        IConfiguration _configuration;
        public Startup (IConfiguration configuration) {
            _configuration = configuration;
        }

        public void ConfigureServices (IServiceCollection services) {

            // ...     

            services.AddOptions (); // Kích hoạt Options
            var mailsettings = _configuration.GetSection ("MailSettings");  // đọc config
            services.Configure<MailSettings> (mailsettings);                // đăng ký để Inject

            // Đăng ký SendMailService với kiểu Transient, mỗi lần gọi dịch
            // vụ ISendMailService một đới tượng SendMailService tạo ra (đã inject config)
            services.AddTransient<ISendMailService, SendMailService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseRouting ();

            app.UseEndpoints (endpoints => {
                endpoints.MapGet ("/", async context => {
                    await context.Response.WriteAsync ("Hello World!");
                });

                endpoints.MapGet("/testmail", async context => {

                    // Lấy dịch vụ sendmailservice
                    var sendmailservice = context.RequestServices.GetService<ISendMailService>();  
                   
                    MailContent content = new MailContent {
                        To = "xuanthulab.net@gmail.com",
                        Subject = "Kiểm tra thử",
                        Body = "<p><strong>Xin chào xuanthulab.net</strong></p>"
                    };

                    await sendmailservice.SendMail(content);
                    await context.Response.WriteAsync("Send mail");
                }); 

            });
        }
    }
}