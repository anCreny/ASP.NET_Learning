namespace WebIsland.Server.MiddleWare;

public class SettingsAPIMiddleWare
{
    public SettingsAPIMiddleWare(RequestDelegate _)
    {
        
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            var groupNumber = await context.Request.ReadFromJsonAsync<GroupNumber>();
            if (groupNumber is not null)
            {
                context.Response.Cookies.Delete("number");
                context.Response.Cookies.Append("number", $"{groupNumber.Course}-{groupNumber.Number}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}