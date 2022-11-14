using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebIsland;

public class MCSerialiser : JsonConverter<MathematicalCalculations>
{
    public override MathematicalCalculations? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = 0;
        var firstNumber = 0;
        var secondNumber = 0;
        var sign = 0;
        var answer = 0;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString(); 
                reader.Read();
                switch (propertyName?.ToLower())
                {
                    case "id":
                        id = reader.GetInt32();
                        break;
                    case "firstnumber":
                        firstNumber = reader.GetInt32();
                        break;
                    case "secondnumber":
                        secondNumber = reader.GetInt32();
                        break;
                    case "sign":
                        sign = reader.GetInt32();
                        break;
                    case "answer":
                        answer = reader.GetInt32();
                        break;
                }
            }
        }

        return new MathematicalCalculations(id, firstNumber, secondNumber, sign, answer);
    }

    public override void Write(Utf8JsonWriter writer, MathematicalCalculations value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteNumber("firstNumber", value.FirstNumber);
        writer.WriteNumber("secondNumber", value.SecondNumber);
        writer.WriteNumber("sign", value.Sign);
        writer.WriteNumber("answer", value.Answer);
        writer.WriteEndObject();
    }
}