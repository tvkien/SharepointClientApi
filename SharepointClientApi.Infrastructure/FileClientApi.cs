using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using SharepointClientApi.Domains;
using SharepointClientApi.Domains.Abstractions;
using SharepointClientApi.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace SharepointClientApi.Infrastructure
{
    public class FileClientApi : IFileClientApi
    {
        public async Task CreateFolderAsync(CreateFolderRequest request)
        {
            using var context = GetSharePointOnlineContext(request.SiteUrl);
            var list = context.Web.GetListByTitle(request.ListName);
            var folder = await list.RootFolder.EnsureFolderAsync(request.FolderName);

            if (request.IsSharing)
            {
                var urlToDocument = $"{AppConfigurations.SpoUrl}{folder.ServerRelativeUrl}";
                await context.Web.ShareDocumentAsync(
                    urlToDocument, 
                    request.TargerEmailToShare, 
                    request.SharingOption.ExternalSharing(), 
                    sendEmail: false);
            }
        }

        public async Task UploadFileAsync(UploadFileRequest request)
        {
            using var context = GetSharePointOnlineContext(request.SiteUrl);
            var list = context.Web.Lists.GetByTitle(request.ListName);
            await list.RootFolder.UploadFileAsync(request.FileName, request.Content, true);
        }

        private static ClientContext GetSharePointOnlineContext(string siteUrl)
        {
            using var authenticationManager = new AuthenticationManager();
            {
                return authenticationManager.GetSharePointOnlineAuthenticatedContextTenant(
                    siteUrl,
                    AppConfigurations.SpoUserAdmin,
                    AppConfigurations.PasswordSecure);
            }
        }
    }
}