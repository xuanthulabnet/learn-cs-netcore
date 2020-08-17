using System;

namespace CS011_ClassAdvanced
{  
    class Program
    {
        
        class IndexerExam {
            string ho = "Nguyễn";
            string ten = "Nam";  
            public string this[int index]
            {
                get { 
                    if (index == 0) return ho;
                    else if (index == 1) return ten;
                    else throw new Exception("Chỉ số không tồn tại");
                 }
                set { 
                    if (index == 0)  ho = value;
                    else if (index == 1) ten = value;
                    else throw new Exception("Chỉ số không tồn tại");
                }
            }

        }

        static void TestConstructor() {
            Product p = new Product("ABC");  // Tạo đối tượng, biến p tham chiếu đến đối tượng
            p = null;  
            // Biến p gán bằng null, đối tượng tạo ra phía trên,                      
            // không còn biến nào tham chiếu đến => Nó được đánh dấu
            // sẽ bị thu hồi bởi GC, lúc nào GC chạy do NET quyết định
        }

        static void Main(string[] args)
        {

            /* Kiểm tra hàm hủy */
            // TestConstructor();
            // // Chủ động cho GC giải phóng bộ nhớ
            // GC.Collect();
            // Console.ReadKey();


            /* Kiểm tra toán tử */
            // MyVector v1 = new MyVector(2,3);
            // MyVector v2 = new MyVector(3,4);
            // MyVector v3 = v1 + v2;
            // v3.ShowXY();



            // IndexerExam obj = new IndexerExam();

            // Console.WriteLine(obj[0] + " " + obj[1]);      // đọc obj.ho và obj.ten
            // obj[0] = "Đinh";                               // gán obj.ho 
            // obj[1] = "Quang Hưng";                         // gán obj.name 
            // Console.WriteLine(obj[0] + " " + obj[1]);      // đọc obj.ho và obj.ten

            

        }
    }
}
