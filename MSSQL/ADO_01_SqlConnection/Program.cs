using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace ADO_01_SqlConnection
{
    class Program
    {
        // Lấy chuỗi kết nối từ file config định dạng json,
        // Điểm lưu: csl:ketnoi2
        public static string GetConnectString() {
            var configBuilder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())      // file config ở thư mục hiện tại
                       .AddJsonFile("appconfig.json");                    // nạp config định dạng JSON
            var configurationroot = configBuilder.Build();                // Tạo configurationroot
            return configurationroot["csdl:ketnoi2"];

        }

        static void Main(string[] args)
        {
            // Exam1.Test();

            String sqlConnectString = GetConnectString();
            SqlConnection connection = new SqlConnection(GetConnectString());
            connection.StatisticsEnabled = true;
            connection.FireInfoMessageEventOnUserErrors = true;

            connection.StateChange += (object  sender, StateChangeEventArgs e) => {
                    Console.WriteLine($"Trạng thái hiện tại: {e.CurrentState}, trạng thái trước: " + $"{e.OriginalState}");
            }; 

            // Mở kết nối
            connection.Open();
            
            // Thực hiện các truy vấn tại đây ...
            
            connection.Close(); 

        }
    }
}
