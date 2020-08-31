using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace razor01.basic {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            // services.AddRazorPages().AddRazorPagesOptions(options => {
            //     options.RootDirectory = "/Pages";
            // });

            services.Configure<RouteOptions> (options => {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddRazorPages ().
            AddRazorPagesOptions (options => {

                // Thêm Page Route (Rewrite) cho thư mục gốc
                // Truy cập /chinh-sach.html  là truy cập /Privacy
                options.Conventions.AddPageRoute (
                    "/Privacy",
                    "/chinh-sach.html"
                );

                // Thêm Page Route cho trang trong Areas
                // Truy cập /sanpham/ten-san-pham = /Product/Detail/ten-san-pưham
                options.Conventions.AddAreaPageRoute (
                    areaName: "Product",
                    pageName: "/Detail",
                    route: "/sanpham/{nameproduct?}"
                );
            });
        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Error");
                app.UseHsts ();
            }
            // Chuyển hướng https
            app.UseHttpsRedirection ();
            // Kích hoạt truy cập file tĩnh (file trong wwwroot)
            app.UseStaticFiles ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                // Thêm endpoint chuyển đến các trang Razor Page
                // trong thư mục Pages
                endpoints.MapRazorPages ();
            });
        }
    }
}