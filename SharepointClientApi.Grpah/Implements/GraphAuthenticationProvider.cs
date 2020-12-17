using Microsoft.Graph;
using SharepointClientApi.Grpah.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharepointClientApi.Grpah.Implements
{
    public class GraphAuthenticationProvider : IAuthenticationProvider
    {
        private readonly ITokenManager tokenManager;

        public GraphAuthenticationProvider(ITokenManager tokenManager) => this.tokenManager = tokenManager;

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var accessToken = await tokenManager.AcquireTokenAsync();
            request.Headers.Add("Authorization", "Bearer " + accessToken);
        }
    }
}