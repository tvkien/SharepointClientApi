using SharepointClientApi.Domains;
using SharepointClientApi.Domains.Abstractions;
using System.Threading.Tasks;

namespace SharepointClientApi.Infrastructure
{
    public class ClientTeamHanlder : IClientTeamHanlder
    {
        private readonly IListClientApi listClientApi;
        private readonly IPermissionClientApi permissionClientApi;

        public ClientTeamHanlder(
            IListClientApi listClientApi,
            IPermissionClientApi permissionClientApi)
        {
            this.listClientApi = listClientApi;
            this.permissionClientApi = permissionClientApi;
        }

        public async Task Handle(ClientTeamRequest request)
        {
            await listClientApi.CreateDocumentLibraryAsync(new CreateDocumentLibraryRequest
            {
                SiteUrl = request.SiteUrl,
                ListName = request.DocumentName
            });

            await permissionClientApi.BreakPermissionsAsync(request.SiteUrl, request.DocumentName);
            await permissionClientApi.AddGroupAsync(new AddGroupRequest
            {
                SiteUrl = request.SiteUrl,
                GroupName = request.GroupName,
                Role = request.Role,
                GroupDescription = request.GroupName
            });

            await permissionClientApi.AddUserToGroupAsync(new AddUserToGroupRequest
            {
                SiteUrl = request.SiteUrl,
                GroupName = request.GroupName,
                Users = request.Users
            });
        }
    }
}