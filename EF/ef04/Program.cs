using System;
using System.Linq;
using System.Threading.Tasks;
using ef03.Model;
using Microsoft.EntityFrameworkCore;

namespace ef03 {
    class Program {

        static async Task Main (string[] args) {
            ShopContext dbcontext = new ShopContext ();
            await dbcontext.DeleteDatabase (); // xóa database: shopdata nếu tồn tại
            await dbcontext.CreateDatabase (); // tạo lại database: shopdata
            await dbcontext.InsertSampleData (); // chèn dữ liệu mẫu
            dbcontext.Dispose ();

            Console.WriteLine ("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");

            // using (var context = new ShopContext ()) {
            //     var products = context.products;
            //     // var products = await context.products.ToListAsync (); nếu muốn dùng async
            //     foreach (var pro in products) {
            //         Console.WriteLine (pro.Name);
            //     }
            // }

            // using (var context = new ShopContext ()) {

            //     var products = context.products
            //                     .Where(p => p.Price > 100)              // Lọc các sản phẩm giá trên 100
            //                     .OrderByDescending(p => p.Price)        // Sắp xếp giảm dần, tăng dần là OrderBy
            //                     .Take(2);                               // Chỉ lấy 2 dòng đầu

            //     // var products = await context.products.ToListAsync (); nếu muốn dùng async
            //     foreach (var pro in products) {
            //         Console.WriteLine (pro.Price);
            //     }
            // }

            // using (var context = new ShopContext ()) {

            //     var products = context.products
            //                     .Where(p => p.Price > 100)              // Lọc các sản phẩm giá trên 100
            //                     .OrderByDescending(p => p.Price)        // Sắp xếp giảm dần, tăng dần là OrderBy
            //                     .Take(2).Sum(p => p.Price);  

            // } 

            using (var context = new ShopContext ()) {
                var products = from p in context.products
                join c in context.categories on p.CategorySecondId equals c.CategoryId into t
                from cate2 in t.DefaultIfEmpty ()
                // where p.ProductId == 2
                select new {
                tensanpham = p.Name,
                gia = p.Price,
                danhmuc = (cate2 == null) ? "KHÔNG CÓ" : cate2.Name
                };

                foreach (var item in products) {
                    Console.WriteLine ($"{item.tensanpham} giá {item.gia} danh mục {item.danhmuc}");
                }
            }

            using (var context = new ShopContext ()) {
                String sql = "select * from products order by Price Desc";
                var products = context.products.FromSqlRaw (sql);

                await products.ForEachAsync (p => {
                    Console.WriteLine (p.Name);
                });

            }

        }
    }
}