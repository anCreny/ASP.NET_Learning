namespace WebIsland;

public class TimeTable
{
    private Day[,] _timeTable =
    {
        {new Day(1,1), new Day(1,2), new Day(1,3), new Day(1,4), new Day(1,5), new Day(1,6)}, 
        {new Day(2,1), new Day(2,2), new Day(2,3), new Day(2,4), new Day(2,5), new Day(2,6)}
    };

    public void AddSubject(Subject subject)
    {
        _timeTable[subject.Week, subject.WeekDay].Subjects.Add(subject);
    }

    public Day GetDay(DateTime day)
    {
        var weekday = -1;
        
        switch (day.DayOfWeek)
        {
            case DayOfWeek.Monday:
                weekday = 0;
                break;
            case DayOfWeek.Tuesday:
                weekday = 1;
                break;
            case DayOfWeek.Wednesday:
                weekday = 2;
                break;
            case DayOfWeek.Thursday:
                weekday = 3;
                break;
            case DayOfWeek.Friday:
                weekday = 4;
                break;
            case DayOfWeek.Saturday:
                weekday = 5;
                break;
        }

        var startPoint = new DateTime(2022, 9, 5);
        var week = (Convert.ToInt32((day - startPoint).TotalDays) / 7) % 2 == 0 ? 1 : 0;

        if (weekday == -1)
        {
            return new Day(week+1, -1);
        }
        return _timeTable[week, weekday];
    }

    public Day[,] Get => _timeTable;
}