namespace App.Utils.Extensions;

public static class StringExtension
{
    public static bool Equals<TEnum>(this string str, TEnum textTypeEnum) where TEnum : struct
    {
        return Enum.TryParse(str, true, out TEnum result) && result.Equals(textTypeEnum);
    }
    
    // public static bool EqualEnum<TEnum>(this string str, TEnum value) where TEnum : struct
    // {
    //     return Enum.TryParse(str, true, out TEnum result) && result == value;
    // }
    
    // public static bool Equals<TEnum>(this string str, Value enumVal) where TEnum : struct
    // {
    //     try
    //     {
    //         Enum.TryParse(str, true, out TEnum parsedEnumVal);
    //         return enumVal.Equals(parsedEnumVal);
    //     }
    //     catch (Exception)
    //     {
    //         return false;
    //     }
    // }
    
    // public static bool Equals(this string str, Enum enumVal)
    // {
    //     try
    //     {
    //         Enum.TryParse(str, true, out enumVal parsedEnumVal);
    //         return enumVal.Equals(parsedEnumVal);
    //     }
    //     catch (Exception)
    //     {
    //         return false;
    //     }
    // }
    
    // public static bool Equals(this string enumString, Name value)
    // {
    //     if(Enum.TryParse<Name>(enumString, out var v))
    //     {
    //         return value == v;
    //     }
    //
    //     return false;
    // }
}