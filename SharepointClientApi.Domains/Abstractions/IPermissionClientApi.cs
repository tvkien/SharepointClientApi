using System.Threading.Tasks;

namespace SharepointClientApi.Domains.Abstractions
{
    public interface IPermissionClientApi
    {
        Task AddGroupAsync(AddGroupRequest request);

        Task AddUserToGroupAsync(AddUserToGroupRequest request);

        Task AddPermissionLevelToUserAsync(AddPermissionLevelToUserRequest request);

        Task BreakPermissionsAsync(string siteUrl, string listName);
    }
}