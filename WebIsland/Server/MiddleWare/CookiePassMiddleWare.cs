using WebIsland.Server.Services;

namespace WebIsland.Server.MiddleWare;

public class CookiePassMiddleWare
{
    private RequestDelegate _next;

    public CookiePassMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, TimeTableService timeTableService)
    {
        if (context.Request.Cookies.TryGetValue("number", out var number))
        {
            if (timeTableService.SetGroup(number.Split("-")[0], number.Split("-")[1]))
            {
                await _next.Invoke(context);
            }
        }
        else
        {
            if (context.Request.Path == "/api")
            {
                var groupNumber = string.Empty;
                try
                {
                    var gNumber = await context.Request.ReadFromJsonAsync<GroupNumber>();
                    if (gNumber is not null)
                    {
                        groupNumber = gNumber.Course + "-" + gNumber.Number;
                        context.Response.Cookies.Append("number", groupNumber); 
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
            await context.Response.SendFileAsync("HTML/groupChoosing.html");
        }
    }
}