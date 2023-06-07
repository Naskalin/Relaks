namespace Relaks.Utils.Extensions;

public static class DatetimeExtensions
{
    public static DateTime StartOfDay(this DateTime date) => date.Date;
    public static DateTime EndOfDay(this DateTime date) => date.Date.AddDays(1).AddTicks(-1);
    public static DateTime StartDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, 1);
    public static DateTime EndDayOfMonth(this DateTime date) => StartDayOfMonth(date).AddMonths(1).AddTicks(-1);
    public static DateTime StartTimeOfMonth(this DateTime date) => StartDayOfMonth(date).StartOfDay();
    public static DateTime EndTimeOfMonth(this DateTime date) => EndDayOfMonth(date).EndOfDay();
}