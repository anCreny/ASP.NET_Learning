using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebIsland;

public class TimeTableParser : JsonConverter<TimeTable>
{
    private string _group;

    public TimeTableParser(string group)
    {
        _group = group;
    }
    
    public override TimeTable? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var timeTable = new TimeTable();
        var subject = string.Empty;
        var type = string.Empty;
        var timeStart = string.Empty;
        var timeEnd = string.Empty;
        var lessonStart = string.Empty;
        var lessonEnd = string.Empty;
        var weekday = 0;
        var week = 0;
        var audience = new List<string>();
        var teachers = new List<string>();
        while (reader.Read())
        {
            if (reader.TokenType != JsonTokenType.PropertyName) continue;
            
            var propertyName = reader.GetString();
            reader.Read();
            
            if (propertyName != "name")
            {
                continue;
            }

            if (reader.TokenType != JsonTokenType.String || reader.GetString() != _group) continue;
            reader.Read();
            
            if (reader.TokenType != JsonTokenType.PropertyName || reader.GetString() != "lessons") continue;
            reader.Read();
            
            if (reader.TokenType != JsonTokenType.StartArray) continue;
            var counter = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                counter++;
                Console.WriteLine(counter);
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                    {
                        if (reader.TokenType == JsonTokenType.PropertyName)
                        {
                            var lessonPropertyName = reader.GetString();
                            reader.Read();
                            switch (lessonPropertyName?.ToLower())
                            {
                                case "subject":
                                    subject = reader.GetString();
                                    break;
                                case "type":
                                    type = reader.GetString();
                                    break;
                                case "time":
                                    while (reader.Read() &&
                                           reader.TokenType != JsonTokenType.EndObject)
                                    {
                                        if (reader.TokenType == JsonTokenType.PropertyName)
                                        {
                                            var timePropertyName = reader.GetString();
                                            reader.Read();
                                            switch (timePropertyName?.ToLower())
                                            {
                                                case "start":
                                                    timeStart = reader.GetString();
                                                    break;
                                                case "end":
                                                    timeEnd = reader.GetString();
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                case "date":
                                    while (reader.Read() &&
                                           reader.TokenType != JsonTokenType.EndObject)
                                    {
                                        if (reader.TokenType == JsonTokenType.PropertyName)
                                        {
                                            var datePropertyName = reader.GetString();
                                            reader.Read();
                                            switch (datePropertyName?.ToLower())
                                            {
                                                case "start":
                                                    lessonStart = reader.GetString();
                                                    break;
                                                case "end":
                                                    lessonEnd = reader.GetString();
                                                    break;
                                                case "weekday":
                                                    weekday = reader.GetInt32();
                                                    break;
                                                case "week":
                                                    week = 3 - reader.GetInt32();
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                case "audiences":
                                    while (reader.Read() &&
                                           reader.TokenType != JsonTokenType.EndArray)
                                    {
                                        if (reader.TokenType == JsonTokenType.PropertyName)
                                        {
                                            reader.Read();
                                            audience.Add(reader.GetString());
                                        }
                                    }
                                    break;
                                case "teachers":
                                    while (reader.Read() &&
                                           reader.TokenType != JsonTokenType.EndArray)
                                    {
                                        if (reader.TokenType == JsonTokenType.PropertyName)
                                        {
                                            reader.Read();
                                            teachers.Add(reader.GetString());
                                        }
                                    }
                                    break;
                            }
                        }
                    }

                }
                
                timeTable.AddSubject(new Subject(subject, type, timeStart, timeEnd, lessonStart, lessonEnd, weekday, week, audience, teachers));
                audience = new List<string>();
                teachers = new List<string>();
            }
        }

        return timeTable;
    }

    public override void Write(Utf8JsonWriter writer, TimeTable value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteStartArray("days");
        foreach (var day in value.Get)
        {
            writer.WriteStartObject();
            writer.WriteNumber("week", day.Week);
            writer.WriteNumber("weekday", day.WeekDay);
            writer.WriteStartArray("lessons");
            foreach (var subject in day.Subjects)
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
                    writer.WriteString("audience", audience);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteStartArray("teachers");
                foreach (var teacher in subject.Teachers)
                {
                    writer.WriteStartObject();
                    writer.WriteString("teacher", teacher);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteEndObject();
                
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
    
}