namespace Sharepoint.Business.Models
{
    public class AzureSetting
    {
        public string Instance { get; set; }

        public string TenantId { get; set; }

        public string TokenEnpoint => $"{Instance}/{TenantId}/oauth2/v2.0/token";

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}