using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ef05.Models
{
    public partial class shopdataContext : DbContext
    {
        public shopdataContext()
        {
        }

        public shopdataContext(DbContextOptions<shopdataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=shopdata;User ID=SA;Password=Password123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.CategorySecondId);

                entity.HasIndex(e => e.UserPostId);

                entity.HasOne(d => d.UserPost)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UserPostId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Products_user_1234");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasComment("Đây là bảng người dùng");

                entity.HasIndex(e => e.UserName)
                    .IsUnique()
                    .HasFilter("([user_name] IS NOT NULL)");

                entity.Property(e => e.UserName).HasDefaultValueSql("(N'Không tên')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
