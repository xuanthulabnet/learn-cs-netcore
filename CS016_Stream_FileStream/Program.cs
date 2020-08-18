using System;
using System.IO;
using System.Text;
namespace CS016_Stream_FileStream {
    class Program {
        static void Main (string[] args) {


            // WriteData writeData = new WriteData ("filename.txt");
            // //do something
            // writeData.Dispose ();

            FileStreamTest.testWrite();
            FileStreamTest.testRead();
            FileStreamTest.testCopyFile();

        }
    }
}