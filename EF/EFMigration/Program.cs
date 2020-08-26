using System;
using System.Threading.Tasks;
using EFMigration.Models;
using Microsoft.EntityFrameworkCore;

namespace EFMigration
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var webcontext =  new WebContext())
            {
                // Thực hiện Migrate - tạo db đúng cấu trúc Migration cuối cùng nếu chưa có
                // Nếu DB đã có từ các Migration trước, sẽ cập nhật
                await webcontext.Database.MigrateAsync();
            }
        }
    }
}
