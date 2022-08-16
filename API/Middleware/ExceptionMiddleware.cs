using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;// whats coming up next in middleware pipeline i.e. passes control to other middleware
        private readonly ILogger<ExceptionMiddleware> _log; // logs erros etc to console.
        private readonly IHostEnvironment _env; // to check if we are in production or development environment
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _log = logger;
            _env = env;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // we use httpconte4xt because this error is in the context of a http request
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                context.Response.ContentType = ("application/json");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // cast status code to int instead of string

                var r = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, e.Message, e.StackTrace?.ToString()) // give us a detailed exception of the error            
                    : new ApiException(context.Response.StatusCode, "internal Server Error");//if we are in production mode.give us a not so detailed exception of the error





                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; // response will be in camal case
                var json = JsonSerializer.Serialize(r, options);
                await context.Response.WriteAsync(json);

            }
        }
    }
}