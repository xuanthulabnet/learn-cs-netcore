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
    public class ViduHttpClient {
        HttpClient _httpClient = null;
        public HttpClient httpClient => _httpClient ?? (new HttpClient());
 
        // Post Json Data
        public async Task<string> SendAsyncJson(string url, string json) 
        {
            Console.WriteLine($"Starting connect {url}");
            try {
                
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = httpContent;
                var response = await httpClient.SendAsync(request);
                var rcontent = await response.Content.ReadAsStringAsync();
                return rcontent;
        
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://xuanthulab.net/api/";
            var json =@"
                {
                    ""id"":""1"", 
                    ""method"":""timestampToDate"", 
                    ""params"": {""routin"":""UnixTime"", ""timestamp"":""1483228800""}
                }";
           ViduHttpClient vidu = new ViduHttpClient();
           var task = vidu.SendAsyncJson(url, json);
           task.Wait();
           Console.WriteLine(task.Result);  
        }
    }
}
