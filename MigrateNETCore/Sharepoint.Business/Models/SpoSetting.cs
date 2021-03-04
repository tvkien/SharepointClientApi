namespace Sharepoint.Business.Models
{
    public class SpoSetting
    {
        public string SiteUrl { get; set; }

        public string SiteUrlAdmin => GetSiteUrlAdmin();

        public string UserName { get; set; }

        public string Password { get; set; }

        private string GetSiteUrlAdmin()
        {
            var arrString = SiteUrl.Split(".");
            arrString[0] += "-admin";
            string siteUriAdmin = string.Join(".", arrString);
            return siteUriAdmin;
        }
    }
}