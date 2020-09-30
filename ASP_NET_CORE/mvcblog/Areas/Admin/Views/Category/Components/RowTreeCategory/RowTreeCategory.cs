using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyViewComponent {
    
  [ViewComponent]
  public class RowTreeCategory : ViewComponent { 
    public RowTreeCategory () {

    } 
    // data là sữ liệu có cấu trúc
    // { 
    //    categories - danh sách các Category
    //    level - cấp của các Category 
    // }
    public IViewComponentResult Invoke (dynamic data) {
      return View(data);
    }
  }
}
