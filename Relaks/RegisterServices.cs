using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Relaks;

public static class RegisterServices
{
    public static void RegisterManagers(this IServiceCollection services)
    {
        // services.AddSingleton<RelaksConfigManager>();
    }
    public static void RegisterLocalization(this IServiceCollection services)
    {
        var defaultCulture = "ru";
        var supportedCultures = new[]
        {
            new CultureInfo(defaultCulture),
            new CultureInfo("en")
        };
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(options => {
            options.DefaultRequestCulture = new RequestCulture(defaultCulture);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
    }
}