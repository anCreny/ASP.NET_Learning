using System.Text.RegularExpressions;
using WebIsland;
using WebIsland.Server.Services;

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<TimeTableHandler>();
builder.Services.AddScoped<TimeTableService>();

var app = builder.Build();

app.UseCookieChecker();

app.Map("/settings", _ =>
{
    _.Map("/api", __ =>
        {
            __.UseSettingAPI();
        })
        .UseSettings();
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
    await c.Response.SendFileAsync("HTML/index.html");
});

app.Run();







