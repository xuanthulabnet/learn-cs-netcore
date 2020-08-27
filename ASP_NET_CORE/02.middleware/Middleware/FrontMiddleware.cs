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
  public class FrontMiddleware : IMiddleware
  {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Console.Clear();
        Console.WriteLine("FrontMiddleware: " + context.Request.Path);
        context.Items.Add("dulieu1", "Data Object ...");
        
        await next(context);
    }
  }
}

