using System.Text.Json;

namespace WebIsland;

public class TimeTableHandler
{
    private TimeTable? _table;

    public TimeTable? ParseTimetable(string groupNumber)
    {
        var clinet = new HttpClient();
        var options = new JsonSerializerOptions();
        options.Converters.Add(new TimeTableParser("3-42"));
        TimeTable? result = null;
        try
        {
            result = clinet.GetFromJsonAsync<TimeTable>("https://forms.isuct.ru/timetable/rvuzov", options).Result;
            _table = result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }

    public TimeTable? GetTimetable()
    {
        return _table;
    }
}