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

    public bool TryCacheGroup(GroupNumber groupNumber)
    {
        return _handler.TryParse(groupNumber);
    }
    
    public TimeTable TimeTable => _timeTable;
}