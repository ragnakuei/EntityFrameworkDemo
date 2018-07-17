using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EntityFrameworkDemo.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkDemo.DI
{

    internal class DependencyInjectionResolver : IDependencyResolver
    {
        private static readonly ServiceProvider _provider;

        static DependencyInjectionResolver()
        {
            var service = new ServiceCollection();
            service.AddTransient<HomeController>();
            //service.AddScoped<ITest>(t => new Test());
            //service.AddScoped<ITest, Test>();
            _provider = service.BuildServiceProvider();
        }

        public object GetService(Type serviceType)
        {
            return _provider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _provider.GetServices(serviceType);
        }
    }
}