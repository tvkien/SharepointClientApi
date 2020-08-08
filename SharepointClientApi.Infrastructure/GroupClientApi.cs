using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using SharepointClientApi.Domains;
using SharepointClientApi.Domains.Abstractions;
using System.Threading.Tasks;

namespace SharepointClientApi.Infrastructure
{
    public class GroupClientApi : IGroupClientApi
    {
        public Task AddGroupAsync(AddGroupRequest request)
        {
            using var authenticationManager = new AuthenticationManager();
            using var context = authenticationManager.GetSharePointOnlineAuthenticatedContextTenant(
                request.SiteUrl,
                AppConfigurations.SpoUserAdmin,
                AppConfigurations.PasswordSecure);

            var groupExists = context.Web.GroupExists(request.GroupName);

            if (!groupExists)
            {
                context.Web.AddGroup(request.GroupName, request.GroupDescription, true);
            }

            return Task.CompletedTask;
        }
    }
}