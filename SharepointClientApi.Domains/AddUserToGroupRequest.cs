namespace SharepointClientApi.Domains
{
    public class AddUserToGroupRequest
    {
        public string SiteUrl { get; set; }

        public string GroupName { get; set; }

        public string[] Users { get; set; }
    }
}