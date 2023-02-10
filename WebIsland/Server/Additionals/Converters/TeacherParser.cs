using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebIsland;

public class TeacherParser : JsonConverter<TimeTable>
{
    private string _teacherName;

    public TeacherParser(string teacherName)
    {
        _teacherName = teacherName;
    }
    
    public override TimeTable? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var groupsNumber = new List<string>();
        var timeTable = new TimeTable();
        var subject = string.Empty;
        var type = string.Empty;
        var timeStart = string.Empty;
        var timeEnd = string.Empty;
        var weekday = 0;
        var week = 0;
        var audience = new List<string>();
        var accept = false;
        var wasCollision = false;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "name")
            {
                reader.Read();
                groupsNumber.Add(reader.GetString());
            }
            if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "lessons")
            {
                reader.Read();
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    if (reader.TokenType == JsonTokenType.StartObject)
                    {
                        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                        {

                            if (reader.TokenType == JsonTokenType.PropertyName)
                            {
                                var propertyName = reader.GetString();
                                reader.Read();
                                switch (propertyName?.ToLower())
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
                                                if (reader.GetString() == _teacherName)
                                                {
                                                    accept = true;
                                                    wasCollision = true;
                                                }
                                            }
                                        }

                                        break;
                                }
                            }
                        }
                    }

                    if (accept)
                    {
                        timeTable.AddSubject(new Subject(subject, type, timeStart, timeEnd, string.Empty, string.Empty, weekday, week, audience, groupsNumber));
                    }
                    accept = false;
                    audience = new List<string>();
                    

                }
                groupsNumber = new List<string>();
            }
        }
        return !wasCollision ? null : timeTable;
    }

    public override void Write(Utf8JsonWriter writer, TimeTable value, JsonSerializerOptions options)
    {
    }
}