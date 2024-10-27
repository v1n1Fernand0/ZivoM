namespace ZivoM.Domain.Constants
{
    public static class KeycloakServiceMessages
    {
        public const string AuthenticationFailed = "Authentication with Keycloak failed.";
        public const string AccessTokenRetrievalFailed = "Failed to retrieve access token from Keycloak.";

        public const string ClientIdMissing = "ClientId for Keycloak is not configured.";
        public const string ClientSecretMissing = "ClientSecret for Keycloak is not configured.";
        public const string AuthorityUrlMissing = "Authority URL for Keycloak is not configured.";

        public const string UserCreationFailed = "Failed to create user in Keycloak.";
        public const string InvalidUserData = "User data is invalid or incomplete.";

        public const string ServiceUnavailable = "Keycloak service is temporarily unavailable. Please try again later.";
        public const string UnexpectedError = "An unexpected error occurred while communicating with Keycloak.";
    }
}
