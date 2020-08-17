using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace CS018_LINQ
{
    public class Product
    {
        #region CÁC THUỘC TÍNH SẢN PHẨM
        public int ID {set; get;}
        public string Name {set; get;}         // tên
        public double Price {set; get;}        // giá
        public string[] Colors {set; get;}     // cá màu 
        public int Brand {set; get;}           // Nhãn hiệu, hãng
        #endregion

        public Product(int id, string name, double price,
            string[] colors, int brand) {
            ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
        }
        // Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
        override public string ToString() => $"ID {ID} - {Name}, giá {Price}"; 

        

    }

    public class Products
    {
        // thành viên biến tĩnh, là danh sách sản phẩm
        public static List<Product> products;

        // Hàm khởi tạo thành viên tĩnh
        static Products()
        {
            // Khởi tạo products với 7 sản phẩm mẫu
            products = new List<Product>()
            {
                new Product(1, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
                new Product(2, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
                new Product(3, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
                new Product(4, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
                new Product(5, "Đèn trùm",   500, new string[] {"Trắng"},               3),
                new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
                new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
            };
        }

        // In ra các sản phẩm có giá 400
        public static void ProductPrice500()
        {
            var products = Products.products;
            var ketqua = from product in products
                where product.Price == 400
                select product;

            foreach (var product in ketqua)
                Console.WriteLine(product.ToString());
            // In ra    
            // ID 3 - Bàn trà, giá 400
            // ID 4 - Tranh treo, giá 400
        }
    }
 
    public class  Brand {
        public string Name {set; get;}
        public int ID {set; get;}

        static List<Brand> _brands; 
        public static List<Brand> brands { 
            get {
                if (_brands == null) {
                    _brands = new List<Brand>() {
                        new Brand{ID = 1, Name = "Công ty AAA"},
                        new Brand{ID = 2, Name = "Công ty BBB"},
                        new Brand{ID = 4, Name = "Công ty CCC"},
                    };
                }
                return _brands;
            }
        } 
    }
}