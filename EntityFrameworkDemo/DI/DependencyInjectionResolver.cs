using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EntityFrameworkDemo.BLL;
using EntityFrameworkDemo.Controllers;
using EntityFrameworkDemo.DAL;
using EntityFrameworkDemo.EF;
using EntityFrameworkDemo.Log;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace EntityFrameworkDemo.DI
{

    internal class DependencyInjectionResolver : IDependencyResolver
    {
        private static readonly ServiceProvider _provider;

        static DependencyInjectionResolver()
        {
            var service = new ServiceCollection();

            service.AddScoped<LogAdapter, LogAdapter>();
            
            service.AddTransient<HomeController>();
            service.AddTransient<CountryController>();

            service.AddScoped<ICountryBLL,CountryBLL>();

            service.AddScoped<ICountryDAL, CountryDAL>();
            
            service.AddScoped<DemoDbContext>( s => DemoDbContext.Create());
            
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