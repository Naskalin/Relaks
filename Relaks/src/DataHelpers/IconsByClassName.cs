using Relaks.Models;

namespace Relaks.DataHelpers;

public static partial class DataHelper
{
    private static Dictionary<string, string> IconsByEntityNames => new()
    {
        {nameof(EPerson), "las la-user"},
        {nameof(EMeet), "las la-handshake"},
        {nameof(ECompany), "las la-building"},
        
        {nameof(EiDate), "las la-calendar"},
        {nameof(EiEmail), "las la-envelope"},
        {nameof(EiUrl), "las la-link"},
        {nameof(EiPhone), "las la-phone"},
        {nameof(EiCustom), "las la-align-left"},
    };
    
    public static string EntityIcon(string className)
    {
        return IconsByEntityNames.TryGetValue(className, out var value) ? value : "las la-question";
    }
}