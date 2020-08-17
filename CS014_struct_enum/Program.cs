using System;

namespace CS014_struct_enum
{
    class Program
    {
        enum HocLuc {Kem, TrungBinh, Kha, Gioi}
        static void Main(string[] args)
        {
            /* Sử dụng struct Product */
            Product giuongngu = new Product("Giường");
            giuongngu.description = "Đồ nội thất XYZ";

            Product banlamviec = giuongngu;
            banlamviec.name = "Bàn";

            giuongngu.PrintInfo();
            banlamviec.PrintInfo();


            /* Su dung enum */
            // khai báo biến hocluc kiểu enum và khởi tạo giá trị bằng HocLuc.Kha
            HocLuc hocluc = HocLuc.Kha;    
            switch (hocluc) 
            {
                case HocLuc.Kem: 
                    Console.WriteLine("Học lực kém");
                break;
                case HocLuc.Kha: 
                    Console.WriteLine("Học lực Kha");
                break;
                 case HocLuc.Gioi: 
                    Console.WriteLine("Học lực Giỏi");
                break;
                default:
                    Console.WriteLine("Học lực TB");
                break;

            }

            HocLuc hoc = (HocLuc)2;
            Console.WriteLine(hoc); //Kha


            HocLuc hocluc_hocsinhA = HocLuc.Kha;
            int    hocluc_hocsinhB = 2;



            // int a = (int)HocLuc.Kha;
            // Console.WriteLine(a);

        }
    }
}
