using WebIsland.MiddleWare;
using WebIsland.Server.MiddleWare;

namespace WebIsland;

public static class MiddleWareHandler
{
    public static IApplicationBuilder UseTimeTable(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TimeTableMiddleWare>();
    }

    public static IApplicationBuilder UseTimeTableAPI(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TimeTableAPIMiddleWare>();
    }

    public static IApplicationBuilder UseTest(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TestMiddleWare>();
    }

    public static IApplicationBuilder UseToday(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TodayMiddleWare>();
    }

    public static IApplicationBuilder UseTodayAPI(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TodayApiMiddleWare>();
    }

    public static IApplicationBuilder UseCachingError(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CachingErrorMiddleWare>();
    }

    public static IApplicationBuilder UseCookieChecker(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CookiePassMiddleWare>();
    }

    public static IApplicationBuilder UseGroupNumberAPI(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GroupNumberAPIMiddleWare>();
    }

    public static IApplicationBuilder UseSettings(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SettingsMiddleWare>();
    }

    public static IApplicationBuilder UseSettingAPI(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SettingsAPIMiddleWare>();
    }
}