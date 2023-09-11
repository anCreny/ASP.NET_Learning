using WebIsland;
using WebIsland.Server.Logs;
using WebIsland.Server.Services;

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton(new Logger("Server/Logs/numberOfRequestsLogs.txt", "Server/Logs/parsedGroupsLogs.txt", new Counter()));
builder.Services.AddSingleton<TimeTableHandler>();
builder.Services.AddScoped<TimeTableService>();
builder.Services.AddSingleton(new TeachersNamesHandler());
var app = builder.Build();


app.UseStaticFiles();
app.UseCookieChecker();

app.Map("/names/api", _ =>
{
    _.UseNamesCatching();
});

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
    _.Map(
        "/api", __ =>
    {
        __.UseTodayAPI();
    })
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
});

app.Run();







