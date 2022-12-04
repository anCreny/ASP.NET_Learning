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
            var groupNumber = number.Split("-");

            if (timeTableService.TryCacheGroup(new GroupNumber(groupNumber[0], groupNumber[1])))
            {
                timeTableService.SetGroup(groupNumber[0], groupNumber[1]);
                await _next.Invoke(context);
            }
            else
            {
                context.Response.Cookies.Delete("number");
                context.Response.Redirect("/", true);
            }
        }
        else
        {
            if (context.Request.Path == "/api")
            {
                try
                {
                    var gNumber = await context.Request.ReadFromJsonAsync<GroupNumber>();
                    if (gNumber is not null)
                    {
                        var groupNumber = gNumber.Course + "-" + gNumber.Number;
                        if (timeTableService.TryCacheGroup(gNumber))
                        {
                            context.Response.Cookies.Append("number", groupNumber);
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
            else
            {
                await context.Response.SendFileAsync("HTML/groupChoosing.html");
            }
        }
    }
}