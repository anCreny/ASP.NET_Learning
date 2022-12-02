using System.Text.Json;
using WebIsland.Server.Services;

namespace WebIsland;

public class TodayApiMiddleWare
{
    private readonly RequestDelegate _next;
    public TodayApiMiddleWare(RequestDelegate next)
    {
        _next = next; 
    }

    public async Task Invoke(HttpContext context, TimeTableService timeTableService)
    {
        var dayOffset = Convert.ToInt32(context.Request.Path.ToString().Split("/")[2]);
        var today = DateTime.Today;
        today = today.AddDays(Convert.ToInt32(dayOffset));
        var currentDay = timeTableService.TimeTable.GetDay(today);

        var options = new JsonSerializerOptions();
        options.Converters.Add(new DayConverter());
            
        await context.Response.WriteAsJsonAsync(currentDay, options);
    }
}