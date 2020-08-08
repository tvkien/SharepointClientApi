using Microsoft.SharePoint.Client;
using SharepointClientApi.Domains;

namespace SharepointClientApi.Infrastructure.Extensions
{
    public static class RoleExtension
    {
        public static RoleType GetRoleType(this Role role)
            => role switch
            {
                Role.None => RoleType.None,
                Role.FullControl => RoleType.Administrator,
                Role.Design => RoleType.WebDesigner,
                Role.Edit => RoleType.Editor,
                Role.Contribute => RoleType.Contributor,
                Role.Read => RoleType.Reader,
                _ => RoleType.None,
            };
    }
}