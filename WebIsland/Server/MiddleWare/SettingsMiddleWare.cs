namespace WebIsland.Server.MiddleWare;

public class SettingsMiddleWare
{
    public SettingsMiddleWare(RequestDelegate _)
    {
        
    }

    public async Task Invoke(HttpContext context)
    {
        await context.Response.SendFileAsync("HTML/Settings.html");
    }
}