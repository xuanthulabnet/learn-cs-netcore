using System;
using System.Collections.Generic;
using System.Linq;

namespace razor04.codebehide.Models {
  // Lớp Product
  public class Product {
    public int ID {set; get;}
    public String Name {set; get;}
    public String Desciption {set; get;}
    public Decimal Price {set; get;} = 0;
  }

  // Lớp tĩnh giả định DbContext
  public static class ProductContext {
    public static List<Product> products;
    static ProductContext() {
      // Khởi tạo một danh sách các sản phẩm mẫu
      products = new List<Product>() {
        new Product {
          ID=1,
          Name = "Iphone",
          Price = 900,
          Desciption = "Điện thoại Iphone abc, xyz ..."
        },
        new Product {
          ID = 2,
          Name = "Samsung",
          Price = 800,
          Desciption = "Điện thoại Samsung, samsung điện thoại ..."
        },
        new Product {
          ID = 3,
          Name = "Nokia",
          Price = 700,
          Desciption = "Điện thoại Nokia, điện thoại Android"
        }
      };
    }

    // Tìm sản phẩm theo ID
    public static Product FindProductByID(int ID) {
      var p = from product in products 
              where product.ID == ID
              select product; 
      return p.FirstOrDefault();        
    }

  }

}