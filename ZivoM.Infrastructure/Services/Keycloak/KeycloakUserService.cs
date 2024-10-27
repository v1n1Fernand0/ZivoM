using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ZivoM.Domain.Constants;
using ZivoM.Helpers;

namespace ZivoM.Infrastructure.Services
{
    public class KeycloakUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public KeycloakUserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var authority = _configuration["Keycloak:Authority"];
            var realm = _configuration["Keycloak:Realm"];

            if (string.IsNullOrEmpty(authority))
                throw new InvalidOperationException(KeycloakServiceMessages.AuthorityUrlMissing);
            if (string.IsNullOrEmpty(realm))
                throw new InvalidOperationException(KeycloakServiceMessages.ClientIdMissing);

            _httpClient.BaseAddress = new Uri($"{authority}/admin/realms/{realm}");
        }

        public async Task CreateUserAsync(KeycloakUserModel user)
        {
            var token = await GetAdminTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/users", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(KeycloakServiceMessages.UserCreationFailed);
            }
        }

        private async Task<string> GetAdminTokenAsync()
        {
            var clientId = _configuration["Keycloak:ClientId"];
            var clientSecret = _configuration["Keycloak:ClientSecret"];
            var authority = _configuration["Keycloak:Authority"];

            if (string.IsNullOrEmpty(clientId))
                throw new InvalidOperationException(KeycloakServiceMessages.ClientIdMissing);
            if (string.IsNullOrEmpty(clientSecret))
                throw new InvalidOperationException(KeycloakServiceMessages.ClientSecretMissing);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{authority}/protocol/openid-connect/token");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["grant_type"] = "client_credentials"
            });

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(KeycloakServiceMessages.AuthenticationFailed);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
            return tokenResponse?.AccessToken ?? throw new InvalidOperationException(KeycloakServiceMessages.AccessTokenRetrievalFailed);
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var clientId = _configuration["Keycloak:ClientId"];
            var authority = _configuration["Keycloak:Authority"];

            if (string.IsNullOrEmpty(clientId))
                throw new InvalidOperationException(KeycloakServiceMessages.ClientIdMissing);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{authority}/protocol/openid-connect/token");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = clientId,
                ["grant_type"] = "password",
                ["username"] = username,
                ["password"] = password
            });

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(KeycloakServiceMessages.AuthenticationFailed);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
            return tokenResponse?.AccessToken ?? throw new InvalidOperationException(KeycloakServiceMessages.AccessTokenRetrievalFailed);
        }
    }
}
