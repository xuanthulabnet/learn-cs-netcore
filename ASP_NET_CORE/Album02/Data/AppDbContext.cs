using Album.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Album.Data
{
  public class AppDbContext : IdentityDbContext<AppUser>
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
     protected override void OnModelCreating(ModelBuilder builder) {
          base.OnModelCreating(builder);
          // Bỏ tiề tố AspNet của các bảng
          foreach (var entityType in builder.Model.GetEntityTypes())
          {
              var tableName = entityType.GetTableName();
              if (tableName.StartsWith("AspNet"))
              {
                  entityType.SetTableName(tableName.Substring(6));
              }
          }
     }

  }
}
// dotnet ef migrations add Init
// dotnet ef database update
// dotnet ef database drop -f
