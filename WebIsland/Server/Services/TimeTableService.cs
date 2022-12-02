namespace WebIsland.Server.Services;

public class TimeTableService
{
    private TimeTable _timeTable;
    private TimeTableHandler _handler;

    public TimeTableService(TimeTableHandler handler)
    {
        _handler = handler;
    }

    public bool SetGroup(string course, string groupNumber)
    {
        Console.WriteLine($"{course}-{groupNumber}");
        Console.WriteLine(!_handler.CheckGroupTimeTable(course, groupNumber));
        var confirm = true;
        if (!_handler.CheckGroupTimeTable(course, groupNumber))
        {
            confirm = false;
            if (_handler.ParseNewTimeTable(course, groupNumber))
            {
                return true;
            }
        }

        if (confirm)
        {
            _timeTable = _handler.GetGroupTimeTable(course, groupNumber);
        }

        return confirm;
    }
    
    public TimeTable TimeTable => _timeTable;
}