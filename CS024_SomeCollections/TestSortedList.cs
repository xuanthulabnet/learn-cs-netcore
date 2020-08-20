using System;
using System.Collections;
using System.Collections.Generic;
namespace CS024_SomeCollections {
  class TestSortedList {

    public static void test () {

      // Khởi tạo SortedList
      var products = new SortedList<string, string> ();
      products.Add ("Iphone 6", "P-IPHONE-6"); // Thêm vào phần tử mới (key, value)
      products.Add ("Laptop Abc", "P-LAP");
      products["Điện thoại Z"] = "P-DIENTHOAI"; // Thêm vào phần tử bằng Indexer
      products["Tai nghe XXX"] = "P-TAI";       // Thêm vào phần tử bằng Indexer

      
      // Duyệt qua các phần tử, mỗi phần tử lấy key/value lưu trong biến
      // kiểu KeyValuePair
      Console.WriteLine ("TÊN VÀ MÃ");
      foreach (KeyValuePair<string, string> p in products) {
        Console.WriteLine ($"    {p.Key} - {p.Value}");
      }
      // kết quả: (để ý danh sách đã xếp theo key)
      // TÊN và MÃ
      //     Điện thoại Z - P-DIENTHOAI
      //     Iphone 6 - P-IPHONE-6
      //     Laptop Abc - P-LAP
      //     Tai nghe XXX - P-TAI

      // Đọc value khi biết key
      string productName = "Tai nghe XXX";
      Console.WriteLine ($"{productName} có mã là {products[productName]}");

      // Cập nhật giá trị vào phần tử theo key
      products[productName] = "P-TAI-UPDATED"; 

      // Duyệt qua các giá trị
      Console.WriteLine ("\nDANH SÁCH MÃ SẢN PHẢM");
      foreach (var product_code in products.Values) {
          Console.WriteLine ($"--- {product_code}");
      }
      // kết quả:
      // DANH SÁCH MÃ SẢN PHẢM
      // -- P-DIENTHOAI
      // -- P-IPHONE-6
      // -- P-LAP
      // -- P-TAI-UPDATED

      // Duyệt qua các key
      Console.WriteLine ("\nDANH SÁCH TÊN SP");
      foreach (var product_name in products.Keys) {
        Console.WriteLine ($"... {product_name}");
      }
      // DANH SÁCH TÊN SP
      // -- Điện thoại Z
      // -- Iphone 6
      // -- Laptop Abc
      // -- Tai nghe XXX

    }
  }
}