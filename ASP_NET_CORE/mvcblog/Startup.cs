using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace mvcblog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions> (options => {
                options.AppendTrailingSlash = false;        // Thêm dấu / vào cuối URL
                options.LowercaseUrls = true;               // url chữ thường
                options.LowercaseQueryStrings = false;      // không bắt query trong url phải in thường
            });


            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute (
                    name: "learnasproute",    // đặt tên route
                    defaults : new { controller = "LearnAsp", action = "Index" },
                    pattern: "learn-asp-net/{id:int?}");

                // Đến Razor Page    
                endpoints.MapRazorPages();    
            });

            app.Map("/testapi", app => {
                app.Run(async context => {
                    context.Response.StatusCode = 500;  
                    context.Response.ContentType = "application/json";
                    var ob = new {
                        url = context.Request.GetDisplayUrl(),
                        content = "Trả về từ testapi"
                    };
                    string jsonString = JsonConvert.SerializeObject(ob);
                    await context.Response.WriteAsync(jsonString, Encoding.UTF8);
                });
            });

            app.Run (async (HttpContext context) => {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync ("Page not found!");
            });

        }
    }
}
