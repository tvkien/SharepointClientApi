using Microsoft.Identity.Client;
using SharepointClientApi.Grpah.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
            var app = ConfidentialClientApplicationBuilder
                .Create(azureSetting.ClientId)
                .WithTenantId(azureSetting.TenantId)
                .WithClientSecret(azureSetting.ClientSecret)
                .Build();
            string[] scopes = new string[] { "user.read" };
            var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
            return result.AccessToken;
        }

        public async Task<string> AcquireTokenByUsernamePasswordAsync()
        {
            var options = new PublicClientApplicationOptions()
            {
                ClientId = azureSetting.ClientId,
                TenantId = azureSetting.TenantId,
                AzureCloudInstance = AzureCloudInstance.AzurePublic,
            };

            var app = PublicClientApplicationBuilder.CreateWithApplicationOptions(options).Build();
            string[] scopes = { azureSetting.GraphResource };

            var accounts = (await app.GetAccountsAsync()).ToList();
            if (accounts.Any())
            {
                var result = await app.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync();
                return result.AccessToken;
            }
            else
            {
                var result = await app.AcquireTokenByUsernamePassword(scopes, azureSetting.Username, azureSetting.PasswordSecure).ExecuteAsync();
                return result.AccessToken;
            }
            
        }
    }
}