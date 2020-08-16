using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
namespace CS017_GenericCollect {

    class Program {

        static void Main (string[] args) {
            // TestMethoGeneric.TestSwap ();

            MyClass<double> myClass = new MyClass<double>(123.123);
            myClass.TestMethod(123);

        }
    }
}