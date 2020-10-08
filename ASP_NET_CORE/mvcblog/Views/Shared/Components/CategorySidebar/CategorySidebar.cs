using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvcblog.Models;

namespace XTLASPNET
{
        [ViewComponent]
        public class CategorySidebar : ViewComponent
        {
            public class CategorySidebarData {
                public List<Category> categories {set;get;}
                public int level {set; get;}
                public string slugCategory {set; get;}
            }
            public const string COMPONENTNAME = "CategorySidebar";
            
            public CategorySidebar() {}
            public IViewComponentResult Invoke(CategorySidebarData data) {
                return View(data);
            }
        }
}