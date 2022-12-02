namespace WebIsland.Server.MiddleWare;

public class GroupNumberAPIMiddleWare
{
    public GroupNumberAPIMiddleWare(RequestDelegate _)
    {
    }

    public async Task Invoke(HttpContext context)
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
}