using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebIsland;

public class GroupStatusChecker : JsonConverter<GroupStatus>
{
    private string _groupNumber; 
        
    public GroupStatusChecker(string groupNumber)
    {
        _groupNumber = groupNumber;
    }
    
    public override GroupStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var status = false;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();
                if (propertyName == "name" && reader.GetString() == _groupNumber)
                {
                    status = true;
                }
            }
        }

        return new GroupStatus(status);
    }

    public override void Write(Utf8JsonWriter writer, GroupStatus value, JsonSerializerOptions options)
    {
    }
}