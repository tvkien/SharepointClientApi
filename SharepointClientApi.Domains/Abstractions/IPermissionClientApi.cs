using System.Threading.Tasks;

namespace SharepointClientApi.Domains.Abstractions
{
    public interface IPermissionClientApi
    {
        Task AddGroupAsync(GroupPermissionRequest request);

        Task AddUserToGroupAsync(GroupPermissionRequest request);

        Task AddPermissionLevelToUserAsync(GroupPermissionRequest request);

        Task AddPermissionLevelToGroupAsync(GroupPermissionRequest request);

        Task BreakPermissionsAsync(string siteUrl, string listName);
    }
}