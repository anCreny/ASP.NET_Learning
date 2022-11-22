using WebIsland.MiddleWare;

namespace WebIsland;

public static class MiddleWareHandler
{
    public static IApplicationBuilder UseTimeTable(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TimeTableMiddleWare>();
    }

    public static IApplicationBuilder UseTimeTableAPI(this IApplicationBuilder builder, TimeTable timeTable)
    {
        return builder.UseMiddleware<TimeTableAPIMiddleWare>(timeTable);
    }

    public static IApplicationBuilder UseTest(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TestMiddleWare>();
    }

    public static IApplicationBuilder UseToday(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TodayMiddleWare>();
    }

    public static IApplicationBuilder UseTodayAPI(this IApplicationBuilder builder, TimeTable timeTable)
    {
        return builder.UseMiddleware<TodayApiMiddleWare>(timeTable);
    }

    public static IApplicationBuilder UseCachingError(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CachingErrorMiddleWare>();
    }
}