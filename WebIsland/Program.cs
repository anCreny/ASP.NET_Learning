using System.Text.Json;
using System.Text.RegularExpressions;
using WebIsland;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var handler = new MCHandler();
app.Run(FirstMiddleWare);

app.Run();

async Task FirstMiddleWare(HttpContext httpContext)
{
    var response = httpContext.Response;
    var request = httpContext.Request;
    var path = request.Path;

    string expressionForNumber = @"^/api/notes/([0-9]+)$";

    if (path == "/api/notes" && request.Method == "GET")
    {
        await handler.GetAllMC(response);
    }
    else if (Regex.IsMatch(path, expressionForNumber) && request.Method == "DELETE")
    {
        var status = int.TryParse(path.Value?.Split("/")[3], out var id);
        Console.WriteLine(status);
        if (status)
        {
            await handler.DeleteNote(id, response);
        }
    }
    else if (path == "/api/notes" && request.Method == "POST")
    {
        await handler.CreateNewNote(request, response);
    }
    else if (path == "/api/calculate")
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new MCSerialiser());
        MathematicalCalculations? operation = null;
        try
        {
            operation = await request.ReadFromJsonAsync<MathematicalCalculations>(options);
            if (operation is not null)
            {
                switch (operation.Sign)
                {
                    case 1:
                        operation.Answer = operation.FirstNumber + operation.SecondNumber;
                        break;
                    case 2:
                        operation.Answer = operation.SecondNumber - operation.FirstNumber;
                        break;
                }
                
            }
        }
        catch { }
        await response.WriteAsJsonAsync(operation, options);
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("HTML/index.html");
    }
}