using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _04.ServiceCollection {
    public class Startup {
        IServiceCollection  _services;


        public void ConfigureServices (IServiceCollection services) {

            services.AddSingleton<IListProductName, PhoneName> (); //  đăng ký dịch vụ, đối tượng chỉ tạo một lần
            services.AddTransient<LaptopName, LaptopName> (); //  đăng ký dịch vụ, tạo mới  mỗi lần  triệu gọi
            services.AddTransient<ProductController, ProductController> ();
            _services = services;


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseStaticFiles ();

            app.UseRouting ();

            app.Map ("/Product", app => {
                app.Run (async (context) => {
                    // Gọi đến dịch vụ ProductController
                    var productcontroller = app.ApplicationServices.GetService<ProductController> ();
                    await productcontroller.List (context);
                });
            });

            app.MapWhen (
                (context) => {
                    return context.Request.Path.Value.StartsWith ("/Abcxyz");
                },

                appProduct => {
                    appProduct.Run (async (context) => {
                        await appProduct.ApplicationServices.GetService<ProductController> ().List (context);
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

            app.UseEndpoints (endpoints => {
                endpoints.MapGet ("/", async context => {
                    await context.Response.WriteAsync ("Hello World!");
                });
            });
        }
    }
}