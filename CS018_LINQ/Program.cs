using System;
using System.Collections.Generic;
using System.Linq;

namespace CS018_LINQ
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Products.ProductPrice500();
            /* 

            var products =  Product.products;
            var brands   =  Brand.brands; 


            // truy vấn cơ bản
            var ketqua = from product in products
                where product.Price == 400
                select product;
            Console.WriteLine("Các sản phẩm giá 400:");
            foreach (var product in ketqua)
                Console.WriteLine(product.ToString());
            Console.WriteLine();    
      
               

            // tạo đối tượng vô danh kết quả trả về
            var ketqua1 = from product in products
                        where product.Price == 400 
                        select new {
                            ten = product.Name,
                            mausac = string.Join(',', product.Colors)
                        };
            Console.WriteLine("Tên, màu sắc sản phẩm có giá 400");
            foreach (var item in ketqua1) Console.WriteLine(item.ten + " - " + item.mausac);
            Console.WriteLine();    
 


            // lọc dữ liệu bằng where
            var ketqua2 = from product in products
                where product.Price >= 500
                where product.Name.StartsWith("Giường")
                select product;
            Console.WriteLine("Sản phẩm có tên bắt đầu là Giường, giá trên 500");
            foreach (var product in ketqua2)
                Console.WriteLine(product.ToString());
            Console.WriteLine();    
      

            //sử dụng from kết hợp để lọc
            var ketqua3 = from product in products
                          from color in product.Colors
                            where product.Price < 500
                            where color == "Vàng"
                            select product;
            Console.WriteLine("Sản phẩm có màu Vàng, giá dưới 500");
            foreach (var product in ketqua3) Console.WriteLine(product.ToString());
            Console.WriteLine();    


            // Sắp xếp bằng orderby
            var ketqua4 = from product in products
                        where product.Price <= 300
                        orderby product.Price descending
                        select product;

            Console.WriteLine("Sản phẩm giá nhỏ hơn bằng 300, sắp xếp theo giá giảm dần");
            foreach (var product in ketqua4) Console.WriteLine($"{product.Name} - {product.Price}");
            Console.WriteLine(); 

            // Nhóm kết quả bằng group
            var ketqua5 = from product in products
            where product.Price >=400 && product.Price <= 500
            group product by product.Price;
            
            Console.WriteLine("Các sản phẩm nhóm theo giá 400, 500");
            foreach (var group in ketqua5)
            {
                Console.WriteLine(group.Key);
                foreach (var product in group)
                {
                    Console.WriteLine($"    {product.Name} - {product.Price}");
                }

            }
            Console.WriteLine(); 

            // dùng biến trong truy vấn
            var ketqua6 = from product in products
            group product by product.Price into gr
            let count = gr.Count()
            select new {
                price = gr.Key,
                number_product = count
            };
            Console.WriteLine("Số sản phẩm theo giá");
            foreach (var item in ketqua6)
            {
                Console.WriteLine($"   Giá {item.price} - có {item.number_product} sp");
            } 
            Console.WriteLine();


            // inner join
            var ketqua7 = from product in products
                        join brand in brands on product.Brand equals brand.ID
                        select new {
                            name  = product.Name,
                            brand = brand.Name,
                            price = product.Price
                        };
            
            Console.WriteLine("Sản phẩm - giá - tên hãng");
            foreach (var item in ketqua7)
            {
                Console.WriteLine($"{item.name,10} {item.price, 4} {item.brand,12}");
            }
            Console.WriteLine();


            // left join
            var ketqua8 = from product in products
                        join brand in brands on product.Brand equals brand.ID into t
                        from brand in t.DefaultIfEmpty()
                        select new {
                            name  = product.Name,
                            brand = (brand == null) ? "NO-BRAND" : brand.Name,
                            price = product.Price
                        };
            Console.WriteLine("Sản phẩm - giá - tên hãng");
            foreach (var item in ketqua8)
            {
                Console.WriteLine($"{item.name,10} {item.price, 4} {item.brand,12}");
            }
            */
        }
    }
}
