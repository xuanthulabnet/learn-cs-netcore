using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace mvc01_HelloWorld {
    public class Startup {

    public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllersWithViews ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
                // Thông báo chỉ truy cập bằng https
                app.UseHsts ();
            }

            // Middleware HttpsRedirection -> Chuyển hướng http sang https
            app.UseHttpsRedirection ();

            // StaticFileMiddleware - truy cập file tĩnh
            app.UseStaticFiles ();

            app.UseRouting ();

            // AuthorizationMiddleware 
            app.UseAuthorization ();

            // Tạo các Endpoint
            app.UseEndpoints (endpoints => {
               


                endpoints.MapControllerRoute (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                    
                // endpoints.MapControllerRoute (
                //     name: "myroute", // đặt tên route
                //     defaults : new { controller = "Home", action = "Index" },
                //     constraints : new {
                //         id = @"\d+", // id phải có và phải là số
                //         title = new RegexRouteConstraint (new Regex (@"^[a-z0-9-]*$")) // title chỉ chứa số, chữ thường và ký hiệu -
                //     },
                //     pattern: "{title}-{id}.html");

                 endpoints.MapControllerRoute (
                    name: "myroute", // đặt tên route
                    defaults : new { controller = "Home", action = "Index" },
                    pattern: "{title:alpha:maxlength(8)}-{id:int}.html");
            });
        }
    }
}