using System;

namespace CS007B_PARTIAL {
  class MobileProduct {
    public Manufactory manufactory { set; get; }

    // Lớp Manufactory nằm trong MobileProduct 
    public class Manufactory {
      string address;
      public Manufactory (string address) {
        this.address = address;
      }
      public void ShowAddress () {
        Console.WriteLine (address);
      }
    }

    public void ProductInfo () {
      manufactory.ShowAddress ();
    }
  }

}