using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerExample {

    class Program {

        
        // Chạy một HTTP Server, prefixes example: new string[] { "http://*:8080/" }
        public static async Task RunWebserverAsync (params string[] prefixes) {
            
            if (!HttpListener.IsSupported)
                throw new Exception ("Máy không hỗ trợ HttpListener.");

            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException ("prefixes");
            
            // Khởi tạo HttpListener
            HttpListener listener = new HttpListener ();

            foreach (string s in prefixes) {
                listener.Prefixes.Add (s);
            }
            Console.WriteLine ("Server start ...");
            // Http bắt đầu lắng nghe truy vấn gửi đến
            listener.Start();

            do {

                
                HttpListenerContext  context = await listener.GetContextAsync();
                HttpListenerRequest  request  = context.Request;
                HttpListenerResponse response = context.Response;

                // Refuse connect
                if (response.StatusCode != 200) 
                {
                    Console.WriteLine($"{request.HttpMethod} {request.RawUrl} LỖI {response.StatusCode}");
                    continue;
                }

               
                Console.WriteLine($"{request.HttpMethod} {request.RawUrl} from IP {context.Request.RemoteEndPoint.ToString()}");
                

                // Gửi thông tin về cho Client
                context.Response.Headers.Add ("content-type", "text/html");
                context.Response.StatusCode = (int) HttpStatusCode.OK;
                byte[] buffer = GenerateHTMP(context.Request);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                await output.WriteAsync (buffer, 0, buffer.Length);
                

            } while (listener.IsListening);


            //  listener.Stop();    

        }

        // Tạo nội dung HTML trả về cho Client (HTML chứa thông tin về Request)
        public static byte[] GenerateHTMP (HttpListenerRequest request) {
            string format = @"<!DOCTYPE html>
                                <html lang=""en""> 
                                    <head><meta charset=""UTF-8"">{0}</head> 
                                    <body>{1}</body> 
                                </html>";
            string head = "<title>Test WebServer</title>";
            var body = new StringBuilder ();
            body.Append ("<h1>Request Info</h1>");
            body.Append ("<h2>Request Header:</h2>");

            // Header infomation
            var headers = from key in request.Headers.AllKeys
            select $"<div>{key} : {string.Join(",", request.Headers.GetValues(key))}</div>";
            body.Append (string.Join ("", headers));

            //Extract request properties
            body.Append ("<h2>Request properties:</h2>");
            var properties = request.GetType ().GetProperties ();
            foreach (var property in properties) {
                var name_pro = property.Name;
                string value_pro;
                try {
                    value_pro = property.GetValue (request).ToString ();
                } catch (Exception e) {
                    value_pro = e.Message;
                }
                body.Append ($"<div>{name_pro} : {value_pro}</div>");

            };
            string html = string.Format (format, head, body.ToString ());
            return Encoding.UTF8.GetBytes(html);
        }

        static async Task Main (string[] args) {
            await RunWebserverAsync (new string[] { "http://*:8080/" });
            Console.ReadKey();
            Console.WriteLine("End");
        }

    }
}