using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace TCP
{
    class Program
    {    
        public class TpcServerAsyncv {
            readonly int PortNumber;
            public TpcServerAsyncv(int portNumber) => PortNumber = portNumber;
            public async Task StartLinster()
            {
                try
                {
                    var listener = new TcpListener(IPAddress.Any, PortNumber);
                    Console.WriteLine($"Listener lắng nghe ở cổng {PortNumber}");
                    listener.Start();

                    while (true)
                    {
                        Console.WriteLine("Chờ client kết nối ...");
                        TcpClient client = await listener.AcceptTcpClientAsync();
                        Task t = RunClientRequestAsync(client);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception of type {ex.GetType().Name}, Message: {ex.Message}");
                }
            }
            private Task RunClientRequestAsync(TcpClient client)
            {
                 Action action = async () => {
                    try
                    {
                        using (client)
                        {
                            Console.WriteLine("client kết nối");  
                            using (NetworkStream stream = client.GetStream())
                            using (StreamWriter  writer = new StreamWriter(stream))
                            using (StreamReader  reader = new StreamReader(stream))
                            {
                                writer.AutoFlush = true;
                                bool exit = false;
                                while (!exit) {
                                    string data = await reader.ReadLineAsync();
                                    switch (data.ToLower()) 
                                    {
                                        case "time": 
                                            await writer.WriteLineAsync(DateTime.Now.ToLongTimeString());
                                        break;
                                        case "exit":
                                            exit = true;
                                            await writer.WriteLineAsync("exit");
                                        break;
                                        default:
                                            await writer.WriteLineAsync("Không thấy lệnh");
                                        break;
                                    } 
                                }
                                                        
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi {ex.GetType().Name}, Message: {ex.Message}");
                    }
                    Console.WriteLine("Client ngắt kế nối");
                };

                Task task = new Task(action);
                task.Start();
                return task;
            } 
        }
        static async Task  Main(string[] args)
        {
            await (new TpcServerAsyncv(1950)).StartLinster();
        }

    }
}