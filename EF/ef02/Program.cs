using System;
using System.Threading.Tasks;
using ef02.Model;

namespace ef02
{
    class Program
    {
         
        static async Task Main(string[] args)
        {
            ShopContext context = new ShopContext();

            await context.DeleteDatabase();  // xóa database: shopdata nếu tồn tại
            await context.CreateDatabase();  // tạo lại database: shopdata
            await context.InsertSampleData();

            // Giải phóng và kết nối lại
            await context.DisposeAsync();
            context = new ShopContext();

            var p    = await context.FindProduct(2);
            var c    = p.Category; 
            if (p != null) 
            {
                Console.WriteLine($"{p.Name} có CategoryId = {p.CategoryId}");
                string CategoryName = (c != null) ? c.Name :  "Category đang null";
                Console.WriteLine(CategoryName);
            }

            var catef = await context.FindCategory(2);
            if (catef != null)
            {
                Console.WriteLine($"Các sản phẩm thuộc danh mục {catef.Name}");
                foreach (var item in catef.products)
                {
                    Console.WriteLine(item.Name);
                }
            }

        }
    }
}
