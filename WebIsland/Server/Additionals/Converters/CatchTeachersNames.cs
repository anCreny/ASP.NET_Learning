using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebIsland;

public class CatchTeachersNames : JsonConverter<List<string>>
{
    public override List<string>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var names = new List<string>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "teachers")
            {
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        reader.Read();
                        var name = reader.GetString();
                        if (!names.Contains(name) && name != "—")
                        {
                            names.Add(name);
                        }
                    }
                }
            }
        }
        return names;
    }

    public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteStartArray("teachers");
        foreach (var name in value)
        {
            writer.WriteStartObject();
            writer.WriteString("name", name);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}