using Microsoft.Extensions.DependencyInjection;
using Relaks.Managers;

namespace Relaks;

public static class RegisterServices
{
    public static void RegisterManagers(this IServiceCollection services)
    {
        // services.AddSingleton<RelaksConfigManager>();
    }
}