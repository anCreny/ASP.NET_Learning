using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebIsland;

public class TestMiddleWare
{
    private readonly RequestDelegate _next;

    public TestMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path == "/api")
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new TestConverter());
            await context.Response.WriteAsJsonAsync(new Test(), options);
        }
        else
        {
            await context.Response.SendFileAsync("HTML/test.html");
        }
    }

}

public class Test
{
    public List<string> TestArray { get; } = new();

    public Test()
    {
        TestArray.Add("Hello");
        TestArray.Add("Goodbye");
        TestArray.Add("smth");
    }
}

public class TestConverter : JsonConverter<Test>
{
    public override Test? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Test value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteStartArray("strings");
        foreach (var item in value.TestArray)
        {
            writer.WriteStartObject();
            writer.WriteString("item", item);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}