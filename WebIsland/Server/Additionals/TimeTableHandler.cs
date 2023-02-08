using System.Text.Json;
using WebIsland.Server.Logs;

namespace WebIsland;

public class TimeTableHandler
{
    private Dictionary<string, TimeTable> _tables = new ();
    private Logger _logger;

    public TimeTableHandler(Logger logger)
    {
        _logger = logger;
    }
    
    private TimeTable? ParseTimetable(Value value)
    {
        var clinet = new HttpClient();
        var options = new JsonSerializerOptions();
        if (value.RightPart.Contains("."))
        {
            var surname = Encoder.Decode(value.LeftPart);
            var otherName = Encoder.Decode(value.RightPart);
            options.Converters.Add(new TeacherParser($"{surname} {otherName}"));
        }
        else
        {
            options.Converters.Add(new TimeTableParser($"{value.LeftPart}-{value.RightPart}"));
        }
        TimeTable? result = null;
        try
        {
            result = clinet.GetFromJsonAsync<TimeTable>("https://forms.isuct.ru/timetable/rvuzov", options).Result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }

    private bool CheckGroupTimeTable(Value value)
    {
        return _tables.ContainsKey($"{value.LeftPart}-{value.RightPart}");
    }

    public TimeTable GetGroupTimeTable(string valueLeft, string valueRight)
    {
        return _tables[$"{valueLeft}-{valueRight}"];
    }

    public bool TryParse(Value value)
    {
        var result = false;
        if (!CheckGroupTimeTable(value))
        {
            var timeTable = ParseTimetable(value);
            if (timeTable is not null)
            {
                result = true;
                if (_tables.ContainsKey($"{value.LeftPart}-{value.RightPart}")) return result;
                _tables.Add($"{value.LeftPart}-{value.RightPart}", timeTable);
                _logger.LogParsedGroup($"{value.LeftPart}-{value.RightPart}");
            }
        }
        else
        {
            result = true;
        }

        return result;
    }

}