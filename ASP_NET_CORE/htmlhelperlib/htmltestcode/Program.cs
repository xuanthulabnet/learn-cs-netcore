using System;
using XTLAB.HtmlHelper;

namespace htmltestcode
{
    class Program
    {
        static void Main(string[] args)
        {
            String html = "Ví dụ sử dụng HtmlHelper".HtmlTag("div", "text-danger");
            Console.WriteLine(html);
        }
    }
}
