using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using SharepointClientApi.Domains;
using SharepointClientApi.Domains.Abstractions;
using System.Threading.Tasks;

namespace SharepointClientApi.Infrastructure
{
    public class ListClientApi : IListClientApi
    {
        public Task CreateDocumentLibraryAsync(CreateDocumentLibraryRequest request)
        {
            using var authenticationManager = new AuthenticationManager();
            using var context = authenticationManager.GetSharePointOnlineAuthenticatedContextTenant(
                request.SiteUrl,
                AppConfigurations.SpoUserAdmin,
                AppConfigurations.PasswordSecure);

            context.Web.CreateDocumentLibrary(request.ListName);
            return Task.CompletedTask;
        }
    }
}