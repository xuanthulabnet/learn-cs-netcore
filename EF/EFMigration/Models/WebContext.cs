using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EFMigration.Models {
  public class WebContext : DbContext {
    public DbSet<Article> articles { set; get; } // bảng article
    public DbSet<Tag> tags { set; get; }         // bảng tag

    // bảng ArticleTag thêm vào ở Migrate thứ 3
    public DbSet<ArticleTag> articleTags {set; get;}


    // chuỗi kết nối với tên db sẽ làm  việc đặt là webdb
    public const string ConnectStrring = @"Data Source=localhost,1433;
                                          Initial Catalog=webdb;
                                          User ID=SA;Password=Password123";

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlServer (ConnectStrring);
      optionsBuilder.UseLoggerFactory (GetLoggerFactory ()); // bật logger
    }

    private ILoggerFactory GetLoggerFactory () {
      IServiceCollection serviceCollection = new ServiceCollection ();
      serviceCollection.AddLogging (builder =>
        builder.AddConsole ()
        .AddFilter (DbLoggerCategory.Database.Command.Name,
          LogLevel.Information));
      return serviceCollection.BuildServiceProvider ()
        .GetService<ILoggerFactory> ();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticleTag>(entity => {
            // Tạo Index Unique trên 2 cột
            entity.HasIndex(p => new {p.ArticleId,  p.TagId})
                  .IsUnique();
        });
    }


  }

}