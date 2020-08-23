using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlCommandExam {
  public partial class Program {
    public static void CreateSqlCommand01 () {

      // Mở kết nối đến SQL Server
      string sqlconnectStr = "Data Source=localhost,1433;Initial Catalog=xtlab;User ID=SA;Password=Password123";
      SqlConnection connection = new SqlConnection (sqlconnectStr);
      connection.Open ();

      string queryString = "SELECT TenDanhMuc, MoTa FROM Danhmuc";
      using (SqlCommand cmd = new SqlCommand (queryString, connection)) {
        // Mã sử dụng SqlCommand ở đây
        // Thiết lập tham số cmd cmd.Parameters nếu cần thiết
        // Thi hành lệnh  (như ) cmd.ExecuteReader(); ...
        using (SqlDataReader reader = cmd.ExecuteReader ()) {
          // Đoạn này đọc dữ liệu do SqlCommand trả về, đọc ở phần sau
          DataTable myTable = new DataTable ();
          myTable.Load (reader);
          foreach (DataRow dataRow in myTable.Rows) {
            foreach (var item in dataRow.ItemArray) {
              Console.Write ($"{item} ");
            }
            Console.WriteLine ();
          }

        }
      }
      // Đóng kết nối khi không còn dùng
      connection.Close ();

    }
  }
}