using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text;
namespace SocketsHttpHandler
{
 
    class Program
    {        
        static CookieContainer cookieContainer = new CookieContainer();  // Sử dụng CookieContainer riêng, để lưu lại Cookie - hoặc thêm cookie
 
        public static async Task<string> GetWebContent(string url)
        {
            using (var socketsHandler = new SocketsHttpHandler())
            {
                socketsHandler.CookieContainer         = cookieContainer;     // Thay thế CookieContainer mặc định
                socketsHandler.AllowAutoRedirect       = false;                // không cho tự động Redirect
                socketsHandler.AutomaticDecompression  = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                socketsHandler.UseCookies              = true;


                using (var httpClient = new  HttpClient(socketsHandler))
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
        }
        
        static void Main(string[] args)
        {
            string url = "https://www.google.com.vn/";
            
            cookieContainer.Add(new Uri(url), new Cookie("NameCookie", "ValueCookie"));     // Thêm Cookie khi gửi Requests    

            var htmltask = GetWebContent(url); 
            htmltask.Wait();                                                                // cho hoàn thành tác vụ
            var html = htmltask.Result;                                                     // đọc chuỗi trả về (content)
            Console.WriteLine(html!=null ? html.Substring(0, 150): "Lỗi"); 

            Console.WriteLine();
            Console.WriteLine("Cookie Header:");
            Console.WriteLine(cookieContainer.GetCookieHeader(new Uri(url)));
        }

    }
}
