namespace Relaks.Utils.Extensions;

public static class DatetimeExtensions
{
    public static DateTime StartOfDay(this DateTime date) => date.Date;
    public static DateTime EndOfDay(this DateTime date) => date.Date.AddDays(1).AddTicks(-1);
    public static DateTime StartDayOfMonth(this DateTime date) => new(date.Year, date.Month, 1);
    public static DateTime EndDayOfMonth(this DateTime date) => StartDayOfMonth(date).AddMonths(1).AddTicks(-1);
    public static DateTime StartTimeOfMonth(this DateTime date) => StartDayOfMonth(date).StartOfDay();
    public static DateTime EndTimeOfMonth(this DateTime date) => EndDayOfMonth(date).EndOfDay();

    public static List<DateTime> ToMonthRangeDays(this DateTime date)
    {
        var result = new List<DateTime>();
        var y = date.Year;
        var m = date.Month;
        for (var di = 0; di < DateTime.DaysInMonth(date.Year, date.Month); di++)
        {
            var d = di + 1;
            result.Add(new DateTime(y, m, d).StartOfDay());
        }

        return result;
    }

    public static List<DateTime> ToYearRangeDays(this DateTime date)
    {
        var result = new List<DateTime>();
        var y = date.Year;
        for (var mi = 0; mi < 12; mi++)
        {
            var m = mi + 1;
         
            for (var di = 0; di < DateTime.DaysInMonth(date.Year, date.Month); di++)
            {
                var d = di + 1;
                result.Add(new DateTime(y, m, d).StartOfDay());
            }
        }

        return result;
    }
}