using System.Collections.Generic;
using System.Collections;

  public class LaptopName : IListProductName
  {
      public LaptopName() => System.Console.WriteLine("LaptopName Created");
      // Mảng tên sản phẩm
      string[] laptops = new string[]  {
          "Apple MacBook Pro 13 inch", "HP Spectre X360", "Samsung Chromebook Pro"};

      public IEnumerable<string> GetNames()
      {
          return laptops;
      }
  }
