using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _02.middleware {
  public class CheckAcessMiddleware {
    // Lưu middlewware tiếp theo trong Pipeline
    private readonly RequestDelegate _next;
    public CheckAcessMiddleware (RequestDelegate next) => _next = next;
    public async Task Invoke (HttpContext httpContext) {
      if (httpContext.Request.Path == "/testxxx") {

        Console.WriteLine ("CheckAcessMiddleware: Cấm truy cập");
        await Task.Run (
          async () => {
            string html = "<h1>CAM KHONG DUOC TRUY CAP</h1>";
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await httpContext.Response.WriteAsync (html);
          }
        );
        
      } else {

        // Thiết lập Header cho HttpResponse
        httpContext.Response.Headers.Add ("throughCheckAcessMiddleware", new [] { DateTime.Now.ToString () });

        Console.WriteLine ("CheckAcessMiddleware: Cho truy cập");

        // Chuyển Middleware tiếp theo trong pipeline
        await _next (httpContext);
        
      }

    }
  }

}