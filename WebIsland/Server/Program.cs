using System.Text.RegularExpressions;
using WebIsland;
using WebIsland.Server.Services;

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<TimeTableHandler>();
builder.Services.AddScoped<TimeTableService>();

var app = builder.Build();

app.UseCookieChecker();

app.Map("/settings/api", _ =>
{
    _.UseSettingsAPI();
});

app.Map("/schedule", _ =>
{
    _.Map("/api", __ =>
    {
        __.UseTimeTableAPI();
    })
        .UseTimeTable();
});

app.Map("/test", _ =>
{
    _.UseTest();
});

app.Map("/today", _ =>
{
    _.MapWhen(
        context => Regex.IsMatch(context.Request.Path, "^/api/([(-9)-9]+)$"), __ =>
    {
        __.UseTodayAPI();
    })
    .UseCachingError()
    .UseToday();
});

app.Run(async (c) =>
{
    var isMobile = false;
    foreach (var value in c.Request.Headers["User-Agent"])
    {
        isMobile = value.Contains("Mobile");
    }

    if (isMobile)
    {
        await c.Response.SendFileAsync("HTML/MobileMain.html");
    }
    else
    {
        await c.Response.SendFileAsync("HTML/index.html");
    }
    Console.WriteLine(isMobile ? "MOBILE" : "PC");
});

app.Run();







