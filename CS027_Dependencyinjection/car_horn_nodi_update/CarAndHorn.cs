using System;

namespace car_horn_nodi_update {
    public class Horn {
        int level; // độ lớn của còi xe
        public Horn (int level) => this.level = level; // thêm khởi tạo level
        public void Beep () => Console.WriteLine ($"(level {level}) Beep - beep - beep ...");
    }

    public class Car {
        public void Beep () {
            // Tạo Horn phải cung cập level
            Horn horn = new Horn (10);
            horn.Beep ();
        }
    }
}