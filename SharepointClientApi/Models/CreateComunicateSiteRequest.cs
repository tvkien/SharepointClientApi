using SharepointClientApi.Domains;
using System.ComponentModel.DataAnnotations;

namespace SharepointClientApi.Models
{
    public class CreateComunicateSiteRequest
    {
        [Required]
        public string Alias { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }

        public CommunicationSiteRequest CommunicationSiteRequest()
            => new CommunicationSiteRequest
            {
                Alias = Alias,
                Title = Title,
                Description = Description,
                Language = Language
            };
    }
}