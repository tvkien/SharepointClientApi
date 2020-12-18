using System.Linq;
using System.Security;

namespace SharepointClientApi.Grpah
{
    public class AzureSetting
    {
        public string Instance { get; set; }

        public string TenantId { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string GraphResource { get; set; }

        public SecureString PasswordSecure => GetPasswordSecure();

        private SecureString GetPasswordSecure()
        {
            var secureString = new SecureString();
            Password.ToList().ForEach(secureString.AppendChar);

            return secureString;
        }
    }
}