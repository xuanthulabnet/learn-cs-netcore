using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using mvcblog.Models;

namespace mvcblog.Data {

    public class AppDbContext : IdentityDbContext<AppUser> {

        public DbSet<Category> Categories {set; get;}
        public DbSet<Post> Posts {set; get;}
        public DbSet<PostCategory> PostCategories {set; get;}


        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder builder) {

            base.OnModelCreating (builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in builder.Model.GetEntityTypes ()) {
                var tableName = entityType.GetTableName ();
                if (tableName.StartsWith ("AspNet")) {
                    entityType.SetTableName (tableName.Substring (6));
                }
            }

            // Tạo Index cho cột Slug bảng Category
            builder.Entity<Category>(entity => {
                entity.HasIndex(p => p.Slug);
            });


            // Tạo key của bảng là sự kết hợp PostID, CategoryID, qua đó
            // tạo quan hệ many to many giữa Post và Category
            builder.Entity<PostCategory>().HasKey(p => new {p.PostID, p.CategoryID});


            // builder.Entity<PostCategory>().HasNoKey();


        }

    }

}