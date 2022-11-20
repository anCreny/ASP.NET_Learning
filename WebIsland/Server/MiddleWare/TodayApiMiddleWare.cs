using System.Text.Json;

namespace WebIsland;

public class TodayApiMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly TimeTable _timeTable;

    public TodayApiMiddleWare(RequestDelegate next, TimeTable timeTable)
    {
        _next = next;
        _timeTable = timeTable;
    }

    public async Task Invoke(HttpContext context)
    {
        var dayOffset = Convert.ToInt32(context.Request.Path.ToString().Split("/")[2]);
        var today = DateTime.Today;
        today = today.AddDays(Convert.ToInt32(dayOffset));
        var currentDay = _timeTable.GetDay(today);

        var options = new JsonSerializerOptions();
        options.Converters.Add(new DayConverter());
            
        await context.Response.WriteAsJsonAsync(currentDay, options);
    }
}