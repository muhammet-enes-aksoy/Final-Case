using System.Net;
using System.Text.Json;
using ExpensePaymentSystem.Business.Services;

namespace ExpensePaymentSystem.Api.Middleware;

public class HeartBeatMiddleware
{
    private readonly RequestDelegate next;
    private readonly IServiceProvider _serviceProvider;

    public HeartBeatMiddleware(RequestDelegate next, IServiceProvider _serviceProvider)
    {
        this.next = next;
        this._serviceProvider = _serviceProvider;
    }


    public async Task Invoke(HttpContext context, IServiceProvider IServiceProvider)
    {
        if (context.Request.Path.StartsWithSegments("/hello"))
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsync(JsonSerializer.Serialize("Hello from server"));
            return;
        }

        await next.Invoke(context);
    }
}