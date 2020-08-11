using System;
using CS009;

namespace CS009_Event {
    class Program {
        static void TestDelegate () {
            Publisher p = new Publisher ();
            SubscriberA sa = new SubscriberA ();
            SubscriberB sb = new SubscriberB ();

            sa.Sub (p);
            sb.Sub (p);

            p.Send ();

        }

        static void TestEventHandler () 
        {
            ClassA p  = new ClassA();
            ClassB sa = new ClassB();
            ClassC sb = new ClassC();

            sa.Sub (p); // sa đăng ký nhận sự kiện từ p
            sb.Sub (p); // sb đăng ký nhận sự kiện từ p

            p.Send ();
        }

        static void Main (string[] args) {
            // TestDelegate ();
            // TestEventHandler();

        }
    }
}