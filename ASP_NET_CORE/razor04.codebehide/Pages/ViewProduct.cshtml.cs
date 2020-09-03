using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor04.codebehide.Models;
using static System.Console;

namespace razor04.codebehide.Pages {
  // Lớp là Model của Razor, nên phải kế thừa PageModel
  public class ViewProductModel : PageModel {
    // Khai báo thuộc tính  
    public Product product;
    // Handler chạy khi truy cập trang bằng phương thức get
    public void OnGet (int id) {
      Console.WriteLine(id);
      int? ID = null;
      if (Request.RouteValues["id"] != null) {
        ID = Int32.Parse (Request.RouteValues["id"].ToString ());
        product = ProductContext.FindProductByID (ID.Value);
      }
    }

    public String Thongbao;

    // Chạy truy cập post tới, url = /sanpham/2?handler=xoa
    public void OnPostXoa(int i)
    {
        Thongbao = "Gọi OnPostXoa";
    }
    // Chạy truy cập post tới, url = /sanpham/2?handler=soanthao
    public void OnPostSoanthao(int id)
    {
        Thongbao = "Gọi OnPostSoanthao";
    }
    // Chạy truy cập post tới, url = /sanpham/2?handler=xemchitiet
    public void OnPostXemchitiet(int id)
    {
        Thongbao = "Gọi OnPostXemchitiet";
    }

  }
}