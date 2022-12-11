namespace WebIsland.Server.Logs;

public class Counter
{
    private int _count;
    public int Count => _count;

    public void AddNumber(int number)
    {
        _count += number;
    }
}