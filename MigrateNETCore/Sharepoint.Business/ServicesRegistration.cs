using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sharepoint.Business.Interfaces;
using Sharepoint.Business.Implements;
using Sharepoint.Business.Models;

namespace Sharepoint.Business
{
    public static class ServicesRegistration
    {
        public static void RegisterServices(
            this IServiceCollection services,
            IConfiguration configuration,
            string sectionAzureSetting,
            string sectionSpoSetting)
        {
            services.AddSingleton(provider
                => configuration.GetSection(sectionAzureSetting).Get<AzureSetting>());
            services.AddSingleton(provider
                => configuration.GetSection(sectionSpoSetting).Get<SpoSetting>());
            services.AddHttpClient();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<ISiteCollectionManager, SiteCollectionManager>();
        }
    }
}