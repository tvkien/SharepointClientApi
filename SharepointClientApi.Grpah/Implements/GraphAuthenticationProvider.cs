using Microsoft.Graph;
using SharepointClientApi.Grpah.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SharepointClientApi.Grpah.Implements
{
    public class GraphAuthenticationProvider : IAuthenticationProvider
    {
        private readonly ITokenManager tokenManager;

        public GraphAuthenticationProvider(ITokenManager tokenManager) => this.tokenManager = tokenManager;

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var accessToken = await tokenManager.AcquireTokenByUsernamePasswordAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}