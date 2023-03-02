using Microsoft.Extensions.DependencyInjection;
using PremiumIdentity.Helpers;
using PremiumIdentity.Models;
using PremiumIdentity.Services;

namespace PremiumIdentity.Extensions;

public static class IServiceCollectionExtension
{
    public static void AddPremiumIdentity(this IServiceCollection services, Action<IdentityOptions> action)
    {
        var options = new IdentityOptions();
        action(options);

        services.AddScoped<IdentityContext>();
        services.AddSingleton(new IdentityConfig(options.Host,options.ClientName,options.ApiKey,options.ClientId));
        
        StartupMessageHelper.PrintWelcomeMessage(options.Host,options.ClientName);
        
        if (Uri.TryCreate(options.Host, UriKind.Absolute, out var uriResult) &&
            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
        {
            options.Host = uriResult.OriginalString;
        }
        else
        {
            throw new ArgumentException("Invalid Identity host url");
        }
    }
}
