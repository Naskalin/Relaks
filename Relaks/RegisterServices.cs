using System.Globalization;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Relaks.Models.Requests.EntryRequests;
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
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

        // services.AddFormValidation(c => c.AddFluentValidation(typeof(EntryFormRequest).Assembly));
        services.AddScoped<IValidator<EntryFormRequest>, EntryFormValidator>();
        // services.AddValidatorsFromAssemblyContaining<Program>();

    }
    
    public static void RegisterLocalization(this IServiceCollection services)
    {
        // var defaultCulture = "ru";
        // var supportedCultures = new[]
        // {
        //     new CultureInfo(defaultCulture),
        //     new CultureInfo("en")
        // };
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        // services.AddScoped<IStringLocalizer<App>, StringLocalizer<App>>();
        // services.Configure<RequestLocalizationOptions>(options => {
        //     options.DefaultRequestCulture = new RequestCulture(defaultCulture);
        //     options.SupportedCultures = supportedCultures;
        //     options.SupportedUICultures = supportedCultures;
        // });
    }
}