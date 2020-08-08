using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using SharepointClientApi.Domains;
using SharepointClientApi.Domains.Abstractions;
using System.Threading.Tasks;

namespace SharepointClientApi.Infrastructure
{
    public class FileClientApi : IFileClientApi
    {
        public async Task UploadFileAsync(UploadFileRequest request)
        {
            using var authenticationManager = new AuthenticationManager();
            using var context = authenticationManager.GetSharePointOnlineAuthenticatedContextTenant(
                request.SiteUrl, 
                AppConfigurations.SpoUserAdmin, 
                AppConfigurations.PasswordSecure);
            var list = context.Web.Lists.GetByTitle("Documents");
            await list.RootFolder.UploadFileAsync(request.FileName, request.Content, true);
        }
    }
}