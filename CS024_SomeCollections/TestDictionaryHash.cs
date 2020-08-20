
using System;
using System.Collections;
using System.Collections.Generic;
namespace CS024_SomeCollections {
  class TestDictionaryHash {

      public static void testHash()
      {
          HashSet<int> hashset1 = new HashSet<int>() {
              5,2,3,4
          };

          Console.WriteLine($"Phần tử trong hashset1 {hashset1.Count}");
          foreach (var v in hashset1)
          {
              Console.Write(v + " ");
          }
          Console.WriteLine();

          HashSet<int>hashset2 = new HashSet<int>();
          hashset2.Add(3);
          hashset2.Add(4);
          if (hashset1.IsSupersetOf(hashset2))
              Console.WriteLine($"hashset1 là tập chứa hashset2");

      }

      public static void testDic()
      {
            // Khởi tạo với 2 phần tử
            Dictionary<string, int> sodem = new Dictionary<string, int>()
            {
              ["one"] = 1,
              ["tow"] = 2,
            };
            // Thêm hoặc cập nhật
            sodem["three"] = 3;


            var keys = sodem.Keys;
            foreach (var k in keys)
            {
                var value = sodem[k];
                Console.WriteLine($"{k} = {value}");
            }

      }
 
  }
}