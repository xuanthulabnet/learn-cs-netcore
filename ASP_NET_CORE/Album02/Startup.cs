using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.Data;
using Album.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Album {
    public class Startup {
        public Startup (IConfiguration configuration) {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddRazorPages ();

            // Đăng ký AppDbContext    
            services.AddDbContext<AppDbContext> (options => {
                // Đọc chuỗi kết nối
                string connectstring = Configuration.GetConnectionString ("ArticleContext");
                // Sử dụng MS SQL Server
                options.UseSqlServer (connectstring);
            });

            services.AddIdentity<AppUser, IdentityRole> ()
                .AddEntityFrameworkStores<AppDbContext> ()
                .AddDefaultTokenProviders ();

            services.Configure<RouteOptions> (options => {
                options.LowercaseUrls = true;               // Url viết chữ thường
                options.LowercaseQueryStrings = true;       // Query trong Url viết chữ thường
            });


            // Truy cập IdentityOptions
            services.Configure<IdentityOptions> (options => {
                // Thiết lập về Password
                options.Password.RequireDigit = false;           // Không bắt phải có số
                options.Password.RequireLowercase = false;       // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false;       // Không bắt buộc chữ in   
                options.Password.RequiredLength = 3;             // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1;        // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (1); // Khóa 1 phút
                options.Lockout.MaxFailedAccessAttempts = 5;                       // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;
        

                // User settings.
                options.User.AllowedUserNameCharacters =  // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;  // Email là duy nhất

                options.SignIn.RequireConfirmedEmail = true;
                
            });

            services.ConfigureApplicationCookie (options => {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes (5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            app.UseRouting ();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints (endpoints => {
                endpoints.MapRazorPages ();
            });
        }
    }
}