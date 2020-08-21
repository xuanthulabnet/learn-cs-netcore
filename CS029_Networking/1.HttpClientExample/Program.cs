using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HttpClientExample
{
    
    class Program
    {
        /// In ra thông tin các Header của HTTP Response
        public static void ShowHeaders(HttpHeaders headers) 
        {
            Console.WriteLine("Các Header:");
            foreach (var header in headers) 
            {
                string value = string.Join(" ", header.Value);              
                Console.WriteLine($"{header.Key,20} : {value}"); 
             }
            Console.WriteLine(); 
         }

        // Tải về và hiện thị thông tin trang tải về, 
        // url là địa chỉ cần tải ví dụ: https://google.com.vn 
        public static async Task<string> GetWebContent(string url) 
        {
            using (var httpClient = new  HttpClient())
            {
                Console.WriteLine($"Starting connect {url}");
                try {
                    // Thêm header vào HTTP Request
                    httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    
                    // Phát sinh Exception nếu mã trạng thái trả về là lỗi 
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Tải thành công - statusCode {(int)response.StatusCode} {response.ReasonPhrase}");
                        // Đọc thông tin header trả về
                        ShowHeaders(response.Headers);
                        

                        Console.WriteLine("Starting read data");

                        // Đọc nội dung content trả về
                        string htmltext = await response.Content.ReadAsStringAsync(); 
                        Console.WriteLine($"Nhận được {htmltext.Length} ký tự"); 
                        Console.WriteLine();
                        return htmltext;
                    }
                    else 
                    {
                        Console.WriteLine($"Lỗi - statusCode {response.StatusCode} {response.ReasonPhrase}");
                        return null;
                    }
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
        static void Main(string[] args)
        {
            var htmltask = GetWebContent("https://google.com.vn");
            htmltask.Wait(); // Chờ tải xong
            // Hoặc wait htmltask; nếu chuyển Main thành async 
           
            var html = htmltask.Result;
            Console.WriteLine(html!=null ? html.Substring(0, 255): "Lỗi"); 
        }
    }
}