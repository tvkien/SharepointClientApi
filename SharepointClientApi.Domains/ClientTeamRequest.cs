namespace SharepointClientApi.Domains
{
    public class ClientTeamRequest
    {
        public string SiteUrl { get; set; }

        public string DocumentName { get; set; }

        public string GroupName { get; set; }

        public Role Role { get; set; }

        public string[] Users { get; set; }
    }
}