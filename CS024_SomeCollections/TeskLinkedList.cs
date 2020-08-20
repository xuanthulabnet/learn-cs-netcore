using System;
using System.Collections;
using System.Collections.Generic;
namespace CS024_SomeCollections {
  class TeskLinkedList {

      public static void test() {
        
          LinkedList<string> cacbaihoc = new  LinkedList<string>();


          cacbaihoc.AddFirst("Bài học 3");   // thêm vào đầu danh sach
          cacbaihoc.AddLast("Bài học 4");    // thêm vào cuối
          cacbaihoc.AddFirst("Bài học 2");
          cacbaihoc.AddFirst("Bài học 1");


          // Lấy phần tử đầu tiên, sau đó duyệt đến cuối
          Console.WriteLine("---------Nút từ đầu về cuối");
          LinkedListNode<string> node = cacbaihoc.First;
          while (node != null) {
              Console.WriteLine(node.Value);
              node = node.Next;   // node gán bằng nút sau nó
          }
          // Bài học 1
          // Bài học 2
          // Bài học 3
          // Bài học 4

          // Lấy phần tử cuối cùng, sau đó duyệt về phần tử đầu  tiên
          Console.WriteLine("--------Nút từ cuối đến đầu");
          node = cacbaihoc.Last;
          while (node != null) {
              Console.WriteLine(node.Value);
              node = node.Previous;   // node gán bằng nút phía trước nó
          }
          // Bài học 4
          // Bài học 3
          // Bài học 2
          // Bài học 1

      }
  }
}