using System.IO;

namespace SharepointClientApi.Domains
{
    public class UploadFileRequest
    {
        public string SiteUrl { get; set; }

        public string ListName { get; set; }

        public string FileName { get; set; }

        public Stream Content { get; set; }
    }
}