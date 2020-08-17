using System;

namespace CS014_struct_enum
{
    struct Product {
        public string name;                
        public string description;
        public decimal price;

        public string manufactory;

        public void PrintInfo() {
            Console.WriteLine(name);
            Console.WriteLine(description);
        }

        public Product(string name) {
            this.name = name;
            this.description = "";
            this.price = 0;
            this.manufactory = "";
        }
    }
}
