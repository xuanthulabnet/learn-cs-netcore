using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace ef01
{
    public class ProductsContext : DbContext
    {
           // Tạo ILoggerFactory 
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
        builder
            //    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Warning)
            //    .AddFilter(DbLoggerCategory.Query.Name, LogLevel.Debug)
               .AddConsole();

            }
        ); 




        // Thuộc tính products kiểu DbSet<Product> cho biết CSDL có bảng mà
        // thông tin về bảng dữ liệu biểu diễn bởi model Product
        public DbSet<Product> products {set; get;}

        // Chuỗi kết nối tới CSDL (MS SQL Server)
        private const string connectionString = @"
                Data Source=localhost,1433;
                Initial Catalog=mydata;
                User ID=SA;Password=Password123";
        
        // Phương thức OnConfiguring gọi mỗi khi một đối tượng DbContext được tạo
        // Nạp chồng nó để thiết lập các cấu hình, như thiết lập chuỗi kết nối
        protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseLoggerFactory(loggerFactory);  
      
        }


        

    }
}