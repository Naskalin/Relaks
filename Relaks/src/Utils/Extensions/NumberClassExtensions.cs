using BootstrapBlazor.Components;

namespace Relaks.Utils.Extensions;

public static class NumberClassExtensions
{
    public static string ToCssColorClass(this decimal val) => val switch
    {
        > 0 => "text-success",
        < 0 => "text-danger",
        _ => ""
    };
    
    public static string ToCssColorClass(this double val) => val switch
    {
        > 0 => "text-success",
        < 0 => "text-danger",
        _ => ""
    };
}