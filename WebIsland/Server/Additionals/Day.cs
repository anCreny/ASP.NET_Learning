namespace WebIsland;

public class Day
{
    public Day(int week, int weekDay)
    {
        Week = week;
        WeekDay = weekDay;
    }

    public List<Subject> Subjects { get; set; } = new();
    
    public int WeekDay { get; }
    public int Week { get; }

}