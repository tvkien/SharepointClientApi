using Microsoft.Extensions.DependencyInjection;
using SharepointClientApi.Domains.Abstractions;
using SharepointClientApi.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SharepointClientApi.Dependencies
{
    public class Startup
    {
        public static void Bootstrapper(HttpConfiguration config)
            => config.DependencyResolver = new DefaultDependencyResolver(Configuration());

        private static IServiceProvider Configuration()
        {
            var services = new ServiceCollection();

            services.AddScoped<IGraphClientApi, GraphClientApi>();
            services.AddScoped<ISiteCreation, SiteCreation>();

            services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
                .Where(x => !x.IsAbstract && !x.IsGenericTypeDefinition)
                .Where(x => typeof(IHttpController).IsAssignableFrom(x) || x.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));

            return services.BuildServiceProvider();
        }
    }
}