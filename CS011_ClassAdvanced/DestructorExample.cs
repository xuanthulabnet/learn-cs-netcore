using System;

namespace CS011_ClassAdvanced
{ 
    class Product {

        private string product_name;
        public Product(string name) 
        {
            this.product_name = name;
            Console.WriteLine("Tạo - " + product_name);
        }
        ~Product() {
            Console.WriteLine("Hủy - " + product_name);
        }
    }
 
}
