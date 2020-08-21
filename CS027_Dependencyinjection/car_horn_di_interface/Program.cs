using System;

namespace car_horn_di_interface {
    public interface IHorn {
        void Beep ();
    }
    public class Car {
        IHorn horn; // IHorn (Interface) là một Dependecy của Car
        public Car (IHorn horn) => this.horn = horn; // Inject từ hàm  tạo
        public void Beep () => horn.Beep ();
    }
    public class HeavyHorn : IHorn {
        public void Beep () => Console.WriteLine ("(kêu to lắm) BEEP BEEP BEEP ...");
    }

    public class LightHorn : IHorn {
        public void Beep () => Console.WriteLine ("(kêu bé lắm) beep  bep  bep ...");
    }
    class Program {
        static void Main (string[] args) {
            Car car1 = new Car (new HeavyHorn ());
            car1.Beep (); // (kểu to lắm) BEEP BEEP BEEP ...

            Car car2 = new Car (new LightHorn ());
            car2.Beep (); // (kểu bé lắm) beep  bep  bep ...
        }
    }
}