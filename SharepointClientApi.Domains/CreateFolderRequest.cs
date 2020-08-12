namespace SharepointClientApi.Domains
{
    public class CreateFolderRequest
    {
        public string SiteUrl { get; set; }

        public string ListName { get; set; }

        public string FolderName { get; set; }

        public bool IsSharing { get; set; }

        public string TargerEmailToShare { get; set; }

        public SharingDocumentOption SharingOption { get; set; }
    }
}