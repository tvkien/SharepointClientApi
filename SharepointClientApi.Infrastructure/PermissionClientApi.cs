using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using SharepointClientApi.Domains;
using SharepointClientApi.Domains.Abstractions;
using SharepointClientApi.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace SharepointClientApi.Infrastructure
{
    public class PermissionClientApi : IPermissionClientApi
    {
        public Task AddGroupAsync(GroupPermissionRequest request)
        {
            using var context = GetSharePointOnlineContext(request.SiteUrl);
            var groupExists = context.Web.GroupExists(request.GroupName);

            if (!groupExists)
            {
                context.Web.AddGroup(request.GroupName, request.GroupDescription, true);
            }

            return Task.CompletedTask;
        }

        public Task AddPermissionLevelToGroupAsync(GroupPermissionRequest request)
        {
            using var context = GetSharePointOnlineContext(request.SiteUrl);

            context.Web.AddPermissionLevelToGroup(request.GroupName, request.Role.GetRoleType());
            return Task.CompletedTask;
        }

        public Task AddPermissionLevelToUserAsync(GroupPermissionRequest request)
        {
            using var context = GetSharePointOnlineContext(request.SiteUrl);

            foreach (var user in request.Users)
            {
                context.Web.AddPermissionLevelToUser(user, request.Role.GetRoleType());
            }

            return Task.CompletedTask;
        }

        public Task AddUserToGroupAsync(GroupPermissionRequest request)
        {
            using var context = GetSharePointOnlineContext(request.SiteUrl);

            foreach (var user in request.Users)
            {
                context.Web.AddUserToGroup(request.GroupName, user);
            }

            return Task.CompletedTask;
        }

        public Task BreakPermissionsAsync(string siteUrl, string listName)
        {
            using var context = GetSharePointOnlineContext(siteUrl);
            var list = context.Web.Lists.GetByTitle(listName);
            list.BreakRoleInheritance(false, true);
            return Task.CompletedTask;
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