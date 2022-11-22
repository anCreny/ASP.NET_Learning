namespace WebIsland.MiddleWare;

public class TodayMiddleWare
{
    private readonly RequestDelegate _next;
    
    public TodayMiddleWare(RequestDelegate next)
    {
        _next = next; 
    }

    public async Task Invoke(HttpContext context)
    {
        await context.Response.SendFileAsync("HTML/today.html");
    }
}