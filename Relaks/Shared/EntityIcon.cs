using Relaks.Models;

namespace Relaks.Shared;

public static class EntityIcon
{
    public static Dictionary<string, string> ByName => new()
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
}