using System.Text.Json;

namespace WebIsland;

public class TimeTableHandler
{
    private Dictionary<string, TimeTable> _tables = new Dictionary<string, TimeTable>();
    
    private TimeTable? ParseTimetable(string groupNumber)
    {
        var clinet = new HttpClient();
        var options = new JsonSerializerOptions();
        options.Converters.Add(new TimeTableParser(groupNumber));
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

    public bool CheckGroupTimeTable(string course, string groupNumber)
    {
        return _tables.ContainsKey($"{course}-{groupNumber}");
    }

    public TimeTable? GetGroupTimeTable(string course, string groupNumber)
    {
        return _tables[$"{course}-{groupNumber}"];
    }

    public bool ParseNewTimeTable(string course, string groupNumber)
    {
        var table = ParseTimetable($"{course}-{groupNumber}");
        if (table is not null)
        {
            _tables.Add($"{course}-{groupNumber}", table);
            return true;
        }

        return false;
    }

}