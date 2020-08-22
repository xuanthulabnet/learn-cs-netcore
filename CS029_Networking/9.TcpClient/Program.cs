using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace TCP
{
    class Program
    {    
        public static  async Task StartConnectAsync(IPAddress iPAddress, int Port) {

            try {
                using (var client = new TcpClient()) 
                {
    
                    await client.ConnectAsync(iPAddress, Port);
                    Console.WriteLine("Đã kết nối");

                    using (NetworkStream stream = client.GetStream())
                    using (StreamWriter writer = new StreamWriter(stream))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        writer.AutoFlush = true;
                        bool quite = false;
                        while (!quite) {
                            Console.Write("Nhập nội dung (time, exit):");
                            string mgs = Console.ReadLine(); 
                            if (mgs == "exit")
                                quite = true;

                            await writer.WriteLineAsync(mgs);
                            string mgs_receive = await reader.ReadLineAsync();
                            Console.WriteLine(mgs_receive); 
                        }
                                                    
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"Lỗi {ex.GetType().Name}, Message: {ex.Message}");
            }
        }
        static async Task  Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int       port = 1950;

            await StartConnectAsync(ip, port);
        }

    }
}