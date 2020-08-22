using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using System.Net.Sockets;
using System.Net.Http;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace TCP
{
 
    class Program
    {    
        // Phương thức này gọi bởi RemoteCertificateValidationDelegate trong quá trình xác thức SSL
        // chỉ dùng khi kết nối HTTPS

        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
           Console.WriteLine("ValidateServerCertificate");
           if (sslPolicyErrors == SslPolicyErrors.None)  return true;
            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
            
            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }

        // Kết nối đến server Tpc bằng TcpClient, đọc nội dung trả về
        public static  async Task ReadHtmlAsync(string url) {

            Uri uri = new Uri(url);
             

            using (var client = new TcpClient(uri.Host, 443)) 
            {
                Console.WriteLine($"Start get {url}");

                // var hostAdress = await Dns.GetHostAddressesAsync(uri.Host);
                // IPAddress ipaddrress = hostAdress[0];
            
                // Console.WriteLine($"Host: {uri.Host}, IP: {ipaddrress}:{uri.Port}"); 
                // await client.ConnectAsync(ipaddrress.MapToIPv4(), uri.Port);
                // Console.WriteLine("Connected");
                // Console.WriteLine();

 
                Stream stream;
                if (uri.Scheme == "https")
                {
                    // SslStream
                    stream = new SslStream(client.GetStream(),false, 
                                           new RemoteCertificateValidationCallback (ValidateServerCertificate),
                                           null);
                   (stream as SslStream).AuthenticateAsClient(uri.Host);             
                } 
                else {
                    // NetworkStream
                    stream = client.GetStream(); 
                }

                Console.WriteLine($"Get Stream OK: {stream.GetType().Name}");
                
            
                // Xem: /psr-7-chuan-giao-dien-thong-diep-http.html#HTTPRequest
                StringBuilder header = new StringBuilder();
                header.Append($"GET {uri.PathAndQuery} HTTP/1.1\r\n");
                header.Append($"User-agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)\r\n");
                header.Append($"Host: {uri.Host}\r\n");

                header.Append($"\r\n");
                                header.Append($"\r\n");


                Console.WriteLine("Request:");
                Console.WriteLine(header); 

                byte[]  bsend  = Encoding.UTF8.GetBytes(header.ToString());
                await stream.WriteAsync(bsend, 0, bsend.Length);
                
                await stream.FlushAsync(); 

                Console.WriteLine("Send Message OK");


                var ms = new MemoryStream(); 
                byte [] buffer = new byte[255];
                int bytes = -1;
                do
                {
                    bytes = await stream.ReadAsync(buffer, 0, buffer.Length);
                    

                    // Lưu dữ liệu tải về vào ms
                    ms.Write(buffer, 0, bytes); 

                    Array.Clear(buffer, 0, buffer.Length);   
                    
                } while (bytes != 0);

                Console.WriteLine($"Read OK");

                ms.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(ms); 
                string html = reader.ReadToEnd(); 
                Console.WriteLine("Response:");
                Console.WriteLine(html);

            }
        }
        static async Task  Main(string[] args)
        {
            string url = "https://www.google.com.vn";
            await ReadHtmlAsync(url);
        }

    }
}