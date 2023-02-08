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
            var groupNumber = await context.Request.ReadFromJsonAsync<Value>();
            if (groupNumber is not null)
            {
                if (timeTableService.TryCacheGroup(groupNumber))
                {
                    context.Response.Cookies.Delete("value");
                    var options = new CookieOptions()
                    {
                        Expires = DateTimeOffset.MaxValue
                    };
                    context.Response.Cookies.Append("value", $"{groupNumber.LeftPart}-{groupNumber.RightPart}", options);
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