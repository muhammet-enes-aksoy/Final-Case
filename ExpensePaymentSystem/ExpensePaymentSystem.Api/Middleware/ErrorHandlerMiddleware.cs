using System.Data.Common;
using System.Net;
using System.Text.Json;
using ExpensePaymentSystem.Business.Services;
using Serilog;

namespace ExpensePaymentSystem.Api.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly IServiceProvider _serviceProvider;

    public ErrorHandlerMiddleware(RequestDelegate next, IServiceProvider _serviceProvider)
    {
        this.next = next;
        this._serviceProvider = _serviceProvider;
    }


    public async Task Invoke(HttpContext context, IServiceProvider IServiceProvider)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            Log.Error(exception,"UnexpectedError");
            Log.Fatal(                        
                $"Path={context.Request.Path} || " +                      
                $"Method={context.Request.Method} || " +
                $"Exception={exception.Message}"
            );
            
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize("Internal error!"));
        }
    }
}