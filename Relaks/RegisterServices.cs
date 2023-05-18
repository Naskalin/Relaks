using System.Globalization;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Relaks.Interfaces;
using Relaks.Models;
using Relaks.Models.Requests.EntryInfoRequests;
using Relaks.Models.Requests.EntryRequests;
using Relaks.Validators.EntryInfoValidators;
using Relaks.Validators.EntryValidators;

namespace Relaks;

public static class RegisterServices
{
    public static void RegisterManagers(this IServiceCollection services)
    {
        // services.AddSingleton<RelaksConfigManager>();
    }

    public static void RegisterValidators(this IServiceCollection services)
    {
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        services.AddScoped<IValidator<EntryFormRequest>, EntryFormRequestValidator>();

        services.AddScoped<IValidator<EiDate>, EiDateValidator>();
        services.AddScoped<IValidator<EiPhone>, EiPhoneValidator>();
        services.AddScoped<IValidator<EiUrl>, EiUrlValidator>();
        services.AddScoped<IValidator<EiEmail>, EiEmailValidator>();

        // services.AddScoped<IValidator<EntryInfoCreateRequest>, EntryInfoCreateRequestValidator>();
    }

    public static void RegisterLocalization(this IServiceCollection services)
    {
        // var defaultCulture = "ru";
        // var supportedCultures = new[]
        // {
        //     new CultureInfo(defaultCulture),
        //     new CultureInfo("en")
        // };

        var supportedCultures = new[] {"ru", "en"};
        services.AddLocalization(options => options.ResourcesPath = Path.Combine("src", "Resources"));
        // services.AddScoped<IStringLocalizer<App>, StringLocalizer<App>>();
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
        });
    }
}