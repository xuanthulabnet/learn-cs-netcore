using System;
using System.IO;
using System.Text;
namespace CS016_Stream_FileStream {
  public class FileStreamTest {
    public static void testWrite () {
      string filepath = "filtetest.txt";
      using (var stream = new FileStream (path: filepath, mode: FileMode.Create, access: FileAccess.Write, share: FileShare.None)) {
        //Write BOM - UTF8
        Encoding encoding = Encoding.UTF8;
        byte[] bom = encoding.GetPreamble ();
        stream.Write (bom, 0, bom.Length);

        string s1 = "Xuanthulab.net -  Xin chào các bạn! \n";
        string s2 = "Ví dụ - ghi file text bằng stream";

        // Encode chuỗi - lưu vào mảng bytes
        byte[] buffer = encoding.GetBytes (s1);
        stream.Write (buffer, 0, buffer.Length); // lưu vào stream

        buffer = encoding.GetBytes (s2);
        stream.Write (buffer, 0, buffer.Length); // lưu vào stream

      }

    }

    public static void testRead () {
      string filepath = "filtetest.txt";
      int SIZEBUFFER = 256;
      using (var stream = new FileStream (path: filepath, mode: FileMode.Open, access: FileAccess.ReadWrite, share: FileShare.Read)) {
        Encoding encoding = UtilsEncoding.GetEncoding (stream);
        Console.WriteLine (encoding.ToString ());
        byte[] buffer = new byte[SIZEBUFFER];
        bool endread = false;
        do {
          int numberRead = stream.Read (buffer, 0, SIZEBUFFER);
          if (numberRead == 0) endread = true;
          if (numberRead < SIZEBUFFER) {
            Array.Clear (buffer, numberRead, SIZEBUFFER - numberRead);
          }
          string s = encoding.GetString (buffer, 0, numberRead);
          Console.WriteLine (s);

        } while (!endread);

      }

    }

    public static void testCopyFile () {
      string filepath_src = "filtetest.txt";
      string filepath_des = "abcdef.txt";

      int SIZEBUFFER = 5; // tăng lên đọc sẽ nhanh
      using (var streamwrite = File.OpenWrite (filepath_des))
      using (var streamread = File.OpenRead (filepath_src)) {
        byte[] buffer = new byte[SIZEBUFFER];
        bool endread = false;
        do {
          int numberRead = streamread.Read (buffer, 0, SIZEBUFFER);
          if (numberRead == 0) endread = true;
          else {
            streamwrite.Write (buffer, 0, numberRead);
          }

        } while (!endread);

      }

    }

  }
}