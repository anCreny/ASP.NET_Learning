namespace WebIsland;

public class TimeTableMiddleWare
{
    private readonly RequestDelegate _next;

    public TimeTableMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await context.Response.SendFileAsync("HTML/timetable.html");
    }
}