using System;
using System.Reflection;

namespace CS025_Type {
    public class A {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    class Program {
        static void Main (string[] args) {

            A a = new A {
                Name = "HOTEN",
                ID = 10
            };

            //Lấy tên và giá trị các thuộc tính có trong a
            foreach (PropertyInfo property in a.GetType().GetProperties())
            {
                string property_name = property.Name;         // Lấy tên thuộc tính      
                object property_value = property.GetValue(a); // Đọc giá trị thuộc tính đối tượng a
                Console.WriteLine($"Thuộc tính {property_name} giá trị là {property_value}");
            }
        }
    }
}
/*
Kết quả:
Thuộc tính ID giá trị là 10
Thuộc tính Name giá trị là HOTEN
*/