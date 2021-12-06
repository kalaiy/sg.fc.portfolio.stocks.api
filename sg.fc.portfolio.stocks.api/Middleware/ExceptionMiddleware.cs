using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Sg.Fc.Portfolio.Stocks.Api.Middleware
{
    public class ExceptionMiddleware
    {
       
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
           
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
               
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
          
            var exceptionType = exception.GetType();
            //context.Response.StatusCode
            //switch (exception)
            //{
            //    case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                   
            //        break;
              
            //}

            return context.Response.WriteAsync(JsonConvert.SerializeObject(exception.Message));
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}