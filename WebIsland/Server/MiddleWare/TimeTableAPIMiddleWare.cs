using System.Text.Json;
using WebIsland.Server.Services;

namespace WebIsland;

public class TimeTableAPIMiddleWare
{
    private readonly RequestDelegate _next;
    public TimeTableAPIMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, TimeTableService timeTableService)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new TimeTableParser(string.Empty));
        await context.Response.WriteAsJsonAsync(timeTableService.TimeTable, options);
    }

}