using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SharepointClientApi.Domains;
using SharepointClientApi.Domains.Abstractions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SharepointClientApi.Infrastructure
{
    public class GraphClientApi : IGraphClientApi
    {
        public async Task<string> AcquireTokenAsync(X509Certificate2 X509Cert)
        {
            var certificate = new ClientAssertionCertificate(AppConfigurations.ClientID, X509Cert);
            var context = new AuthenticationContext($"{AppConfigurations.AzureInstance}/{AppConfigurations.TenantID}");
            var authResult = await context.AcquireTokenAsync(AppConfigurations.GraphResource, certificate);
            return authResult.AccessToken;
        }
    }
}