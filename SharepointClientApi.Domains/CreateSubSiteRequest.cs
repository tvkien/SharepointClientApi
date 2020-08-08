namespace SharepointClientApi.Domains
{
    public class CreateSubSiteRequest
    {
        public string SiteUrl { get; set; }

        public string Alias { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }
    }
}