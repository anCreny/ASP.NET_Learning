namespace WebIsland;

public class MainMiddleWare
{
    private readonly RequestDelegate _next;

    public MainMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await context.Response.SendFileAsync("HTML/index.html");
    }
}