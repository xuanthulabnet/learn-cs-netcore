using System;

namespace car_horn_with_di {
    public class Horn {
        public void Beep () => Console.WriteLine ("Beep - beep - beep ...");
    }

    public class Car {
        // horn là một Dependecy của Car
        Horn horn;
        // horn trong có được qua hàm tạo, ta nói
        // horn Inject (bơm vào) bằng hàm khởi tạo                                      
        public Car(Horn horn) => this.horn = horn;      

        public void Beep () {
            // Sử dụng Dependecy đã được Inject
            horn.Beep ();
        }
    }
}