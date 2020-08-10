using System;

namespace CS007_Class {

    class Student {
        private string name;

        public string Name {
            // set thi hành khi gán, write
            // dữ liệu gán là value
            set {
                Console.WriteLine ("Ghi dữ liệu <--" + value);
                name = value;
            }

            //get thi hành ghi đọc dữ liệu
            get {
                return "Tên là: " + name;
            }
        }
    }

    class Program {

        static void Main (string[] args) {

            // var sungluc = new VuKhi();              // Khai báo và khởi tạo
            // sungluc.name = "SÚNG LỤC";              // Truy cập và gán thuộc tính
            // sungluc.SetDoSatThuong(5);              // Truy cập (gọi) phương thức

            // VuKhi sungtruong = new VuKhi();
            // sungtruong.name = "SÚNG TRƯỜNG";
            // sungtruong.SetDoSatThuong(20);

            // sungluc.TanCong();                      // Gọi phương thức
            // sungtruong.TanCong();                   // Gọi phương thức
            
 


            // var s = new Student ();
            // s.Name = "XYZ";
            // Console.WriteLine (s.Name);



            // // Khởi tạo đối tượng, hàm tạo VuKhi() được gọi
            // var sungluc = new VuKhi();
            // sungluc.name = "SÚNG LỤC";
            // sungluc.SetDoSatThuong(5);

            // // Khởi tạo đối tượng, hàm tạo VuKhi(name, dosatthuong) được gọi
            // VuKhi sungtruong = new VuKhi(name: "SÚNG TRƯỜNG", dosatthuong: 20);

            // sungluc.TanCong();
            // sungtruong.TanCong();

            
            // double a = 1;
            // double b = 2;
            // var c  = OverloadingExample.Sum(a, b); // c có kiểu double
        }
    }
}