namespace WebIsland;

public class CachingErrorMiddleWare
{
    private readonly RequestDelegate _next;

    public CachingErrorMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;
        
        if (request.Query.TryGetValue("day", out var day) && (day.ToString().All(Char.IsDigit) || (day.ToString()[0] == '-' && day.ToString().Substring(1).All(Char.IsDigit) )))
        {
            await _next.Invoke(context);
        }
        else
        {
            await response.SendFileAsync("HTML/TodayError.html");
        }
    }
}