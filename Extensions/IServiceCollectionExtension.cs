using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PremiumIdentity.Helpers;
using PremiumIdentity.Models;
using PremiumIdentity.Services;

namespace PremiumIdentity.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddPremiumIdentity(this IServiceCollection services, Func<IHttpContextAccessor, IdentityOptions> optionsGetter)
    {
        services.AddSingleton(new IdentityConfig(optionsGetter));
        services.AddScoped<IdentityContext>();
        StartupMessageHelper.PrintWelcomeMessage();
    }
}
