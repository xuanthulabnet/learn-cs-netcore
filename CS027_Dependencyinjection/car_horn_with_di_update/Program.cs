using System;

namespace car_horn_with_di_update
{
    class Program
    {
        static void Main(string[] args)
        {
            Horn horn = new Horn(10); 

            var car = new Car(horn); // horn inject vào car
            car.Beep(); // Beep - beep - beep ...

        }
    }
}
