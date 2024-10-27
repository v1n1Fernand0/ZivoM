using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace ZivoM.Helpers
{
    public class KeycloakUserModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; } = true;
        public List<Credential> Credentials { get; set; }
    }
}
