using PnP.Framework;
using PnP.Framework.Sites;
using Sharepoint.Business.Interfaces;
using Sharepoint.Business.Models;
using Sharepoint.Business.Requests;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Sharepoint.Business.Implements
{
    public class SiteCollectionManager : ISiteCollectionManager
    {
        private readonly SpoSetting spoSetting;
        private readonly ITokenManager tokenManager;

        public SiteCollectionManager(SpoSetting spoSetting, ITokenManager tokenManager)
        {
            this.spoSetting = spoSetting;
            this.tokenManager = tokenManager;
        }

        public async Task<string> CreateSiteCollectionAsync(SiteCollectionRequest request)
        {
            var accessToken = await tokenManager.GetAccessTokenSPOAsync(spoSetting.SiteUrlAdmin);
            var accessTokenSecure = new SecureString();
            accessToken.ToList().ForEach(accessTokenSecure.AppendChar);
            var authManager = new AuthenticationManager(accessTokenSecure);
            using var context = authManager.GetContext(spoSetting.SiteUrlAdmin);
            var site = new TeamNoGroupSiteCollectionCreationInformation
            {
                Owner = spoSetting.UserName,
                Title = request.Title,
                Url = $"{spoSetting.SiteUrl}/sites/" + request.Alias,
                Lcid = 1033,
                ShareByEmailEnabled = true,
            };

            await SiteCollection.CreateAsync(context, site, noWait: true);
            return site.Url;
        }
    }
}