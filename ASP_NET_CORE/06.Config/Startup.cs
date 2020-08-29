using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _06.Config {
    public class Startup {
        IServiceCollection _services;

        IConfiguration _configuration;
        //..

        // Thêm khởi tạo để lấy IConfiguration của ứng dụng
        public Startup (IConfiguration configuration) {
            _configuration = configuration;
        }

        public void ConfigureServices (IServiceCollection services) {

            services.AddSingleton<IListProductName, PhoneName> (); //  đăng ký dịch vụ, đối tượng chỉ tạo một lần
            services.AddTransient<LaptopName, LaptopName> (); //  đăng ký dịch vụ, tạo mới  mỗi lần  triệu gọi
            services.AddTransient<ProductController, ProductController> ();
            _services = services;

            services.AddDistributedMemoryCache (); // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            services.AddSession (cfg => { // Đăng ký dịch vụ Session
                cfg.Cookie.Name = "xuanthulab"; // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                cfg.IdleTimeout = new TimeSpan (0, 60, 0); // Thời gian tồn tại của Session
            });

            services.AddOptions (); // Kích hoạt Options
            var testoptions = _configuration.GetSection ("TestOptions"); // đọc config
            // đăng ký để Inject, dịch vụ nào cần TestOptions sẽ lấy được testoptions
            services.Configure<TestOptions> (testoptions); 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseStaticFiles ();

            app.UseSession ();

            app.UseRouting ();

            app.Map ("/Product", app => {
                app.Run (async (context) => {
                    // Gọi đến dịch vụ ProductController
                    var productcontroller = app.ApplicationServices.GetService<ProductController> ();
                    await productcontroller.List (context);
                });
            });

            app.Map ("/ShowOptions", appOptions => {
                appOptions.Run (async (context) => {

                    StringBuilder stb = new StringBuilder ();
                    IConfiguration configuration = appOptions.ApplicationServices.GetService<IConfiguration> ();

                    TestOptions testoptions = configuration.GetSection ("TestOptions").Get<TestOptions> ();
                    var opt_key1 = testoptions.opt_key1;
                    var k1 = testoptions.opt_key2.k1;
                    var k2 = testoptions.opt_key2.k2;

                    stb.Append ($"TestOptions.opt_key1:  {opt_key1}\n");
                    stb.Append ($"TestOptions.opt_key2.k1:  {k1}\n");
                    stb.Append ($"TestOptions.opt_key2.k2:  {k2}\n");

                    await context.Response.WriteAsync (stb.ToString ().HtmlTag ("pre"));

                });
            });

            app.Map ("/allservice", app01 => {
                app01.Run (async (context) => {

                    var stringBuilder = new StringBuilder ();
                    stringBuilder.Append ("<tr><th>Tên</th><th>Lifetime</th><th>Tên đầy đủ</th></tr>");
                    foreach (var service in _services) {
                        string tr = service.ServiceType.Name.ToString ().HtmlTag ("td") +
                            service.Lifetime.ToString ().HtmlTag ("td") +
                            service.ServiceType.FullName.HtmlTag ("td");
                        stringBuilder.Append (tr.HtmlTag ("tr"));
                    }

                    string htmlallservice = stringBuilder.ToString ().HtmlTag ("table", "table table-bordered table-sm");
                    string html = HtmlHelper.HtmlDocument ("Các dịch vụ", (htmlallservice));

                    await context.Response.WriteAsync (html);
                });
            });

            app.Map ("/RequestInfo", app01 => {
                app01.Run (async (context) => {
                    string menu = HtmlHelper.MenuTop (HtmlHelper.DefaultMenuTopItems (), context.Request);
                    string requestinfo = RequestProcess.RequestInfo (context.Request).HtmlTag ("div", "container");

                    string accessinfo = ProductController.CountAccessInfo (context).HtmlTag ("div", "container");

                    string html = HtmlHelper.HtmlDocument ("Thông tin Request", (menu + accessinfo + requestinfo));
                    await context.Response.WriteAsync (html);
                });
            });

            app.UseEndpoints (endpoints => {
                endpoints.MapGet ("/", async context => {
                    await context.Response.WriteAsync ("Hello World!");
                });
            });
        }
    }
}