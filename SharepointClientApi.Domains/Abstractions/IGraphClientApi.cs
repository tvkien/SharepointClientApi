using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SharepointClientApi.Domains.Abstractions
{
    public interface IGraphClientApi
    {
        Task<string> AcquireTokenAsync(X509Certificate2 X509Cert);
    }
}