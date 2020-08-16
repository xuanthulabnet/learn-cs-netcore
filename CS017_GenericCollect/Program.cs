using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
namespace CS017_GenericCollect {

    class Program {

        static void Main (string[] args) {
            /*  Ví dụ Generic Method */
            // TestMethoGeneric.TestSwap ();

            /* Ví dụ Generic Class */
            // MyClass<double> myClass = new MyClass<double>(123.123);
            // myClass.TestMethod(123);

            /* Ví dụ khởi tạo List */
            // var numbers  = new List<int>();           // danh sách số nguyên
            // var products = new List<Product>();       // danh sách Product

            /* Khởi tạo List với một số phần tử chèn vào */
            // var numbers  = new List<int>() {1,2,3,4};     // khởi tạo 4 phần tử
            // var products = new List<Product>()            // khởi tạo 1 phần tử
            // {
            //     new Product(1, "Iphone 6", 100, "Trung Quốc")
            // };

            /* Ví dụ thêm vào cuối, chèn phần tử*/
            var products = new List<Product> () {
                new Product (1, "Iphone 6", 100, "Trung Quốc")
            };
            var p = new Product (2, "IPhone 7", 200, "Trung Quốc");
            products.Add (p); // Thêm p vào cuối List
            products.Add (new Product (3, "IPhone 8", 400, "Trung Quốc")); // thêm đối tượng mới vào cuối List
            var arrayProducts = new Product[] // Mảng 2 phần tử
            {
                new Product (4, "Glaxy 7", 500, "Việt Nam"),
                new Product (5, "Glaxy 8", 700, "Việt Nam"),
            };
            products.AddRange (arrayProducts); // Nối các phần tử của mảng vào danh sách

            products.Insert (3, new Product (6, "Macbook Pro", 1000, "Mỹ")); // chèn phần tử vào vị trí index 3, (thứ 4)

            var pro = products[2]; // đọc phần tử có index = 2
            Console.WriteLine (pro.ToString ());

            /* Ví dụ duyệt qua phần tử */
            // Duyệt qua tất cả các phần tử bằng for
            // products.Count = lấy tổng phần tử trong List
            for (int i = 1; i < products.Count; i++) {
                var pi = products[i - 1];
                Console.WriteLine (pi.ToString ());
            }

            // Duyệt qua các phần tử bằng foreach
            foreach (var pi in products) {
                Console.WriteLine (pi.ToString ());
            }

            /* Ví dụ xóa phần tủ */
            products.RemoveAt (0); // xóa phần tử đầu tien
            products.RemoveRange (products.Count - 2, 2); // xóa 2 phần tử cuối
            var pro_rm = products[1];
            products.Remove (pro_rm); // xóa phần tử pro_rm


            /* Tìm phần tử Product với thuộc tính Name bằng Glaxy 8 */
            Product foundpr1 = products.Find (
                (Product ob) => { return (ob.Name == "Glaxy 8"); }
            );
            if (foundpr1 != null) 
                Console.WriteLine ("(found) " + foundpr1.ToString ("O"));
            
            /* tìm các sản phẩm có giá trên 100 */
            List<Product> p_100 = products.FindAll(product => product.Price > 100);
            Console.WriteLine("Các sản phẩm giá > 100");
            foreach (var pi in p_100) {
                Console.WriteLine (pi.ToString ());
            }

            /* Tìm kiếm */
            Product pr1 = products.Find( (new SearchNameProduct("Glaxy 8")).search);        // Tìm sản phẩm có tên Glaxy 8
            Product pr2 = products.Find( (new SearchNameProduct("IPhone 6")).search);       // Tìm sản phẩm có tên IPhone 6
            /* Sắp xếp */
            products.Sort(
                (p1, p2) => {
                    if (p1.ID > p2.ID)
                        return 1;
                    else if (p1.ID == p2.ID)
                        return  0;
                    return   -1;
                }
        );
        foreach (var pi in products)
        {
            Console.WriteLine(pi.ToString("N"));
        }


        }
    }
}