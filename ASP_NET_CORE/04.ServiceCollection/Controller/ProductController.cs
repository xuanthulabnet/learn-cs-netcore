using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Linq;

namespace _04.ServiceCollection
{
    public class ProductController
    {
        IListProductName lsPhone;
        IListProductName lsLaptop;

        // Inject hai dịch vụ qua phương thức khởi tạo
        public ProductController(IListProductName lsphone, LaptopName lslaptop) {
            Console.WriteLine(this.GetType().Name + " created");
            this.lsPhone  = lsphone;
            this.lsLaptop = lslaptop;
        }

        // Xuất danh sách sản phẩm cho Response
        public async Task List(HttpContext context) {

            var sb = new StringBuilder();
            string lsPhoneHTML  = string.Join("", lsPhone.GetNames().Select(name  => name.HtmlTag("li"))).HtmlTag("ul");
            string lsLaptopHTML = string.Join("", lsLaptop.GetNames().Select(name => name.HtmlTag("li"))).HtmlTag("ul");
            sb.Append("Danh sách điện thoại".HtmlTag("h2"));
            sb.Append(lsPhoneHTML);

            sb.Append("Danh sách Laptop".HtmlTag("h2"));
            sb.Append(lsLaptopHTML);

            string menu         = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
            string html         = HtmlHelper.HtmlDocument("DS Sản phẩm", menu + sb.ToString().HtmlTag("div", "container"));

            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(html);
        }
    }
}
