using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor04.codebehide.Models;
using static System.Console;

namespace razor04.codebehide.Pages {
  // Lớp là Model của Razor, nên phải kế thừa PageModel
  public class ViewProductModel : PageModel {
    // Khai báo thuộc tính  

    public Product product;
    // Handler chạy khi truy cập trang bằng phương thức get
    public IActionResult OnGet (int id) {
      int? ID = null;
      if (Request.RouteValues["id"] != null) {
        ID = Int32.Parse (Request.RouteValues["id"].ToString ());
        product = ProductContext.FindProductByID (ID.Value);
        if (product == null) {
          // Khônng thấy chuyển hướng về trang /product/
          return RedirectToPage("ViewProduct", new { id = ""});
        }
        
      }
      // Trả về kết quả trang Razor
      return Page();

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