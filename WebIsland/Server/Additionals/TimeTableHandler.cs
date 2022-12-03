using System.Text.Json;

namespace WebIsland;

public class TimeTableHandler
{
    private Dictionary<string, TimeTable> _tables = new ();
    
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
                _tables.Add($"{groupNumber.Course}-{groupNumber.Number}", timeTable);
            }
        }
        else
        {
            result = true;
        }

        return result;
    }

}