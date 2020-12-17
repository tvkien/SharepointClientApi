using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using SharepointClientApi.Grpah.Implements;
using SharepointClientApi.Grpah.Interfaces;

namespace SharepointClientApi.Grpah
{
    public static class ServiceRegistration
    {
        public static void AddAzureInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddSingleton(provider => config.GetSection("AzureSetting").Get<AzureSetting>());
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IGraphServiceClient, GraphServiceClient>();
            services.AddScoped<IAuthenticationProvider, GraphAuthenticationProvider>();
        }
    }
}