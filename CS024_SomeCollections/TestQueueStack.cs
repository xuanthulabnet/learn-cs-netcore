using System;
using System.Collections;
using System.Collections.Generic;
namespace CS024_SomeCollections {
  class TestQueueStack {

    public static void testStack()
    {
        var nhakho = new Stack<string>();

        nhakho.Push("Sản phẩm A");
        nhakho.Push("Sản phẩm B");
        nhakho.Push("Sản phẩm C");
        
        // Xếp vào sau thì tháo ra trước
        while (nhakho.Count > 0) 
        {
            var sp = nhakho.Pop();
            Console.WriteLine($"Bốc dỡ {sp} / còn lại {nhakho.Count}");
        }

    }

    public static void testQueue () {

      Queue<string> hoso_canxuly = new Queue<string> ();

      hoso_canxuly.Enqueue ("Hồ sơ A"); // Hồ sơ xếp thứ nhất trong hàng đợi
      hoso_canxuly.Enqueue ("Hồ sơ B"); // Hồ sơ xếp thứ hai
      hoso_canxuly.Enqueue ("Hồ sơ C");


      // Lấy hồ sơ xếp trước xử lý  trước, cho đến hết
      while (hoso_canxuly.Count > 0) {
        var hs = hoso_canxuly.Dequeue();
        Console.WriteLine($"Xử lý {hs}, còn lại {hoso_canxuly.Count}");
      }

    }
  }
}