using System;

namespace CS011_ClassAdvanced
{ 
    class MyVector {
        double x;
        double y;
        public MyVector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public void ShowXY() {
            Console.WriteLine("x = " + x);
            Console.WriteLine("y = " + y);
        }

        public static MyVector operator+(MyVector a, MyVector b)
        {
            double sx = a.x + b.x;
            double sy = a.x + b.y;
            MyVector v = new MyVector(sx,sy);
            return v;
        }
    }
 
}
