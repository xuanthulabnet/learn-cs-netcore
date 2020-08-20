using System;

namespace struct_and_enum {
    public struct Product {

        public Product(string _name)
        {
          name = _name;  // đồng nghĩa khởi tạo thuộc tính Name
          price = 100;  
          Description = "Mô tả về sản phẩm {name}";
        }
  
        public string name;   // trường tên sản phẩm
        public decimal price; // trường giá sản phẩm

        // Phương thức sinh ra chuỗi thông tin
        public override string ToString() => $"{name} : {price}$";

        // Thuộc tính Name
        public string Name {set => name = value; get => name;}
        // Thuộc tính Description
        public string Description {set; get;}
        
    }

}