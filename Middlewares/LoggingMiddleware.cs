using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            if (httpContext.Request != null)
            {
                string path = httpContext.Request.Path;
                string method = httpContext.Request.Method;
                string query = httpContext.Request.QueryString.ToString();
                string body = "";

                using (StreamReader reader = new StreamReader(httpContext.Request.Body, System.Text.Encoding.UTF8, true, 1024, true))
                {
                    body = await reader.ReadToEndAsync();
                    httpContext.Request.Body.Position = 0;
                };

                File.AppendAllText(Directory.GetCurrentDirectory()+"/requestsLog.txt", "["+System.DateTime.Now.ToString()+"]"+"\n"+method + "\n" + path + "\n" + query + "\n" + body+"\n");
            } 
            await _next(httpContext); 
            
        }
    }

}
