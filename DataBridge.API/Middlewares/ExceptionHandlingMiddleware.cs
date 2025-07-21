using DataBridge.API.Brokers.Loggings;

namespace DataBridge.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILoggingBroker logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILoggingBroker logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal server error occurred.");
        }
    }
}
