using System;
using Microsoft.Extensions.DependencyInjection;
using razor08.efcore.Data;
using System.Linq;

namespace razor08.efcore.Models
{
    public static class InsertTestArticle
    {
        public static void InsertArticle(IServiceProvider serviceProvider) {
           using(var context = serviceProvider.GetService<ArticleContext>())
           {
                Console.WriteLine("Insert");
               if (context.Article.Any()) {
                   // Đã có dữ liệu
                   return;
               }

               context.AddRange(new Article[] {
                   new Article() {
                       Title = "Giới thiệu C# và viết chương trình CS đầu tiên",
                       PublishDate = DateTime.Parse("1-2-2020"),
                       Content = @"Giới thiệu C#, cài đặt .NET Core SDK, VSC và viết 
                                   chương trình CS (C# CSharp) đầu tiên chạy đa nền tảng Windows, 
                                   macOS, Linux, hàm Main trong C# và viết các 
                                   comment - ghi chú - xml document"
                   },
                   new Article() {
                       Title = "Biến hằng số kiểu dữ liệu và nhập xuất dữ liệu C# .NET Core",
                       PublishDate = DateTime.Parse("2-2-2020"),
                       Content = @"Tìm hiểu về biến - hằng số, cách khai báo và khởi tạo biến 
                                   cùng các kiểu dữ liệu cơ bản trong C#, khai báo biến kiểu 
                                   ngầm định var, tiến hành nhập xuất dữ liệu với Console"
                   },
                   new Article() {
                       Title = "Các toán tử tính toán số học trong C# toán tử gán và tăng giảm",
                       PublishDate = DateTime.Parse("3-2-2020"),
                       Content = @"(C#) Khái niệm về toán tử, các loại toán tử số học như 
                                   + - / *, thứ tự ưu tiên toán tự trong biểu thức, 
                                   các loại toán tử gán và toán tử tăng giảm"
                   },   
                   new Article() {
                       Title = "Toán tử so sánh logic và các câu lệnh if switch trong C# .NET",
                       PublishDate = DateTime.Parse("4-2-2020"),
                       Content = @"Giới thiệu C#, cài đặt .NET Core SDK, VSC và viết chương trình CS 
                                   (C# CSharp) đầu tiên chạy đa nền tảng Windows, macOS,
                                   Các toán tử so sánh như so sánh bằng, so sánh lớn hơn .. 
                                   cùng các phép toán logic và, hoặc hay phủ định, 
                                   áp dụng viết câu lệnh điều kiện if else hay câu lệnh rẽ nhiều nhánh 
                                   switch case trong lập trình C#"
                   },
                   new Article() {
                       Title = "Vòng lặp trong trong C# for do while và câu lệnh break continue",
                       PublishDate = DateTime.Parse("5-2-2020"),
                       Content = @"Tạo các vòng lặp for, while, do while trong C# và sử dụng câu lệnh 
                                   .điều hướng vòng lặp continue, break"
                   },
               });
               context.SaveChanges();

           }
        }
  
    }
}