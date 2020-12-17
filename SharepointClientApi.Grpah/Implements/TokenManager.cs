using Microsoft.Identity.Client;
using SharepointClientApi.Grpah.Interfaces;
using System;
using System.Threading.Tasks;

namespace SharepointClientApi.Grpah.Implements
{
    public class TokenManager : ITokenManager
    {
        private readonly AzureSetting azureSetting;

        public TokenManager(AzureSetting azureSetting)
        {
            this.azureSetting = azureSetting;
        }

        public async Task<string> AcquireTokenAsync()
        {
            var authority = $"{azureSetting.Instance}/{azureSetting.TenantId}";
            var app = ConfidentialClientApplicationBuilder.Create(azureSetting.ClientId)
                                                      .WithClientSecret(azureSetting.ClientSecret)
                                                      .WithAuthority(new Uri(authority))
                                                      .Build();
            string[] scopes = { azureSetting.GraphResource };
            var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
            return result.AccessToken;
        }
    }
}