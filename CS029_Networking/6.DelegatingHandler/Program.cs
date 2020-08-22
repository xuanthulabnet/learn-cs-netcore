using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text;
namespace HttpClientExample
{
 
    class Program
    {        
        static CookieContainer cookieContainer = new CookieContainer();  

        public class ChangeUri : DelegatingHandler
        {
            public ChangeUri(HttpMessageHandler innerHandler) : base(innerHandler)
            {
            } 

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var host = request.RequestUri.Host.ToLower();
                Console.WriteLine($"Check in  ChangeUri - {host}");
                if (host.Contains("google.com")) 
                {
                    request.RequestUri = new Uri("https://github.com/");
                } 
                
                return base.SendAsync(request, cancellationToken);
            }
        }
        public class DenyAccessFacebook : DelegatingHandler
        {
            public DenyAccessFacebook(HttpMessageHandler innerHandler) : base(innerHandler)
            {
            } 

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                
                var host = request.RequestUri.Host.ToLower();
                Console.WriteLine($"Check in  DenyAccessFacebook - {host}");
                if (host.Contains("facebook.com")) 
                {
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content  = new ByteArrayContent(Encoding.UTF8.GetBytes("Không được truy cập"));
                    return Task.FromResult<HttpResponseMessage>(response);  
                } 
 
                return base.SendAsync(request, cancellationToken);
            }
        }

        
        public class MyHttpClientHandler : HttpClientHandler {
            public MyHttpClientHandler(CookieContainer  cookie_container) {
                
                CookieContainer         = cookie_container;     // Thay thế CookieContainer mặc định
                AllowAutoRedirect       = false;                // không cho tự động Redirect
                AutomaticDecompression  = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                UseCookies              = true; 
            }
 
            protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken)
            {
                ShowHeaders("Request header trước khi qua Handler MyHttpClientHandler", request.Headers);

                var task  = base.SendAsync(request, cancellationToken); // bắt buộc gọi
                await task;                                             

                ShowHeaders("Request header sau khi qua Handler MyHttpClientHandler", request.Headers);


                return task.Result;
            } 
        }


        // Chức năng in thông tin Header
        public static void ShowHeaders(string lable, HttpHeaders headers)
        {
            Console.WriteLine(lable);
            foreach (var header in headers)
            {
                string value = string.Join(" ", header.Value);              
                Console.WriteLine($"{header.Key,20} : {value}");
            }
            Console.WriteLine();
            
         }


        public static async Task<string> GetWebContent(string url)
        {
            //  Midleware Pipeline:
            //  facebookHandler -- changeUri -- myHttpClientHandler

            using (var myHttpClientHandler = new MyHttpClientHandler(cookieContainer))
            using (var changeUri  = new ChangeUri(myHttpClientHandler))
            using (var facebookHandler  = new DenyAccessFacebook(changeUri)) 

            using (var httpClient = new  HttpClient(facebookHandler))
            {
                
                Console.WriteLine($"Starting connect {url}");
                httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); 
                string htmltext = await response.Content.ReadAsStringAsync();
                return htmltext;    
            }
        }
        
        static void Main(string[] args)
        {
            // string url = "https://www.facebook.com/xuanthulab/";
            // string url = "https://xuanthulab.net";
            string url = "https://www.google.com/";
            

            var htmltask = GetWebContent(url); 
            htmltask.Wait();                                                                // cho hoàn thành tác vụ
            var html = htmltask.Result;                                                     // đọc chuỗi trả về (content)
            Console.WriteLine(html!=null ? html.Substring(0, Math.Min(150, html.Length)): "Lỗi"); 
        }

    }
}
