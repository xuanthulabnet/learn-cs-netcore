using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientExample {
    public class ViduHttpClient : IDisposable {

        public HttpClient httpClient;
        public ViduHttpClient () {
            httpClient = new HttpClient ();
            // Thiết lập httpClient nếu muốn ở đây
        }

        // Giải phóng tài nguyên
        public void Dispose () {
            if (httpClient != null) {
                httpClient.Dispose ();
                httpClient = null;
            }
        }

        // Tải từ url trả về mảng byte dữ liệu
        public async Task<byte[]> DownloadDataBytes (string url) {
            Console.WriteLine ($"Starting connect {url}");
            try {
                HttpResponseMessage response = await httpClient.GetAsync (url);
                response.EnsureSuccessStatusCode ();
                var data = await response.Content.ReadAsByteArrayAsync();
                Console.WriteLine("Received data success");
                return data;
            } catch (Exception e) {
                Console.WriteLine (e.Message);
                throw e;
            }
        }

        // Tải từ url, trả về stream để đọc dữ liệu (xem bài về stream)
        public async Task<Stream> DownloadDataStream (string url) {
            Console.WriteLine ($"Starting connect {url}");
            try {
                HttpResponseMessage response = await httpClient.GetAsync (url);
                response.EnsureSuccessStatusCode ();
                var stream =  await response.Content.ReadAsStreamAsync();
                Console.WriteLine("Stream for read data OK");
                return stream;

            } catch (Exception e) {
                Console.WriteLine (e.Message);
                throw e;
            }
        }
    }

    class Program {

        static async Task Main (string[] args) {

            var httpclient = new ViduHttpClient ();

            // Tải dữ liệu - trả về mảng byte[]
            var url1 = "https://raw.githubusercontent.com/xuanthulabnet/jekyll-example/master/images/jekyll-01.png";
            var task1 = httpclient.DownloadDataBytes(url1);


            //Tải dữ liệu - trả về stream
            string url2 = "https://raw.githubusercontent.com/xuanthulabnet/linux-centos/master/docs/samba1.png";
            var task2 = httpclient.DownloadDataStream (url2);




            await task1; // chờ cho tải xong
            byte[] dataimg = task1.Result;
            // Lưu mảng ra file anh1.png
            string filepath = "anh1.png";
            using (var stream = new FileStream (filepath, FileMode.Create, FileAccess.Write, FileShare.None)) {
                stream.Write (dataimg, 0, dataimg.Length);
                Console.WriteLine("save " + filepath);
            }


            await task2; // chờ cho tải xong
            int SIZEBUFFER = 500;
            // Đọc dữ liệu từ stream trả về, lưu ra file anh2.pnng
            string filepath2 = "anh2.png";
            using (var streamwrite = File.OpenWrite (filepath2))
            using (var streamread = task2.Result) {
                byte[] buffer = new byte[SIZEBUFFER]; // tạo bộ nhớ đệm lưu dữ liệu khi đọc stream
                bool endread = false;
                do {
                    int numberRead = streamread.Read (buffer, 0, SIZEBUFFER);
                    if (numberRead == 0) endread = true;
                    else {
                        streamwrite.Write (buffer, 0, numberRead);
                    }

                } while (!endread);

            }
            Console.WriteLine("save " + filepath2);
        }
    }
}