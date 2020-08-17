using System;
using A.B;

namespace A {
    // Định nghĩa các lớp, cấu trúc ...
    namespace B {
        // Định nghĩa các lớp, cấu trúc ...
    }
}

namespace CS013_Inheritance {

    class Animal {
        protected int Legs { get; set; }
        public float Weight { get; set; }

        public void ShowLegs () {
            Console.WriteLine ($"Legs: {Legs}");
        }
    }

    class Cat : Animal {
        public string food; // thuộc tính mới thêm
        public Cat () {
            Legs = 4; // Thuộc tính Legs có sẵn - vì nó kế thừa từ Animal
            food = "Mouse";
        }

        public void Eat () {
            Console.WriteLine (food);
        }

    }

    class A {

    };
    class B : A {

    };

    class C : B {

    }
    class Program {

        static void Main (string[] args) {
            /* Ví dụ kế thừa */    
            Cat cat = new Cat ();
            cat.ShowLegs (); // Phương thức này kế thừa từ lớp cơ sở
            cat.Eat (); // phương thức của riêng Cat

            /* Ví dụ chuyển kiểu */
            C c = new C();
            A a = (A)c;       // chuyển kiểu tường minh
            a = c;            // ngầm định
            a = new C();      // ngầm định

            B b = c;          // ngầm định

            c = new A();      // lỗi - không thể chuyển kiểu thuận cây kế thừa -  Lớp cha không chuyển thành con được


        }

    }
}