using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace ef02.Model
{
    public class ShopContext : DbContext
    {   
        protected string connect_str = @"Data Source=localhost,1433;
                                         Initial Catalog=shopdata;
                                         User ID=SA;Password=Password123";
        public DbSet<Product> products {set; get;}      // bảng Products
        public DbSet<Category> categories {set; get;}   // bảng Category

        protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            base.OnConfiguring(optionsBuilder);

           // Tạo ILoggerFactory 
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            optionsBuilder.UseSqlServer(connect_str)            // thiết lập làm việc với SqlServer
                          .UseLoggerFactory(loggerFactory)      // thiết lập logging
                          .UseLazyLoadingProxies() ;     
    
        }

        // Tạo database
        public async Task CreateDatabase()
        {
            String databasename = Database.GetDbConnection().Database;

            Console.WriteLine("Tạo " + databasename);
            bool result = await Database.EnsureCreatedAsync();
            string resultstring = result ? "tạo  thành  công" : "đã có trước đó";
            Console.WriteLine($"CSDL {databasename} : {resultstring}");
        } 

        // Xóa Database
        public async Task DeleteDatabase()
        {
            String databasename = Database.GetDbConnection().Database;
            // Console.Write($"Có chắc chắn xóa {databasename} (y) ? "); 
            // string input = Console.ReadLine();

            // // Hỏi lại cho chắc
            // //if (input.ToLower() == "y")
            {
                bool deleted = await Database.EnsureDeletedAsync(); 
                string deletionInfo = deleted ? "đã xóa" : "không xóa được"; 
                Console.WriteLine($"{databasename} {deletionInfo}");
            }
        }
 
  
        // Chèn dữ liệu mẫu
        public async Task InsertSampleData()  
        {
                // Thêm 2 danh mục vào Category
                var cate1 = new Category() {Name = "Cate1", Description = "Description1"};
                var cate2 = new Category() {Name = "Cate2", Description = "Description2"};
                await AddRangeAsync(cate1, cate2);
                await SaveChangesAsync();

                // Thêm 5 sản phẩm vào Products                       
                await  AddRangeAsync(
                    new Product()  {Name = "Sản phẩm 1",    Price=12, Category = cate2},
                    new Product()  {Name = "Sản phẩm 2",    Price=11, Category = cate2},
                    new Product()  {Name = "Sản phẩm 3",    Price=33, Category = cate2},
                    new Product()  {Name = "Sản phẩm 4(1)", Price=323, Category = cate1},
                    new Product()  {Name = "Sản phẩm 5(1)", Price=333, Category = cate1}

                );             
                await SaveChangesAsync(); 

                // Các sản phầm chèn vào
                foreach (var item in products)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append($"ID: {item.ProductId}");
                    stringBuilder.Append($"tên: {item.Name}");
                    stringBuilder.Append($"Danh mục {item.CategoryId}({item.Category.Name})");
                    Console.WriteLine(stringBuilder);

                }
                
        }

        // Truy vấn lấy về sản phẩm theo ID
        public async Task<Product> FindProduct(int id) {

                var p =  await (from c  in products where c.ProductId == id select c).FirstOrDefaultAsync();
                await  Entry(p)                   // lấy DbEntityEntry liên quan đến p
                       .Reference(x => x.Category) // lấy tham chiếu, liên quan đến thuộc tính Category
                       .LoadAsync();               // nạp thuộc tính từ DB
                return  p;
        }
        // Truy vấn lấy về Category theo ID
        public async Task<Category> FindCategory(int id) {

                var cate =  await (from c  in categories where c.CategoryId == id select c).FirstOrDefaultAsync();
                await  Entry(cate)                     // lấy DbEntityEntry liên quan đến p
                       .Collection(cc => cc.products)  // lấy thuộc tính tập hợp, danh sách các sản phẩm
                       .LoadAsync();                   // nạp thuộc tính từ DB
                return  cate;
        }


        // override protected void OnModelCreating(ModelBuilder modelBuilder) {
        //     base.OnModelCreating(modelBuilder);

        //     modelBuilder.Entity<Product>(entity => {
               

        //     });

            
            
        //     // Các Fluent API

        //     // modelBuilder.Entity<Product>()
        //     // .HasOne(b => b.Category)
        //     // .WithMany(b => b.Products)
        //     // .OnDelete(DeleteBehavior.Cascade);

        //     // modelBuilder.Entity<Product>().HasIndex(p => p.Name);

        // }


    }
}