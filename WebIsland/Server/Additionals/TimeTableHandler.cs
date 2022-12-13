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
    
    private TimeTable? ParseTimetable(GroupNumber groupNumber)
    {
        var clinet = new HttpClient();
        var options = new JsonSerializerOptions();
        options.Converters.Add(new TimeTableParser($"{groupNumber.Course}-{groupNumber.Number}"));
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

    private bool CheckGroupTimeTable(GroupNumber groupNumber)
    {
        return _tables.ContainsKey($"{groupNumber.Course}-{groupNumber.Number}");
    }

    public TimeTable GetGroupTimeTable(string course, string groupNumber)
    {
        return _tables[$"{course}-{groupNumber}"];
    }

    public bool TryParse(GroupNumber groupNumber)
    {
        var result = false;
        if (!CheckGroupTimeTable(groupNumber))
        {
            var timeTable = ParseTimetable(groupNumber);
            if (timeTable is not null)
            {
                result = true;
                if (_tables.ContainsKey($"{groupNumber.Course}-{groupNumber.Number}")) return result;
                _tables.Add($"{groupNumber.Course}-{groupNumber.Number}", timeTable);
                _logger.LogParsedGroup($"{groupNumber.Course}-{groupNumber.Number}");
            }
        }
        else
        {
            result = true;
        }

        return result;
    }

}