namespace SharepointClientApi.Domains
{
    public class GroupPermissionRequest
    {
        public string SiteUrl { get; set; }

        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public string[] Users { get; set; }

        public Role Role { get; set; }
    }
}