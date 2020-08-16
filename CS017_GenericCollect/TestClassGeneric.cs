using System;

namespace CS017_GenericCollect {
  class MyClass<T> {
    private T bien;

    public MyClass (T value) {
      bien = value;
    }

    public T TestMethod (T pr) {
      Console.WriteLine (pr);
      return bien;
    }

    public T thuoctinh { get; set; }
  }
}