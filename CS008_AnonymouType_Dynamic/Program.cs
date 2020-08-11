using System;

namespace CS008_AnonymouType_Dynamic {
    class Program {
        static void Main (string[] args) {

            // Tạo đối tượng kiểu Anonymous
            var myProfile = new {
                name = "XuanThuLab",
                age = 20,
                skill = "ABC"
            };
            Console.WriteLine (myProfile.name);

            TestFunc(myProfile);



        }

        static void TestFunc (dynamic dvar) 
        {
            // dynamic d1 = 7;
            // dynamic d2 = "a string";
            // dynamic d3 = System.DateTime.Today;
            // dynamic d4 = System.Diagnostics.Process.GetProcesses();
            
            Console.WriteLine (dvar.age); // ở thời điểm biên dịch - không biết dvar có thuộc tính age hay không, nhưng nó vẫn biên dịch
        }

    }
}