using ZivoM.Accounts;
using ZivoM.Domain.Constants;
using ZivoM.Helpers;
using ZivoM.Infrastructure.Services;

namespace ZivoM.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly KeycloakUserService _keycloakUserService;

        public AccountService(KeycloakUserService keycloakUserService)
        {
            _keycloakUserService = keycloakUserService;
        }

        public async Task CreateAccountAsync(UserRegistrationDto user)
        {
            var keycloakUser = new KeycloakUserModel
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Enabled = true,
                Credentials = new List<Credential>
                {
                    new Credential
                    {
                        Type = "password",
                        Value = user.Password,
                        Temporary = false
                    }
                }
            };

            try
            {
                await _keycloakUserService.CreateUserAsync(keycloakUser);
            }
            catch (HttpRequestException httpEx)
            {
                throw new InvalidOperationException(KeycloakServiceMessages.UserCreationFailed, httpEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{KeycloakServiceMessages.UserCreationFailed}: {ex.Message}", ex);
            }
        }

        public async Task<string> AuthenticateAsync(UserLoginDto login)
        {
            try
            {
                return await _keycloakUserService.AuthenticateAsync(login.Username, login.Password);
            }
            catch (HttpRequestException httpEx)
            {
                throw new InvalidOperationException(KeycloakServiceMessages.AuthenticationFailed, httpEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{KeycloakServiceMessages.AuthenticationFailed}: {ex.Message}", ex);
            }
        }
    }
}
