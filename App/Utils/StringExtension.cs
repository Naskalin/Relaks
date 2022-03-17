namespace App.Utils;

public static class StringExtension
{
    public static bool IsParsable<T>(this string value) where T : struct
    {
        return Enum.TryParse<T>(value, true, out _);
    }
}