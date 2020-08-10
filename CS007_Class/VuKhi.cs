using System;

namespace CS007_Class
{
    public class OverloadingExample {
        public static int Sum(int a, int b)
        {
            return a + b;
        }

        public static double Sum(double a, double b)
        {
            return a + b;
        } 
    }
    public class VuKhi
    {

        /// Tên của vũ khí: Súng Lục, Súng Trường, Dao ..
        public string name = "Tên Vũ Khí";

        /// Độ sát thương 10 cấp độ
        int dosatthuong = 0; 

        /// Phương thức khởi tạo (được gọi khi toán tử new tạo đối tượng)
        /// tên phương thức trùng tên lớp, trường hợp này không tham số
        public VuKhi() 
        {
            this.dosatthuong = 1;
        }

        /// Phương thức khởi tạo (được gọi khi toán tử new tạo đối tượng)
        /// tên phương thức trùng tên lớp, trường hợp này có tham số
        public VuKhi(string name, int dosatthuong) 
        {
            this.name  = name;
            SetDoSatThuong(dosatthuong);
        }

        /// Hàm này thiết lập  độ sát thương
        public void SetDoSatThuong(int mucdo)
        {
            this.dosatthuong = mucdo;
        }

        // In ra: Tên vu khí: * * * * * * * * (bằng độ sát thương)
        public void TanCong() 
        {
            Console.Write(name + ": \t");
            for (int i = 0; i < dosatthuong; i++)
            {
                Console.Write(" * ");   
            }
            Console.WriteLine();
        }
    }
}