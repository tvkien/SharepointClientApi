using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using OfficeDevPnP.Core.Sites;
using SharepointClientApi.Domains;
using SharepointClientApi.Domains.Abstractions;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SharepointClientApi.Infrastructure
{
    public class SiteCreation : ISiteCreation
    {
        private readonly IGraphClientApi graphClientApi;

        public SiteCreation(IGraphClientApi graphClientApi)
        {
            this.graphClientApi = graphClientApi;
        }

        public async Task<string> CreateCommunicationSiteAsync(CommunicationSiteRequest request)
        {
            using var context = new ClientContext(AppConfigurations.SpoUrlAdmin)
            {
                Credentials = new SharePointOnlineCredentials(AppConfigurations.SpoUserAdmin, PasswordSecure)
            };

            var communicationSite = new CommunicationSiteCollectionCreationInformation
            {
                Title = request.Title,
                Url = $"{AppConfigurations.SpoUrl}/sites/{request.Alias}",
                Description = request.Description,
                Lcid = (uint)request.Language,
                ShareByEmailEnabled = true,
            };

            await SiteCollection.CreateAsync(context, communicationSite, noWait: true);

            return communicationSite.Url;
        }

        public async Task<string> CreateTeamSiteCollectionAsync(TeamSiteCollectionRequest request)
        {
            var X509Cert = GetX509Certificate2();
            var accessToken = await graphClientApi.AcquireTokenAsync(X509Cert);

            using var authmanager = new AuthenticationManager();
            using var context = authmanager.GetAzureADAppOnlyAuthenticatedContext(
                AppConfigurations.SpoUrlAdmin,
                AppConfigurations.ClientID,
                AppConfigurations.TenantID,
                X509Cert);

            var teamSiteCollection = new TeamSiteCollectionCreationInformation
            {
                Alias = request.Alias,
                DisplayName = request.DisplayName,
                Description = request.Description,
                Lcid = (uint)request.Language,
                Owners = request.Owners == null || request.Owners.Length == 0
                    ? new[] { AppConfigurations.SpoUserAdmin }
                    : request.Owners
            };

            await SiteCollection.CreateTeamSiteViaGraphAsync(context, teamSiteCollection, noWait: true, graphAccessToken: accessToken);

            return $"{AppConfigurations.SpoUrl}/sites/{request.Alias}";
        }

        public async Task<string> CreateTeamSiteNoGroupAsync(TeamSiteNoGroupRequest request)
        {
            var X509Cert = GetX509Certificate2();

            using var authmanager = new AuthenticationManager();
            using var context = authmanager.GetAzureADAppOnlyAuthenticatedContext(
                AppConfigurations.SpoUrlAdmin,
                AppConfigurations.ClientID,
                AppConfigurations.TenantID, 
                X509Cert);

            var teamNoGroupSiteCollection = new TeamNoGroupSiteCollectionCreationInformation
            {
                Url = $"{AppConfigurations.SpoUrl}/sites/{request.Alias}",
                Owner = string.IsNullOrEmpty(request.Owner) ? AppConfigurations.SpoUserAdmin : request.Owner,
                Title = request.Title,
                Lcid = (uint)request.Language,
                Description = request.Description,
                ShareByEmailEnabled = true
            };

            await SiteCollection.CreateAsync(context, teamNoGroupSiteCollection, noWait: true);

            return teamNoGroupSiteCollection.Url;
        }

        private SecureString PasswordSecure => GetPasswordSecure();

        private SecureString GetPasswordSecure()
        {
            SecureString secureString = new SecureString();
            AppConfigurations.SpoUserPassword.ToList().ForEach(secureString.AppendChar);

            return secureString;
        }

        private static X509Certificate2 GetX509Certificate2()
        {
            using var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            var certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, AppConfigurations.CertThumbPrint, false);
            return certCollection[0];
        }
    }
}