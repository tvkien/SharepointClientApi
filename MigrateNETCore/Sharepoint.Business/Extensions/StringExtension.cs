using System.Linq;
using System.Security;
using System.Text.RegularExpressions;

namespace Sharepoint.Business.Extensions
{
    public static class StringExtension
    {
        public static string RemoveLastSlash(this string link) 
            => Regex.Replace(link, @"\/$", "");

        public static SecureString ToSecureString(this string inputString)
        {
            var secureString = new SecureString();
            inputString.ToList().ForEach(secureString.AppendChar);
            return secureString;
        }
    }
}