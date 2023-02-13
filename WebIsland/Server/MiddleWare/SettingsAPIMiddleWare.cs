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
            var value = await context.Request.ReadFromJsonAsync<Value>();
            if (value is not null)
            {
                if (timeTableService.TryCacheGroup(value))
                {
                    context.Response.Cookies.Delete("value");
                    var options = new CookieOptions()
                    {
                        Expires = DateTimeOffset.MaxValue
                    };
                    context.Response.Cookies.Append("value", $"{value.LeftPart}-{value.RightPart}", options);
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