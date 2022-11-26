using System.Text.RegularExpressions;
using WebIsland;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

var handler = new TimeTableHandler();
var timetable = handler.ParseTimetable("3-42");
// var timetable = handler.GetTimetable();

Console.WriteLine("ЗАПРОС");

app.Map("/schedule", _ =>
{
    _.Map("/api", __ =>
    {
        __.UseTimeTableAPI(timetable);
    });
    _.UseTimeTable();
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
        __.UseTodayAPI(timetable);
    });
    _.UseCachingError();
    _.UseToday();
});

app.Run(async (c) =>
{
    Console.WriteLine("Запрос");
    await c.Response.SendFileAsync("HTML/index.html");
});

app.Run();







