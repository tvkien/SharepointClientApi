namespace SharepointClientApi.Domains
{
    public class AddPermissionLevelToUserRequest
    {
        public string SiteUrl { get; set; }

        public string User { get; set; }

        public Role Role { get; set; }
    }
}