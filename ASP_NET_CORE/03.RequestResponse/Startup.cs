using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _03.RequestResponse {
    public class Startup {

        // Đăng ký các dịch vụ sử dụng bởi ứng dụng, services là một DI container
        public void ConfigureServices (IServiceCollection services) {
            services.AddDistributedMemoryCache ();
            services.AddSession ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            app.UseStaticFiles ();
            app.UseSession ();

            // Điều hướng route bởi EndpointMiddleware
            // Rẽ nhánh nếu Url phù hợp định nghĩa trong UseEndpoints,  nếu không chuyển đến Middleware tiếp
            app.UseRouting ();
            app.UseEndpoints (endpoints => {

                endpoints.MapGet ("/", async context => {
                    string menu = HtmlHelper.MenuTop (HtmlHelper.DefaultMenuTopItems (), context.Request);
                    string content = HtmlHelper.HtmlTrangchu ();
                    string html = HtmlHelper.HtmlDocument ("Trang chủ", menu + content);
                    await context.Response.WriteAsync (html);
                });

                endpoints.Map ("/RequestInfo", async context => {
                    string menu = HtmlHelper.MenuTop (HtmlHelper.DefaultMenuTopItems (), context.Request);
                    string requestinfo = RequestProcess.RequestInfo (context.Request).HtmlTag ("div", "container");
                    string html = HtmlHelper.HtmlDocument ("Thông tin Request", (menu + requestinfo));
                    await context.Response.WriteAsync (html);
                });

                endpoints.MapGet ("/Encoding", async context => {
                    string menu = HtmlHelper.MenuTop (HtmlHelper.DefaultMenuTopItems (), context.Request);
                    string htmlec = RequestProcess.Encoding (context.Request).HtmlTag ("div", "container");
                    string html = HtmlHelper.HtmlDocument ("Encoding", (menu + htmlec));
                    await context.Response.WriteAsync (html);
                });

                endpoints.MapGet("/Cookies/{*action}", async (context) => {
                    string menu     = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
                    string cookies  = RequestProcess.Cookies(context.Request, context.Response).HtmlTag("div", "container");
                    string html    = HtmlHelper.HtmlDocument("Đọc / Ghi Cookies", (menu + cookies));
                    await context.Response.WriteAsync(html);
                });

            });

            // Điểm rẽ nhánh pipeline khi URL là /Json
            app.Map ("/Json", app => {
                app.Run (async context => {
                    string Json  = RequestProcess.GetJson();
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(Json);
                });
            });

            // Điểm rẽ nhánh pipeline khi URL là /Form
            app.Map ("/Form", app => {
                app.Run (async context => {

                    string menu = HtmlHelper.MenuTop (HtmlHelper.DefaultMenuTopItems (), context.Request);
                    string formhtml = await RequestProcess.FormProcess (context.Request);
                    formhtml = formhtml.HtmlTag ("div", "container");
                    string html = HtmlHelper.HtmlDocument ("Form Post", (menu + formhtml));
                    await context.Response.WriteAsync (html);

                });
            });

            app.Run (async (HttpContext context) => {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync ("Page not found!");
            });

        }
    }
}