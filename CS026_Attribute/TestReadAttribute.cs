using System;

namespace CS026_Attribute {
  class TestReadAttribute {
    public static void test () {
      var a = new User ();

      // Đọc các Attribute của lớp
      foreach (Attribute attr in a.GetType ().GetCustomAttributes (false)) {
        MotaAttribute mota = attr as MotaAttribute;
        if (mota != null) {
          Console.WriteLine ($"{a.GetType().Name,10} : {mota.Description}");
        }
      }

      // Đọc Attribute của từng thuộc tính lớp
      foreach (var thuoctinh in a.GetType ().GetProperties ()) {
        foreach (Attribute attr in thuoctinh.GetCustomAttributes (false)) {
          MotaAttribute mota = attr as MotaAttribute;
          if (mota != null) {
            Console.WriteLine ($"{thuoctinh.Name,10} : {mota.Description}");
          }
        }
      }

      // Đọc Attribute của phương thức
      foreach (var m in a.GetType ().GetMethods ()) {
        foreach (Attribute attr in m.GetCustomAttributes (false)) {
          MotaAttribute mota = attr as MotaAttribute;
          if (mota != null) {
            Console.WriteLine ($"{m.Name,10} : {mota.Description}");
          }
        }
      }



    }

  }
}