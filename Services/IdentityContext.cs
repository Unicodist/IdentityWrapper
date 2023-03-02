using System.Net;
using System.Net.Http.Json;
using PremiumIdentity.Entities;
using PremiumIdentity.Models;

namespace PremiumIdentity.Services;

public class IdentityContext
{
    private readonly HttpClient _client;
    private readonly IdentityConfig _identityConfig;
    public IdentityContext(IdentityConfig identityConfig)
    {
        _identityConfig = identityConfig;
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Add("x-api-key",_identityConfig.ApiKey);
        _client.DefaultRequestHeaders.Add("XClientId",_identityConfig.ClientId);
    }
    public async Task<string?> RefreshSession(string refreshToken)
    {
        var url = $"{_identityConfig.VerifyEndpoint}/{refreshToken}";
        var response = await _client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var guid = await response.Content.ReadFromJsonAsync<RefreshResponseModel>();
            return guid?.UserGuid;
        }
        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            throw new Exception((await response.Content.ReadFromJsonAsync<ErrorModel>())?.Message);
        }
        return null;
    }
    public async Task<PremiumUserModel?> GetUserAsync(string uid)
    {
        var url = $"{_identityConfig.UserUrl}/{uid}";
        var response = await _client.GetAsync(url);
        
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<PremiumUserModel>();
            return user;
        }
        return null;
    }

    public async Task<ICollection<PendingUser>> GetPendingUsersAsync()
    {
        var url = $"{_identityConfig.PendingUsers}";
        var response = await _client.GetAsync(url);

        if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
        var users = await response.Content.ReadFromJsonAsync<ICollection<PendingUser>>();
        return users ?? new List<PendingUser>();

    }

    public async Task<PendingUser> InviteUserAsync(string identifier)
    {
        var url = $"{_identityConfig.InviteUser}/{identifier}";
        var response = await _client.GetAsync(url);

        if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
        var user = await response.Content.ReadFromJsonAsync<PendingUser>();
        return user!;

    }
}
