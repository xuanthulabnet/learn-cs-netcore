using System;
using System.Threading.Tasks;
using ef03.Model;

namespace ef03
{
    class Program
    {
         
        static async Task Main(string[] args)
        {
            ShopContext context = new ShopContext();

            await context.DeleteDatabase();  // xóa database: shopdata nếu tồn tại
            await context.CreateDatabase();  // tạo lại database: shopdata

            context.Add(new User {UserName = "xuanthulab"});

            // context.Add(new Category {Name = "Điện tử"});
            // context.Add(new Product { Name = "Iphone", CategoryId = 1, UserPostId = 1});
            context.SaveChanges();
            
            

        }
    }
}
