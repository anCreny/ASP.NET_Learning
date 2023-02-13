using System.Text.Json;

namespace WebIsland.Server.Services;

public class TeachersNamesHandler
{
    private List<string> _names;

    public TeachersNamesHandler()
    {
        try
        {
            var clinet = new HttpClient();
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CatchTeachersNames());
            var result = clinet.GetFromJsonAsync<List<string>>("https://forms.isuct.ru/timetable/rvuzov", options).Result;
            _names = result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<string> Names => _names;
}