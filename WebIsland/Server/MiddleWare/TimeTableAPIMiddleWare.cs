using System.Text.Json;

namespace WebIsland;

public class TimeTableAPIMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly TimeTable _timeTable;

    public TimeTableAPIMiddleWare(RequestDelegate next, TimeTable timeTable)
    {
        _next = next;
        _timeTable = timeTable;
    }

    public async Task Invoke(HttpContext context)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new TimeTableParser(string.Empty));
        await context.Response.WriteAsJsonAsync(_timeTable, options);
    }

}