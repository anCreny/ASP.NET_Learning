namespace WebIsland;

public static class Encoder
{
    public static string Decode(string data)
    {
        var encodedSymbols = data.Split(".");
        var result = string.Empty;
        foreach (var encodedSymbol in encodedSymbols)
        {
            result += Convert.ToChar(Convert.ToInt32(encodedSymbol));
        }

        return result;
    }

    public static string Encode(string data)
    {
        var result = string.Empty;
        foreach (var symbol in data)
        {
            result += "." + (int)symbol;
        }

        return result[1..];
    }
}