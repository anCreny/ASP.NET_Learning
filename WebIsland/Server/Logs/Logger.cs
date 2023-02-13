namespace WebIsland.Server.Logs;

public class Logger
{
    private string _counterPath;
    private string _parserPath;
    private Counter _counter;

    public Logger(string counterPath, string parserPath, Counter counter)
    {
        _counterPath = counterPath;
        _parserPath = parserPath;
        _counter = counter;
        File.WriteAllTextAsync(_parserPath, "Parsed groups:\n");
    }

    public async void LogNumberOfRequests()
    {
        _counter.AddNumber(1);
        await File.WriteAllTextAsync(_counterPath, $"Number of requests to the server: {_counter.Count}");
    }

    public async void LogParsedGroup(string groupNumber)
    {
        await File.AppendAllTextAsync(_parserPath, $"[{DateTime.Now.Hour}:{ (DateTime.Now.Minute < 10 ? $"0{DateTime.Now.Minute}" : DateTime.Now.Minute)}]: {groupNumber}\n");
    }
}