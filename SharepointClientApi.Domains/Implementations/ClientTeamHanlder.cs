using SharepointClientApi.Domains.Abstractions;
using System.Threading.Tasks;

namespace SharepointClientApi.Domains.Implementations
{
    public class ClientTeamHanlder : IClientTeamHanlder
    {
        private readonly IListClientApi listClientApi;
        private readonly IPermissionClientApi permissionClientApi;
        private readonly ISiteCreation siteCreation;
        private readonly IFileClientApi fileClientApi;

        public ClientTeamHanlder(
            IListClientApi listClientApi,
            IPermissionClientApi permissionClientApi,
            ISiteCreation siteCreation,
            IFileClientApi fileClientApi)
        {
            this.listClientApi = listClientApi;
            this.permissionClientApi = permissionClientApi;
            this.siteCreation = siteCreation;
            this.fileClientApi = fileClientApi;
        }

        public async Task ExternalSharingAsync(ClientTeamRequest request)
        {
            var siteCollection = await siteCreation.CreateTeamSiteNoGroupAsync(
                new TeamSiteNoGroupRequest
                {
                    Alias = request.SiteUrl,
                    Title = request.SiteUrl,
                    Language = Language.English,
                    Owner = AppConfigurations.SpoUserAdmin
                });

            var group = new GroupPermissionRequest
            {
                SiteUrl = siteCollection,
                GroupName = $"{request.SiteUrl} Clients",
                GroupDescription = "This is group for Client",
                Users = new[] { "tvkien@tvkien.onmicrosoft.com" },
            };

            await permissionClientApi.AddGroupAsync(group);
            await permissionClientApi.AddUserToGroupAsync(group);

            await fileClientApi.CreateFolderAsync(new CreateFolderRequest
            {
                SiteUrl = siteCollection,
                ListName = "Documents",
                FolderName = "Documents Client",
                IsSharing = true,
                TargerEmailToShare = group.GroupName,
                SharingOption = SharingDocumentOption.View
            });
        }

        public async Task HandleAsync(ClientTeamRequest request)
        {
            await listClientApi.CreateDocumentLibraryAsync(new CreateDocumentLibraryRequest
            {
                SiteUrl = request.SiteUrl,
                ListName = request.DocumentName
            });

            await permissionClientApi.BreakPermissionsAsync(request.SiteUrl, request.DocumentName);
            await permissionClientApi.AddGroupAsync(new GroupPermissionRequest
            {
                SiteUrl = request.SiteUrl,
                GroupName = request.GroupName,
                Role = request.Role,
                GroupDescription = request.GroupName
            });

            await permissionClientApi.AddUserToGroupAsync(new GroupPermissionRequest
            {
                SiteUrl = request.SiteUrl,
                GroupName = request.GroupName,
                Users = request.Users
            });
        }
    }
}