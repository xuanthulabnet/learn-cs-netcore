using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlCommandExam
{
    public partial class Program
    {
      public static void CallStoredProcedure()
      {
            string sqlconnectStr     = "Data Source=localhost,1433;Initial Catalog=xtlab;User ID=SA;Password=Password123";
            SqlConnection connection = new SqlConnection(sqlconnectStr);
            connection.Open();

            // Thi hành thủ tục PROCEDURE [dbo].[getproduct](@id int) trong MS SQL Server
            SqlCommand cmd   = new SqlCommand("getproduct", connection);
            cmd.CommandType  = CommandType.StoredProcedure;
            // Tham số của procedure
            cmd.Parameters.Add(
                new SqlParameter() {
                    ParameterName   = "@id",
                    SqlDbType       = SqlDbType.Int,
                    Value           = 10
                }
            );

            // Đọc kết quả trả về
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var ten = reader["TenSanpham"];
                    var gia = reader["Gia"];

                    Console.WriteLine($"{ten} \t {gia}");
                }
            }


          connection.Close();
      }
    }
}
