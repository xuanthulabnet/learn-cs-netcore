using System;
using System.Net;
using System.Threading.Tasks;
namespace CS021_ASYNCHRONOUS {
    public class DownloadAsync {
        
        public static async Task DownloadFile (string url) {
            Action downloadaction = () => {
                using (var client = new WebClient ()) {
                    Console.Write ("Starting download ..." + url);
                    // mảng byte tải về
                    byte[] data = client.DownloadData(new Uri(url));

                    // Lấy tên file để lưu
                    string filename = System.IO.Path.GetFileName(url);
                    System.IO.File.WriteAllBytes(filename, data);
                }
            };

            Task task = new Task(downloadaction);
            task.Start();

            await task;
            Console.WriteLine("Đã hoàn thành tải file");
        }
  }
}