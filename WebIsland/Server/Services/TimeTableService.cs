namespace WebIsland.Server.Services;

public class TimeTableService
{
    private TimeTable _timeTable;
    private TimeTableHandler _handler;

    public TimeTableService(TimeTableHandler handler)
    {
        _handler = handler;
    }

    public void SetGroup(string course, string groupNumber)
    {
        _timeTable = _handler.GetGroupTimeTable(course, groupNumber);
    }

    public bool TryCacheGroup(Value value)
    {
        return _handler.TryParse(value);
    }
    
    public TimeTable TimeTable => _timeTable;
}