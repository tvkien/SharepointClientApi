using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace SharepointClientApi.Dependencies
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        protected IServiceProvider ServiceProvider { get; set; }

        public DefaultDependencyResolver(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IDependencyScope BeginScope() 
            => new DefaultDependencyResolver(ServiceProvider.CreateScope().ServiceProvider);

        public object GetService(Type serviceType) => ServiceProvider.GetService(serviceType);

        public IEnumerable<object> GetServices(Type serviceType) => ServiceProvider.GetServices(serviceType);

        public void Dispose()
        {
        }
    }
}