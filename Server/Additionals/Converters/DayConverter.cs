using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebIsland;

public class DayConverter : JsonConverter<Day>
{
    public override Day? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Day value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("week", value.Week);
        writer.WriteNumber("weekday", value.WeekDay);
        writer.WriteStartArray("subjects");
        foreach (var subject in value.Subjects)
        {
            writer.WriteStartObject();
            
            writer.WriteString("name", subject.Name);
            
            writer.WriteString("type", subject.Type);
            
            writer.WriteStartObject("time");
            writer.WriteString("start", subject.TimeStart);
            writer.WriteString("end", subject.TimeEnd);
            writer.WriteEndObject();
            
            writer.WriteStartArray("audience");
            foreach (var audience in subject.Audience)
            {
                writer.WriteStartObject();
                writer.WriteString("name", audience);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            
            writer.WriteStartArray("teachers");
            foreach (var teacher in subject.Teachers)
            {
                writer.WriteStartObject();
                writer.WriteString("name", teacher);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}