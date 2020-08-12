using System;

namespace CS010_Constructors
{
    class Product 
    {
        private string name;
        private decimal price;

        // Khai báo phương thức khởi tạo với 2 tham số
        public Product(string nameproduct, decimal priceproduct)
        {
            name = nameproduct;
            price = priceproduct;
        }

        // Khai báo phương thức khởi tạo không tham số
        public Product()
        {
            name = "Không tên";
            price = 0;
        }

        public string Name 
        {
            set { name = value;}
            get { return name;}
        }

    }


    class Category 
    {
        private string categoryname;

        // Dùng thân biểu thức cho phương thức khởi tạo
        public Category(string nameofCategory) => categoryname = nameofCategory;
        public String Name 
        {
            // Dùng thân biểu thức cho setter, getter
            set => categoryname = value;
            get => categoryname;
        }
    }

  // CategoryMobile kế thừa Category
  class CategoryMobile : Category
  {
      private string description;
        // Khi phương thức khởi tạo này được gọi, nó gọi phương thức khởi tạo có 
        // một tham số của lớp cơ sở (Category) trước, rồi mới thi hành các code
        // trong thân của phương thức khởi tạo này
        public CategoryMobile(string nameofCategory, string mota) : base(nameofCategory)
        {
            description = mota;
        }    
        
        public string Description 
        {
            set => description = value;
            get => description;
        }

  }

  class MyLib 
  {
      public static double PI = 3.14;

      private MyLib()
      {
      }
  }

  class MyColorCode
  {
        public static string color_primary;
        public static string color_success;
        public static string color_danger;
        public static string color_warning;
        public static string color_info;

        static MyColorCode()
        {
            Console.WriteLine("Static MyColorCode Contructor Call");
            color_danger = "Red";
            color_info = "Cyan";
            color_primary = "Navy";
            color_success = "Green";
            color_warning = "Yellow";
        }


  }


  class Program
    {
        static void Main(string[] args)
        {
            // // Tạo đối tượng, không truyền tham số nên
            // // nó gọi phương thức khởi tạo không tham số
            Product product1 = new Product();
            Console.WriteLine(product1.Name); // Không tên

            Product product2 = new Product("Laptop", 123);
            Console.WriteLine(product2.Name); // Laptop


            CategoryMobile cat1 = new CategoryMobile("Điện thoại", "Danh mục các loại điện thoại");
            Console.WriteLine(cat1.Name);
            Console.WriteLine(cat1.Description);


            Console.WriteLine(MyColorCode.color_danger);
            Console.WriteLine(MyColorCode.color_info);

            

        }
    }
}
