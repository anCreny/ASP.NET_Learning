using WebIsland.Server.Logs;
using WebIsland.Server.Services;

namespace WebIsland.Server.MiddleWare;

public class CookiePassMiddleWare
{
    private RequestDelegate _next;

    public CookiePassMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, TimeTableService timeTableService, Logger logger)
    {
        logger.LogNumberOfRequests();
        if (context.Request.Cookies.TryGetValue("value", out var number))
        {
            var value = number.Split("-");

            if (timeTableService.TryCacheGroup(new Value(value[0], value[1])))
            {
                timeTableService.SetGroup(value[0], value[1]);
                await _next.Invoke(context);
            }
            else
            {
                context.Response.Cookies.Delete("value");
                context.Response.Redirect("/", true);
            }
        }
        else
        {
            if (context.Request.Path == "/api")
            {
                try
                {
                    var gNumber = await context.Request.ReadFromJsonAsync<Value>();
                    if (gNumber is not null)
                    {
                        var groupNumber = gNumber.LeftPart + "-" + gNumber.RightPart;
                        if (timeTableService.TryCacheGroup(gNumber))
                        {
                            var options = new CookieOptions()
                            {
                                Expires = DateTimeOffset.MaxValue
                            };
                            context.Response.Cookies.Append("value", groupNumber, options);
                        }
                        else
                        {
                            context.Response.StatusCode = 11;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"CookiePass[64]:{e.Message}");
                }
            }
            else
            {
                await context.Response.SendFileAsync("HTML/welcome.html");
            }
        }
    }
}