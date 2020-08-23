using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlCommandExam
{
    public partial class Program
    {
      public static void ReadCategory()
      {
          string sqlconnectStr     = "Data Source=localhost,1433;Initial Catalog=xtlab;User ID=SA;Password=Password123";
          SqlConnection connection = new SqlConnection(sqlconnectStr);
          connection.Open();

          using (SqlCommand command = new SqlCommand("SELECT DanhmucID, TenDanhMuc FROM Danhmuc;", connection))
          using (SqlDataReader reader = command.ExecuteReader())
          {
              // Kiểm tra có kết quả trả về
              if (reader.HasRows)
              {
                  // Đọc từng dòng kết quả cho đế hết
                  while (reader.Read())
                  {
                      // Mỗi lần thực hiện read.Read() con trỏ sẽ dịch chuyển đến dòng dữ liệu tiếp
                      // theo nếu có.
                      // Đọc dòng dữ liệu dòng hiện tại dùng ký hiệu [chỉ-số-cột] hoặc các hàmg lấy dữ liệu
                      // từng cột như reader.GetInt21(chỉ-số-cột)
                      Console.WriteLine("{0}\t{1}", reader[0].ToString(), reader.GetString(1));
                  }
              }
              else  Console.WriteLine("No rows found.");
          }

          connection.Close();
      }
    }
}
