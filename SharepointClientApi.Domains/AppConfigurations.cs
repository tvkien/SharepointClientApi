using System.Configuration;
using System.Linq;
using System.Security;

namespace SharepointClientApi.Domains
{
    public static class AppConfigurations
    {
        public static string SpoUrl => ConfigurationManager.AppSettings["SpoUrl"];

        public static string SpoUrlAdmin => ConfigurationManager.AppSettings["SpoUrlAdmin"];

        public static string SpoUserAdmin => ConfigurationManager.AppSettings["SpoUserAdmin"];

        public static string SpoUserPassword => ConfigurationManager.AppSettings["SpoUserPassword"];

        public static string TenantID => ConfigurationManager.AppSettings["TenantID"];

        public static string ClientID => ConfigurationManager.AppSettings["ClientID"];

        public static string CertThumbPrint => ConfigurationManager.AppSettings["CertThumbPrint"];

        public static string AzureInstance => ConfigurationManager.AppSettings["AzureInstance"];

        public static string GraphResource => ConfigurationManager.AppSettings["GraphResource"];

        public static SecureString PasswordSecure => GetPasswordSecure();

        private static SecureString GetPasswordSecure()
        {
            var secureString = new SecureString();
            SpoUserPassword.ToList().ForEach(secureString.AppendChar);

            return secureString;
        }
    }
}