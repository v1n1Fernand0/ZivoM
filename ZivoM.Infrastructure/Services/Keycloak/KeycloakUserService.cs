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
        private readonly string _authority;
        private readonly string _realm;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public KeycloakUserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _authority = _configuration["Keycloak:Authority"] ?? throw new InvalidOperationException(KeycloakServiceMessages.AuthorityUrlMissing);
            _realm = _configuration["Keycloak:Realm"] ?? throw new InvalidOperationException(KeycloakServiceMessages.RealmMissing);
            _clientId = _configuration["Keycloak:ClientId"] ?? throw new InvalidOperationException(KeycloakServiceMessages.ClientIdMissing);
            _clientSecret = _configuration["Keycloak:ClientSecret"] ?? throw new InvalidOperationException(KeycloakServiceMessages.ClientSecretMissing);

            // Configura a URL base para acessar o Keycloak
            _httpClient.BaseAddress = new Uri($"{_authority}/admin/realms/{_realm}");
        }

        public async Task CreateUserAsync(KeycloakUserModel user)
        {
            // Obtem o token de administrador para a operação
            var token = await GetAdminTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Serializa o modelo de usuário e realiza a requisição de criação
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/users", content);

            // Verifica se a requisição foi bem-sucedida
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"{KeycloakServiceMessages.UserCreationFailed}: {response.ReasonPhrase}");
            }
        }

        private async Task<string> GetAdminTokenAsync()
        {
            // Monta a requisição para obter o token de administrador
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_authority}/realms/{_realm}/protocol/openid-connect/token");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = _clientId,
                ["client_secret"] = _clientSecret,
                ["grant_type"] = "client_credentials"
            });

            var response = await _httpClient.SendAsync(request);

            // Verifica se a requisição foi bem-sucedida
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"{KeycloakServiceMessages.AuthenticationFailed}: {response.ReasonPhrase}");
            }

            // Processa a resposta JSON para extrair o token de acesso
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
            return tokenResponse?.AccessToken ?? throw new InvalidOperationException(KeycloakServiceMessages.AccessTokenRetrievalFailed);
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            // Monta a requisição para autenticação do usuário
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_authority}/realms/{_realm}/protocol/openid-connect/token");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = _clientId,
                ["grant_type"] = "password",
                ["username"] = username,
                ["password"] = password
            });

            var response = await _httpClient.SendAsync(request);

            // Verifica se a requisição foi bem-sucedida
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"{KeycloakServiceMessages.AuthenticationFailed}: {response.ReasonPhrase}");
            }

            // Processa a resposta JSON para extrair o token de acesso
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
            return tokenResponse?.AccessToken ?? throw new InvalidOperationException(KeycloakServiceMessages.AccessTokenRetrievalFailed);
        }
    }
}
