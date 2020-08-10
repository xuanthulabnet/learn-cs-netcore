using System;

namespace CS007A_REF_VALUE
{

    class Stutent 
    {
        public string Name;
        public Stutent(string name)
        {
            this.Name = name;
        }
    }




    class Program
    {
        public static void valuetypeExample()
        {
            // biến a, kiểu int là kiểu giá trị, nó lưu giá trị 100
            int a = 100;
            // kiểu giá trị khi gán, giá trị được copy
            int b = a;
            Console.WriteLine($"a = {a}, b = {b}");

            // b gán giá trị mới, a không thay đổi - vì chúng không cùng tham
            // chiếu đến 1 đối tượng chung, mà mỗi biến lưu một giá trị
            b = 200;
            Console.WriteLine($"a = {a}, b = {b}");
            // a = 100, b = 100
            // a = 100, b = 200
        }

        // Tham số kiểu lớp, nên là tham chiếu
        static void UpperName(Stutent stutent)
        {
            stutent.Name = stutent.Name.ToUpper();
        }

        public static void ThamSoThamChieu(ref int x) {
            x = x * x;
            Console.WriteLine(x);
        }

        static void Main(string[] args)
        {
            Stutent a = new Stutent("Nguyễn Văn A");
            Console.WriteLine(a.Name); // Nguyễn Văn A

            Stutent b;
            // Khi gán, tham chiếu không sao chép giá trị mà tham chiếu địa chỉ
            // nên có thể có a là b, b là a, b thay đổi nghĩa là a thay đổi
            b = a;

            b.Name = "Nguyễn Văn B";
            Console.WriteLine(a.Name); // Nguyễn Văn B

            Stutent stutent1 = new Stutent("Xuan Thu Lab");
            UpperName(stutent1); // Phương thức biến đổi tham số student, có nghĩa là
                                 // biến đổi biến student1

            Console.WriteLine(stutent1.Name);  // XUAN THU LAB

        }


    }
}
