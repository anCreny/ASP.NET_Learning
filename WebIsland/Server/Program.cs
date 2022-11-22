var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Run(async (c) => await c.Response.SendFileAsync("HTML/index.html"));

app.Run();







