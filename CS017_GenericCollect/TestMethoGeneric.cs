namespace CS017_GenericCollect {
    class TestMethoGeneric {

        // Gán giá trị của a vào b và ngược lại
        // a, b có cùng kiểu T bất kỳ
        static void Swap<T> (ref T a, ref T b) {
            T c = a;
            a = b;
            b = c;
        }

        public static void TestSwap () {
            int a = 1;
            int b = 2;
            Swap<int> (ref a, ref b); // Hàm trên kiểu T = int
            System.Console.WriteLine ($"a = {a}; b = {b}"); // a = 2; b = 1

            string a1 = "A";
            string b1 = "B";
            Swap<string> (ref a1, ref b1); // Hàm trên kiểu T = string
            System.Console.WriteLine ($"a1 = {a1}; b1 = {b1}"); // a1 = B; b1 = C
        }

    }
}