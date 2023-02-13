namespace WebIsland;

public class Subject
{
    private readonly int _week;
    private readonly int _weekDay;
    
    public Subject(string subject, string type, string timeStart, string timeEnd, string lessonStart, string lessonEnd, int weekDay, int week, List<string> audience, List<string> teachers)
    {
        Name = subject;
        Type = type;
        TimeEnd = timeEnd;
        TimeStart = timeStart;
        LessonStart = lessonStart;
        LessonEnd = lessonEnd;
        Week = week;
        WeekDay = weekDay;
        Audience = new List<string>(audience);
        Teachers = new List<string>(teachers);
    }

    public string Name { get; init; }
    public string Type { get; init; }
    public string TimeStart { get; init; }
    public string TimeEnd { get; init; }
    public string LessonStart { get; init; }
    public string LessonEnd { get; init; }
    public List<string> Audience { get; init; }
    public List<string> Teachers { get; set; }

    public int WeekDay
    {
        get => _weekDay;
        init => _weekDay = value - 1;
    }

    public int Week
    {
        get => _week;
        init => _week = value - 1;
    }
}