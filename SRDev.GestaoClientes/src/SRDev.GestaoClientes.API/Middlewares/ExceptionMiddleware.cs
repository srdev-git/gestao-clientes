using Microsoft.AspNetCore.Http;
using SRDev.GestaoClientes.Domain.Exceptions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = ProblemDetailsFactory.CreateProblemDetails(context, exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problemDetails.Status;

            var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return context.Response.WriteAsync(json);
        }
    }
}
