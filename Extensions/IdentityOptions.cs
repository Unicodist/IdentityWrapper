namespace PremiumIdentity.Extensions;

public class IdentityOptions
{
    public IdentityOptions(string host, string apiKey, string clientId,  string? clientName)
    {
        Host = host;
        ApiKey = apiKey;
        ClientId = clientId;
        ClientName = clientName;
    }

    public string Host { get; set; }
    public string? ClientName { get; set; }
    public string ApiKey { get; set; }
    public string ClientId { get; set; }
    
    internal string LoginPath => $"{Host}/oauth/login";
    internal string VerifyEndpoint => $"{Host}/api/auth/verify";
    internal string UserUrl => $"{Host}/api/user";
    internal string PendingUsers => $"{Host}/api/user/pending";
    internal string InviteUser => $"{Host}/api/user/invite";
}
