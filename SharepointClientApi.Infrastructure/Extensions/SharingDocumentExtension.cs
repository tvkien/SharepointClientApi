using Microsoft.SharePoint.Client;
using SharepointClientApi.Domains;

namespace SharepointClientApi.Infrastructure.Extensions
{
    public static class SharingDocumentExtension
    {
        public static ExternalSharingDocumentOption ExternalSharing(this SharingDocumentOption option)
            => option switch
            {
                SharingDocumentOption.Edit => ExternalSharingDocumentOption.Edit,
                SharingDocumentOption.View => ExternalSharingDocumentOption.View,
                _ => ExternalSharingDocumentOption.View
            };
    }
}