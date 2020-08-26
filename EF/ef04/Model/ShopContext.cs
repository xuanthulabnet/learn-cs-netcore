using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ef03.Model {
    public class ShopContext : DbContext {
        protected string connect_str = @"Data Source=localhost,1433;
                                         Initial Catalog=shopdata;
                                         User ID=SA;Password=Password123";
        public DbSet<Product> products { set; get; } // bảng Products
        public DbSet<Category> categories { set; get; } // bảng Category

        public DbSet<User> users {set; get;} // bảng User

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring (optionsBuilder);

            // Tạo ILoggerFactory 
            ILoggerFactory loggerFactory = LoggerFactory.Create (builder => builder.AddConsole ());

            optionsBuilder.UseSqlServer(connect_str) // thiết lập làm việc với SqlServer
                .UseLoggerFactory (loggerFactory); // thiết lập logging

            // optionsBuilder.UseLazyLoadingProxies ();

        }

        // Tạo database
        public async Task CreateDatabase () {
            String databasename = Database.GetDbConnection ().Database;

            Console.WriteLine ("Tạo " + databasename);
            bool result = await Database.EnsureCreatedAsync ();
            string resultstring = result ? "tạo  thành  công" : "đã có trước đó";
            Console.WriteLine ($"CSDL {databasename} : {resultstring}");
        }

        // Xóa Database
        public async Task DeleteDatabase () {
            String databasename = Database.GetDbConnection ().Database;
            // Console.Write($"Có chắc chắn xóa {databasename} (y) ? "); 
            // string input = Console.ReadLine();

            // // Hỏi lại cho chắc
            // //if (input.ToLower() == "y")
            {
                bool deleted = await Database.EnsureDeletedAsync ();
                string deletionInfo = deleted ? "đã xóa" : "không xóa được";
                Console.WriteLine ($"{databasename} {deletionInfo}");
            }
        }

        // Chèn dữ liệu mẫu
        public async Task InsertSampleData () {
            // Thêm User
            var user1 = new User() { UserName = "XTL1" };
            var user2 = new User() { UserName = "XTL2" };
            await AddRangeAsync(user1, user2);
            

            // Thêm 2 danh mục vào Category
            var cate1 = new Category () { Name = "Cate1", Description = "Description1" };
            var cate2 = new Category () { Name = "Cate2", Description = "Description2" };
            await AddRangeAsync (cate1, cate2);
            await SaveChangesAsync ();

            // Thêm 5 sản phẩm vào Products                       
            await AddRangeAsync (
                new Product () { Name = "Sản phẩm 1", Price = 12, Category = cate2, UserPost = user1 },
                new Product () { Name = "Sản phẩm 2", Price = 11, Category = cate2, UserPost = user1, SecondCategory = cate2 },
                new Product () { Name = "Sản phẩm 3", Price = 33, Category = cate2, UserPost = user1 },
                new Product () { Name = "Sản phẩm 4(1)", Price = 323, Category = cate1, UserPost = user2 },
                new Product () { Name = "Sản phẩm 5(1)", Price = 333, Category = cate1, UserPost = user2, SecondCategory = cate2 }

            );
            await SaveChangesAsync ();

            // Các sản phầm chèn vào
            foreach (var item in products) {
                StringBuilder stringBuilder = new StringBuilder ();
                stringBuilder.Append ($"ID: {item.ProductId}");
                stringBuilder.Append ($"tên: {item.Name}");
                stringBuilder.Append ($"Danh mục {item.CategoryId}({item.Category.Name})");
                Console.WriteLine (stringBuilder);

            }

        }

        // Truy vấn lấy về sản phẩm theo ID
        public async Task<Product> FindProduct (int id) {

            var p = await (from c in products where c.ProductId == id select c).FirstOrDefaultAsync ();
            await Entry (p) // lấy DbEntityEntry liên quan đến p
                .Reference (x => x.Category) // lấy tham chiếu, liên quan đến thuộc tính Category
                .LoadAsync (); // nạp thuộc tính từ DB
            return p;
        }
        // Truy vấn lấy về Category theo ID
        public async Task<Category> FindCategory (int id) {

            var cate = await (from c in categories where c.CategoryId == id select c).FirstOrDefaultAsync ();
            await Entry (cate) // lấy DbEntityEntry liên quan đến p
                .Collection (cc => cc.products) // lấy thuộc tính tập hợp, danh sách các sản phẩm
                .LoadAsync (); // nạp thuộc tính từ DB
            return cate;
        }

        // Phương thức này thi hành khi EnsureCreatedAsync chạy, tại đây gọi các Fluent API mong muốn
        override protected void OnModelCreating (ModelBuilder modelBuilder) {

            base.OnModelCreating (modelBuilder);

            modelBuilder.Entity<User>(entity => {


                // Thiết lập cho bảng
                entity
                    .ToTable("User")                        // Tùy chọn tên của bảng là User (mặc định user)
                    .HasComment("Đây là bảng người dùng")   // Dòng chú thichs
                    .HasKey(e => e.UserId);                 // Thiết lập Primary key là UserId

                // Thiết lập cho cột UserID    
                entity.Property(e => e.UserId)
                    .UseIdentityColumn(1,1);  

                // Thiết lập cho cột UserName    
                entity.Property(e => e.UserName)
                      .HasColumnName("user_name")    //Tùy chọn đặt lại tên cột user_name (mặc định UserName)
                    //.HasColumnType("varchar(20)")  
                      .HasDefaultValue("Không tên")  // Giá trị mặc định
                      .HasMaxLength(20);             // Độ dài của trường dữ liệu 20 
                entity.HasIndex(p => p.UserName)     // Đánh chỉ mục UserName (user_name)
                      .IsUnique(true);               // Unique

            });

            // Bảng Products
            
            modelBuilder.Entity<Product>(entity => {
                // Thiết lập cho bảng Product
                entity.HasOne(e => e.UserPost)                       // Chỉ ra Entity là phía một (bảng User)
                        .WithMany(user => user.ProductsPost)         // Chỉ ra Collection tập Product lưu ở phía một
                        .HasForeignKey("UserPostId")                 // Chỉ ra tên FK nếu muốn
                        .OnDelete(DeleteBehavior.SetNull)            // Ứng xử khi User bị xóa (Hoặc chọn DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Products_user_1234"); // Tự đặt tên Constrain (dàng buốc)
                
            });

        

        }

    }
}