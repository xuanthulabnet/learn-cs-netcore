using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mvcblog.Data;
using mvcblog.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using XTLASPNET;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace mvcblog {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {

            // Đăng ký AppDbContext, sử dụng kết nối đến MS SQL Server
            services.AddDbContext<AppDbContext> (options => {
                string connectstring = Configuration.GetConnectionString ("MyBlogContext");
                options.UseSqlServer (connectstring);
            });
            // Đăng ký các dịch vụ của Identity
            services.AddIdentity<AppUser, IdentityRole> ()
                .AddEntityFrameworkStores<AppDbContext> ()
                .AddDefaultTokenProviders ();

            // Truy cập IdentityOptions
            services.Configure<IdentityOptions> (options => {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại

            });

            // Cấu hình Cookie
            services.ConfigureApplicationCookie (options => {
                // options.Cookie.HttpOnly = true;  
                options.ExpireTimeSpan = TimeSpan.FromMinutes (30);
                options.LoginPath = $"/login/"; // Url đến trang đăng nhập
                options.LogoutPath = $"/logout/";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied"; // Trang khi User bị cấm truy cập
            });
            services.Configure<SecurityStampValidatorOptions> (options => {
                // Trên 5 giây truy cập lại sẽ nạp lại thông tin User (Role)
                // SecurityStamp trong bảng User đổi -> nạp lại thông tinn Security
                options.ValidationInterval = TimeSpan.FromSeconds (5);
            });

            services.Configure<RouteOptions> (options => {
                options.AppendTrailingSlash = false; // Thêm dấu / vào cuối URL
                options.LowercaseUrls = true; // url chữ thường
                options.LowercaseQueryStrings = false; // không bắt query trong url phải in thường
            });
            
            services.AddOptions (); // Kích hoạt Options
            var mailsettings = Configuration.GetSection ("MailSettings"); // đọc config
            services.Configure<MailSettings> (mailsettings); // đăng ký để Inject

            services.AddTransient<IEmailSender, SendMailService> (); // Đăng ký dịch vụ Mail


            services.AddAuthorization(options =>
            {
                // User thỏa mãn policy khi có roleclaim: permission với giá trị manage.user
                options.AddPolicy("AdminDropdown", policy => {
                    policy.RequireClaim("permission", "manage.user");
                });
                
            });


            services.AddControllersWithViews ();
            services.AddRazorPages ();
        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
                app.UseHsts ();
            }
            app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            app.UseRouting ();

            app.UseAuthentication (); // Phục hồi thông tin đăng nhập (xác thực)
            app.UseAuthorization (); // Phục hồi thông tinn về quyền của User

            app.UseEndpoints (endpoints => {

                    endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute (
                    name: "learnasproute", // đặt tên route
                    defaults : new { controller = "LearnAsp", action = "Index" },
                    pattern: "learn-asp-net/{id:int?}");

                // Đến Razor Page    
                endpoints.MapRazorPages ();
            });

            app.Map ("/testapi", app => {
                app.Run (async context => {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var ob = new {
                        url = context.Request.GetDisplayUrl (),
                        content = "Trả về từ testapi"
                    };
                    string jsonString = JsonConvert.SerializeObject (ob);
                    await context.Response.WriteAsync (jsonString, Encoding.UTF8);
                });
            });

            app.Run (async (HttpContext context) => {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync ("Page not found (xuanthulab)!");
            });

        }
    }
}