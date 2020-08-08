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

            context.Web.AddPermissionLevelToGroup(request.GroupName, request.Role.GetRoleType());

            return Task.CompletedTask;
        }

        public Task AddPermissionLevelToUserAsync(AddPermissionLevelToUserRequest request)
        {
            using var authenticationManager = new AuthenticationManager();
            using var context = authenticationManager.GetSharePointOnlineAuthenticatedContextTenant(
                request.SiteUrl,
                AppConfigurations.SpoUserAdmin,
                AppConfigurations.PasswordSecure);

            context.Web.AddPermissionLevelToUser(request.User, request.Role.GetRoleType());
            return Task.CompletedTask;
        }

        public Task AddUserToGroupAsync(AddUserToGroupRequest request)
        {
            using var authenticationManager = new AuthenticationManager();
            using var context = authenticationManager.GetSharePointOnlineAuthenticatedContextTenant(
                request.SiteUrl,
                AppConfigurations.SpoUserAdmin,
                AppConfigurations.PasswordSecure);

            foreach(var user in request.Users)
            {
                context.Web.AddUserToGroup(request.GroupName, user);
            }

            return Task.CompletedTask;
        }

        public Task BreakPermissionsAsync(string siteUrl, string listName)
        {
            using var authenticationManager = new AuthenticationManager();
            using var context = authenticationManager.GetSharePointOnlineAuthenticatedContextTenant(
                siteUrl,
                AppConfigurations.SpoUserAdmin,
                AppConfigurations.PasswordSecure);

            var list = context.Web.Lists.GetByTitle(listName);
            list.BreakRoleInheritance(false, true);
            return Task.CompletedTask;
        }
    }
}