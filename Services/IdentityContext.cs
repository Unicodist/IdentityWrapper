using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using PremiumIdentity.Entities;
using PremiumIdentity.Extensions;
using PremiumIdentity.Models;

namespace PremiumIdentity.Services;

public class IdentityContext
{
    private readonly HttpClient _client;
    private readonly IdentityOptions _identityOptions;
    public IdentityContext(IdentityConfig identityConfig, IHttpContextAccessor httpContextAccessor)
    {
        _identityOptions = identityConfig.GetIdentityOptions(httpContextAccessor);
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Add("x-api-key",_identityOptions.ApiKey);
        _client.DefaultRequestHeaders.Add("XClientId",_identityOptions.ClientId);
    }
    public async Task<string?> RefreshSession(string refreshToken)
    {
        var url = $"{_identityOptions.VerifyEndpoint}/{refreshToken}";
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
        var url = $"{_identityOptions.UserUrl}/{uid}";
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
        var url = $"{_identityOptions.PendingUsers}";
        var response = await _client.GetAsync(url);

        if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
        var users = await response.Content.ReadFromJsonAsync<ICollection<PendingUser>>();
        return users ?? new List<PendingUser>();

    }

    public async Task<PendingUser> InviteUserAsync(string identifier)
    {
        var url = $"{_identityOptions.InviteUser}/{identifier}";
        var response = await _client.GetAsync(url);

        if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
        var user = await response.Content.ReadFromJsonAsync<PendingUser>();
        return user!;

    }

    public string GetIdentityHost()
    {
        return _identityOptions.Host;
    }

    public string GetLoginPath(string returnUrl)
    {
        // Todo: screen all the string parameters for XSS
        // Todo: [In PremiumIdServer] screen all the string parameters for SQL/Xss injection
        return $"{_identityOptions.LoginPath}?clientId={_identityOptions.ClientId}&returnUrl={returnUrl}";
    }
}
