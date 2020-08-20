using System;

namespace struct_and_enum {

    enum HocLuc { Kem, TrungBinh = 5, Kha, Gioi }

    class Program {
        static void test_enum () {
            HocLuc hocluc = HocLuc.Kha; // khai báo biến hocluc kiểu enum và khởi tạo giá trị bằng HocLuc.Kha
            switch (hocluc) {
                case HocLuc.Kem:
                    Console.WriteLine ("Học lực kém");
                    break;
                case HocLuc.Kha:
                    Console.WriteLine ("Học lực Kha");
                    break;
                case HocLuc.Gioi:
                    Console.WriteLine ("Học lực Giỏi");
                    break;
                default:
                    Console.WriteLine ("Học lực TB");
                    break;

            }
        }
        static void Main (string[] args) {

            Product product = new Product ("Samsung Abc");
            Console.WriteLine (product.ToString ());

            test_enum();

        }
    }
}