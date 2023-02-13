using System.Text.Json;
using WebIsland.Server.Services;

namespace WebIsland.Server.MiddleWare;

public class NamesMiddleWare
{
    public NamesMiddleWare(RequestDelegate _)
    {
        
    }

    public async Task Invoke(HttpContext context, TeachersNamesHandler teachersNamesHandler)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new CatchTeachersNames());
        await context.Response.WriteAsJsonAsync(teachersNamesHandler.Names, options);
    }
}