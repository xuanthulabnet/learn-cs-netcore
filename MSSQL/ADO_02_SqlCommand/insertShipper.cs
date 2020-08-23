using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlCommandExam
{
    public partial class Program
    {
      public static void InsertNewShipper()
      {
            // Mở kết nối MS SQL Server
            string sqlconnectStr     = "Data Source=localhost,1433;Initial Catalog=xtlab;User ID=SA;Password=Password123";
            SqlConnection connection = new SqlConnection(sqlconnectStr);
            connection.Open();

            // Câu truy vấn gồm: chèn dữ liệu vào và lấy định danh(Primary key) mới chèn vào
            string queryString = @"INSERT INTO Shippers (Hoten, Sodienthoai) VALUES (@Hoten, @Sodienthoai);
                                   SELECT CAST(scope_identity() AS int)";

            using (SqlCommand cmd = connection.CreateCommand()) {
                cmd.CommandText = queryString;
                cmd.Parameters.AddWithValue("@Hoten", "ABC");
                cmd.Parameters.AddWithValue("@Sodienthoai", 123456);
                var id = cmd.ExecuteScalar();
                Console.WriteLine($"ID dữ liệu: {id}");

            }

            connection.Close();


      }
    }
}
