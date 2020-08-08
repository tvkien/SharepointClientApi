using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace SharepointClientApi.Dependencies
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddControllersAsServices(
            this IServiceCollection services,
            IEnumerable<Type> serviceTypes)
        {
            foreach (var type in serviceTypes)
            {
                services.AddTransient(type);
            }

            return services;
        }
    }
}