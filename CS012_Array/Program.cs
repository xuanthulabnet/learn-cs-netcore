using System;
using System.Linq;

namespace CS012_Array
{
    class Program
    {

        static void testForEach()
        {
            int[] numbers = {9, 8, 7, 6, 5, 4, 3, 2, 1 };
            Array.ForEach<int>(numbers, (int n) => {
                Console.WriteLine(n);
            });
        }
        static void testFindAll()
        {
            int[] numbers = {9, 8, 7, 6, 5, 4, 3, 2, 1 };

            // Định nghĩa Predicate là một delegate tham số kiểu int
            // trả về true nếu số chia hết cho 2
            Predicate<int> predicate =  (int number) => {
                return number % 2 == 0; 
            };
            // Tìm các số chẵn
            int[] cacsochan = Array.FindAll(numbers, predicate);
            
            // In kết quả
            Action<int> printnumber = (int number) => Console.WriteLine(number);
            Array.ForEach(cacsochan, printnumber);
        }

        static void testMultidimensional()
        {
            int[][] myArray = new int[][] 
            {
                new int[] {1,2},
                new int[] {2,5,6},
                new int[] {2,3},
                new int[] {2,3,4,5,5}
            };


            foreach (var arr in myArray) {
                foreach (var e in arr) {
                    Console.Write(e + " ");
                }
                Console.WriteLine();
            }
        }


        static void Main(string[] args)
        {
            // testForEach();
            // testFindAll();
            testMultidimensional();
 

        }


    }
}
