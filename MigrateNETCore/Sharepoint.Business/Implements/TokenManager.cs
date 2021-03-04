using Sharepoint.Business.Interfaces;
using Sharepoint.Business.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sharepoint.Business.Implements
{
    public class TokenManager : ITokenManager
    {
        private readonly HttpClient httpClient;
        private readonly AzureSetting azureSetting;
        private readonly SpoSetting spoSetting;

        public TokenManager(
            IHttpClientFactory httpClientFactory,
            AzureSetting azureSetting,
            SpoSetting spoSetting)
        {
            httpClient = httpClientFactory.CreateClient();
            this.azureSetting = azureSetting;
            this.spoSetting = spoSetting;
        }

        public async Task<string> GetAccessTokenSPOAsync(string siteUrl)
        {
            var uri = new Uri(siteUrl);
            var scope = $"{uri.Scheme}://{uri.Authority}/.default";
            var requestData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("client_id", azureSetting.ClientId),
                new KeyValuePair<string, string>("client_secret", azureSetting.ClientSecret),
                new KeyValuePair<string, string>("scope", scope),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", spoSetting.UserName),
                new KeyValuePair<string, string>("password", spoSetting.Password)
            };

            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(azureSetting.TokenEnpoint),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent(requestData)
            };

            var response = await httpClient.SendAsync(httpRequestMessage);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"GetAccessTokenSPOAsync: Get access token failure: {result}");
            }

            var tokenResult = JsonSerializer.Deserialize<JsonElement>(result);
            return tokenResult.GetProperty("access_token").GetString();
        }
    }
}