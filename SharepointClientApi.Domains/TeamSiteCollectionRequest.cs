namespace SharepointClientApi.Domains
{
    public class TeamSiteCollectionRequest
    {
        public string Alias { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string[] Owners { get; set; }

        public Language Language { get; set; }
    }
}