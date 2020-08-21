using System;

namespace car_horn_nodi {
    public class Horn {
        public void Beep () => Console.WriteLine ("Beep - beep - beep ...");
    }

    public class Car {
        public void Beep () {
            // chức năng Beep xây dựng có định với Horn
            // tự tạo đối tượng horn (new) và dùng nó
            Horn horn = new Horn (); 
            horn.Beep ();
        }
    }
}