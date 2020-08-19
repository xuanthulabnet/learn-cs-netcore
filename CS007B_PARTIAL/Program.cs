using System;

namespace CS007B_PARTIAL
{
    class Program
    {
        static void Main(string[] args)
        {
            // Container.Nested nested = new Container.Nested();

            MobileProduct product = new MobileProduct();
            product.manufactory =  new MobileProduct.Manufactory("Abc ...");
            product.ProductInfo();

        }
    }
}
