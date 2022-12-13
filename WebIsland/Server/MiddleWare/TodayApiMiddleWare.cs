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
        var offset = 0;
        try
        {
            var dayOffset = await context.Request.ReadFromJsonAsync<DayOffset>();
            if (dayOffset is not null)
            {
                offset = dayOffset.Offset;
                Console.WriteLine(offset);
            }
            else
            {
                Console.WriteLine("null");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        var today = DateTime.Today;
        today = today.AddDays(offset);
        var currentDay = timeTableService.TimeTable.GetDay(today);

        var options = new JsonSerializerOptions();
        options.Converters.Add(new DayConverter());
            
        await context.Response.WriteAsJsonAsync(currentDay, options);
    }
    private record DayOffset(int Offset);
}

