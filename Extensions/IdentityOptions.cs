namespace PremiumIdentity.Extensions;

public class IdentityOptions
{
    public IdentityOptions(string loginRedirectionHost, string host, string apiKey, string clientId)
    {
        LoginRedirectionHost = loginRedirectionHost;
        Host = host;
        ApiKey = apiKey;
        ClientId = clientId;
    }

    public string LoginRedirectionHost { get; }

    public string Host { get; }
    public string ApiKey { get; }
    public string ClientId { get; }
    
    internal string LoginPath => $"{LoginRedirectionHost}/oauth/login";
    internal string VerifyEndpoint => $"{Host}/api/auth/verify";
    internal string UserUrl => $"{Host}/api/user";
    internal string PendingUsers => $"{Host}/api/user/pending";
    internal string InviteUser => $"{Host}/api/user/invite";
}
