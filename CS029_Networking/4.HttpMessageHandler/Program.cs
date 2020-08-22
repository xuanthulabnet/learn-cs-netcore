using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
namespace HttpClientHandlerExample {

    class Program {
        static CookieContainer cookieContainer = new CookieContainer (); // Sử dụng CookieContainer riêng, để lưu lại Cookie - hoặc thêm cookie

        //nested class - MyHttpClientHandler
        public class MyHttpClientHandler : HttpClientHandler {

            // Khởi tạo và thiết lập cho handler
            public MyHttpClientHandler (CookieContainer cookie_container) {

                CookieContainer = cookie_container; // Thay thế CookieContainer mặc định
                AllowAutoRedirect = false; // không cho tự động Redirect
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip; // Chấp nhận nén dữ liệu
                UseCookies = true;
            }

            // Nạp chồng SendAsync
            protected override async Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken) {
                ShowHeaders ("Request header trước khi qua Handler ", request.Headers);

                var task = base.SendAsync (request, cancellationToken); // gọi SendAsync của lớp cơ sở (bắt buộc)
                await task;

                ShowHeaders ("Request header sau khi qua Handler ", request.Headers);

                // Xem Cookie nếu  có
                // var uri = request.RequestUri;
                // var cookieHeader = CookieContainer.GetCookieHeader(uri);
                // Console.WriteLine(cookieHeader); 

                return task.Result;
            }
        }

        // In thông tin Header
        public static void ShowHeaders (string lable, HttpHeaders headers) {
            Console.WriteLine (lable);
            foreach (var header in headers) {
                string value = string.Join (" ", header.Value);
                Console.WriteLine ($"{header.Key,20} : {value}");
            }
            Console.WriteLine ();

        }

        public static async Task<string> GetWebContent (string url) {
            using (var myHttpClientHandler = new MyHttpClientHandler (cookieContainer))
            using (var httpClient = new HttpClient (myHttpClientHandler)) {

                Console.WriteLine ($"Starting connect {url}");
                httpClient.DefaultRequestHeaders.Add ("Accept", "text/html,application/xhtml+xml+json");
                httpClient.DefaultRequestHeaders.Add ("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
                HttpResponseMessage response = await httpClient.GetAsync (url);

                response.EnsureSuccessStatusCode ();
                string htmltext = await response.Content.ReadAsStringAsync ();
                return htmltext;
            }
        }

        static void Main (string[] args) {
            string url = "https://www.google.com.vn/";

            cookieContainer.Add (new Uri (url), new Cookie ("NameCookie", "ValueCookie")); // Thêm Cookie khi gửi Requests    

            var htmltask = GetWebContent (url);
            htmltask.Wait (); // cho hoàn thành tác vụ
            var html = htmltask.Result; // đọc chuỗi trả về (content)
            Console.WriteLine (html != null ? html.Substring (0, 150) : "Lỗi");
        }

    }
}