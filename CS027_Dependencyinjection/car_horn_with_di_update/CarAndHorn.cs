using System;

namespace car_horn_with_di_update {
    public class Horn {
        int level; // độ lớn còi xe
        public Horn (int level) => this.level = level; // thêm khởi tạo level

        public void Beep () => Console.WriteLine ("Beep - beep - beep ...");
    }

    public class Car {
        // horn là một Dependecy của Car
        Horn horn;
        // horn trong có được qua hàm tạo, ta nói
        // horn Inject (bơm vào) bằng hàm khởi tạo                                      
        public Car (Horn horn) => this.horn = horn;

        public void Beep () {
            // Sử dụng Dependecy đã được Inject
            horn.Beep ();
        }
    }
}