using System;

namespace CS023_Dispose {
    class A : IDisposable {
        bool resource = true;
        public void Dispose () {
            Console.WriteLine ("Phương thức này gọi tự động khi hết using");
            resource = false; // giải phóng tài nguyên
        }
    }




    class Program {
        static void Main (string[] args) {

            // using (var a = new A ()) {
            //     Console.WriteLine ("Do something ...");
            // }

        }
    }
}