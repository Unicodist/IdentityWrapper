namespace PremiumIdentity.Models;

public class IdentityConfig
{    
    public IdentityConfig(string identityHost, string? clientName, string apiKey,string clientId)
    {
        IdentityHost = identityHost;
        ClientName = clientName;
        ApiKey = apiKey;
        ClientId = clientId;
        
        VerifyEndpoint = $"{IdentityHost}/api/auth/verify";
        UserUrl = $"{IdentityHost}/api/user";
        PendingUsers = $"{IdentityHost}/api/user/pending";
        InviteUser = $"{IdentityHost}/api/user/invite";
        LoginPath = "http://103.198.9.245:8081/oauth/login";
    }
    internal string IdentityHost { get; set; }
    private string? ClientName { get; set; }
    internal string ApiKey { get; set; }
    internal string ClientId { get; set; }

    internal readonly string LoginPath;

    internal readonly string VerifyEndpoint;
    internal readonly string UserUrl;
    internal readonly string PendingUsers;
    internal readonly string InviteUser;
}
