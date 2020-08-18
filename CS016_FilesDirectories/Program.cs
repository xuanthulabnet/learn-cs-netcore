using System;
using System.IO;
using System.Text;
namespace CS016_FilesDirectories {
    class Program {

        static void testWriteAllText () {
            var filename = "test.txt";
            string contentfile = "Xin chào! xuanthulab.net";

            // Lấy thư mục Document của User trên hệ thống
            var directory_mydoc = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);

            var fullpath = Path.Combine (directory_mydoc, filename);
            File.WriteAllText (filename, contentfile);

            Console.WriteLine ($"File lưu tại {directory_mydoc}{Path.DirectorySeparatorChar}{filename}");
        }

        static void testAppendAllText() {
            var filename = "test.txt";
            string contentfile = "\nXin chào! xuanthulab.net - " + DateTime.Now.ToString ();

            var directory_mydoc = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
            var fullpath = Path.Combine (directory_mydoc, filename);

            if (File.Exists (fullpath)) {
                // File đã tồn tại - nối thêm nội dung
                File.AppendAllText (fullpath, contentfile);
            } else {
                // tạo mới vì chưa tồn tại file
                File.WriteAllText (fullpath, contentfile);
            }
            // Đọc nội dung File
            Console.WriteLine (fullpath);
            string s = File.ReadAllText (fullpath);
            Console.WriteLine (s);
        }

        static void ListFileDirectory(string path)
        {
            String[] directories = System.IO.Directory.GetDirectories(path);
            String[] files = System.IO.Directory.GetFiles(path);
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
            foreach (var directory in directories)
            {
                Console.WriteLine(directory);
                ListFileDirectory(directory); // Đệ quy
            }
        }

        static void Main (string[] args) {

            // GetDriveInfomation.GetDrivesInfo ();

            // testWriteAllText();

            // testAppendAllText();



            // var directory_mydoc = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
            // String[] files = System.IO.Directory.GetFiles(directory_mydoc);
            // String[] directories = System.IO.Directory.GetDirectories(directory_mydoc);
            
            // foreach (var file in files)
            // {
            //     Console.WriteLine(file);
            // }

            // foreach(var directory in directories)
            // {
            //     Console.WriteLine(directory);
            // }

            // var directory_mydoc = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
            // ListFileDirectory(directory_mydoc);



        }
    }
}