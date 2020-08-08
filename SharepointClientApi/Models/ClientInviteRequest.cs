using SharepointClientApi.Domains;
using System.ComponentModel.DataAnnotations;

namespace SharepointClientApi.Models
{
    public class ClientInviteRequest
    {
        [Required]
        public string Alias { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }

        public string DocumentName { get; set; }

        public string GroupName { get; set; }

        public Role Role { get; set; }

        public string[] Users { get; set; }
    }
}