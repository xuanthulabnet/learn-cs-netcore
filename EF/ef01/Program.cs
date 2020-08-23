using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace ef01
{
    class Program
    {
        // Tạo Database mydata (tên mydata từ thông tin kết nối)
        // Gồm tất cả các bảng định nghĩa bởi các thuộc tính kiểu DbSet
        public static async Task CreateDatabase() {
            using (var dbcontext = new ProductsContext()) 
            {
                // mydata
                String databasename = dbcontext.Database.GetDbConnection().Database;

                Console.WriteLine("Tạo " + databasename);

                bool result = await dbcontext.Database.EnsureCreatedAsync();
                string resultstring = result ? "tạo  thành  công" : "đã có trước đó";
                Console.WriteLine($"CSDL {databasename} : {resultstring}");
            }
        } 
        

        // Xóa CSDL (không phục hồi được)
        public static async Task DeleteDatabase() 
        {

            using (var context = new ProductsContext()) 
            {
                String databasename = context.Database.GetDbConnection().Database;
                Console.Write($"Có chắc chắn xóa {databasename} (y) ? "); 
                string input = Console.ReadLine();

                // Hỏi lại cho chắc
                if (input.ToLower() == "y")
                {
                    bool deleted = await context.Database.EnsureDeletedAsync(); 
                    string deletionInfo = deleted ? "đã xóa" : "không xóa được"; 
                    Console.WriteLine($"{databasename} {deletionInfo}");
                }
            } 
            
        }

        // Thực hiện chèn hai dòng dữ liệu vào bảng Product
        // Dùng AddAsync trong DbSet và trong DbContext
        public static async Task InsertProduct() 
        {
            using (var context = new ProductsContext()) 
            {
                // Thêm sản phẩm 1
                await  context.products.AddAsync(new Product 
                {
                    Name = "Sản phẩm 1",
                    Provider = "Công ty 1"
                });        
                // Thêm sản phẩm 2
                await  context.AddAsync(new Product() 
                {
                    Name = "Sản phẩm 2",
                    Provider = "Công ty 1"
                });

                // Thực hiện cập nhật thay đổi trong DbContext lên Server
                int rows = await context.SaveChangesAsync();
                Console.WriteLine($"Đã lưu {rows} sản phẩm");

            } 
        }

        // Thực hiện chèn mảng các Product
        public static async Task InsertProductRange() 
        {
            using (var context = new ProductsContext()) 
            {
                var p1 = new  Product() {Name = "Sản phẩm 3", Provider = "CTY A"};
                var p2 = new  Product() {Name = "Sản phẩm 4", Provider = "CTY A"};
                var p3 = new  Product() {Name = "Sản phẩm 5", Provider = "CTY B"};

                await context.AddRangeAsync(new object[] {p1, p2, p3});

                int rows = await context.SaveChangesAsync();
                Console.WriteLine($"Đã lưu {rows} sản phẩm");
            } 
        }


        // Truy vấn lấy dữ liệu
       public static async Task ReadProducts() 
        {
            using (var context = new ProductsContext()) 
            {
                // Lấy danh sách các sản phẩm trong bảng 
                var products = await context.products.ToListAsync();

                Console.WriteLine("Tất cả sản phẩm");
                foreach (var product in products)
                {   
                    Console.WriteLine($"{product.ProductId,2} {product.Name,  10} - {product.Provider}");
                }
                Console.WriteLine();
                Console.WriteLine();

                // Dùng LINQ để truy vấn đến DbSet products (bảng product)
                // Lấy các sản phẩm cung cấp bởi CTY A
                products = await (from p in context.products
                                  where (p.Provider == "CTY A") select p
                                 )
                                .ToListAsync();

                // // Nếu không dùng bất đồng bộ chỗ này, có thể dùng    
                // var pros = from p in context.products
                //            where (p.Provider == "CTY A") select p;

                Console.WriteLine("Sản phẩm CTY A");
                foreach (var product in products)
                {
                    
                     
                    Console.WriteLine($"{product.ProductId,2} {product.Name,  10} - {product.Provider}");
                }
            } 
        }
        
        
       public static async Task RenameProduct(int id, string newName) 
        {
            using (var context = new ProductsContext()) 
            {
               // Lấy  Product có  ID  chỉ  ra  
                var product = await (from p in context.products
                                  where (p.ProductId == id) select p)
                                  .FirstOrDefaultAsync(); 

                // Cập nhật tên mới
                if (product != null)
                {
                    product.Name = newName;
                    Console.WriteLine($"{product.ProductId,2} có tên mới = {product.Name,  10}");
                    await context.SaveChangesAsync();
                }
                
                
            
                /* 
                // Cập nhật độc lập
                // Tạo đối tượng 
                var pr = new Product() {
                                ProductId  = 4,
                                Name = "Abc"
                          };
                // Gắn pr vào context để theo dõi, nó trả vể đối tượng 
                EntityEntry<Product> pr_e = context.Attach(pr);
                
                // Lấy thuộc tính Name của Product và thiết lập nó cần cập nhật
                // với IsModified  = true;
                pr_e.Property(p =>  p.Name).IsModified  = true;
                context.SaveChanges();
               */ 

            } 
        }
        // Xóa sản phẩm có ProductID = id        
        public static async Task DeleteProduct(int id) 
        {
            using (var context = new ProductsContext()) 
            {
                 
                var product = await (from p in context.products
                                  where (p.ProductId == id) select p
                                 )
                                .FirstOrDefaultAsync(); // Lấy  Product có  ID  chỉ  ra

                if (product != null)
                {
                    context.Remove(product);
                    Console.WriteLine($"Xóa {product.ProductId}");
                    await context.SaveChangesAsync();
                } 
            } 
        }       

        static async Task  Main(string[] args)
        {
            await CreateDatabase();
            await DeleteDatabase();
            await InsertProduct();
            await InsertProductRange();
            await ReadProducts();
            await RenameProduct(3, "XYZ");
            await DeleteProduct(3); 
        

        }
    }
}
