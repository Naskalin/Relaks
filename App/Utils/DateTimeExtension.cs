namespace App.Utils;

public static class DateTimeExtension
{
    public static DateTime? ValueOrNullDefault(this DateTime dateTime)
    {
        return dateTime == default ? null : dateTime;
    }
}