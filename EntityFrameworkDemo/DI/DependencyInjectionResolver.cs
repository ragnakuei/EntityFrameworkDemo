using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EntityFrameworkDemo.BLL;
using EntityFrameworkDemo.BLL.IBLL;
using EntityFrameworkDemo.Controllers;
using EntityFrameworkDemo.DAL;
using EntityFrameworkDemo.DAL.IDAL;
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
            service.AddTransient<CountyController>();

            service.AddScoped<ICountryBLL,CountryBLL>();
            service.AddScoped<ICountyBLL,CountyBLL>();

            service.AddScoped<ICountryDAL, CountryDAL>();
            service.AddScoped<ICountyDAL, CountyDAL>();
            
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