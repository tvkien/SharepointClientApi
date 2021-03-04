using Microsoft.SharePoint.Client;

namespace Sharepoint.Business.Requests
{
    public class SiteCollectionRequest
    {
        public string Alias { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }

        public string Owner { get; set; }
    }
}