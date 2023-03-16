using Microsoft.AspNetCore.Http;
using PremiumIdentity.Extensions;

namespace PremiumIdentity.Models;

public class IdentityConfig
{    
    public IdentityConfig(Func<IHttpContextAccessor, IdentityOptions> optionsGetter)
    {
        GetIdentityOptions = optionsGetter;
    }
    
    internal Func<IHttpContextAccessor, IdentityOptions> GetIdentityOptions { get; set; }
}
