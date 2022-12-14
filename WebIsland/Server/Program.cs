var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.Run(async (c) =>
{
    if (c.Request.Path == "/api")
    {
        var number = 0;
        try
        {
            Number? value = await c.Request.ReadFromJsonAsync<Number>();
            if (value is not null)
            {
                number = value.Value;
            }
        }
        catch
        {
        }
        
        c.Response.Cookies.Append("value", number.ToString());
    }
    else
    {
        if (c.Request.Cookies.TryGetValue("value", out var number))
        {
            await c.Response.WriteAsync(number);
        }
        else
        {
            await c.Response.SendFileAsync("HTML/index.html");
        }
    }
});

app.Run();


public record Number(int Value);




