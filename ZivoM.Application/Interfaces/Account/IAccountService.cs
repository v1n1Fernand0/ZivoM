namespace ZivoM.Accounts
{
    public interface IAccountService
    {
        Task CreateAccountAsync(UserRegistrationDto user);
        Task<string> AuthenticateAsync(UserLoginDto login);
    }
}
