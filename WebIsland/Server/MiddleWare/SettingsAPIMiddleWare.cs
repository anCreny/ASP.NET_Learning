using WebIsland.Server.Services;

namespace WebIsland.Server.MiddleWare;

public class SettingsAPIMiddleWare
{
    public SettingsAPIMiddleWare(RequestDelegate _)
    {
        
    }

    public async Task Invoke(HttpContext context, TimeTableService timeTableService)
    {
        try
        {
            var groupNumber = await context.Request.ReadFromJsonAsync<GroupNumber>();
            if (groupNumber is not null)
            {
                if (timeTableService.TryCacheGroup(groupNumber))
                {
                    context.Response.Cookies.Delete("number");
                    context.Response.Cookies.Append("number", $"{groupNumber.Course}-{groupNumber.Number}");
                }
                else
                {
                    context.Response.StatusCode = 11;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}