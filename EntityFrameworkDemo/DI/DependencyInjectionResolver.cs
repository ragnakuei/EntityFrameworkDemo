using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using EntityFrameworkDemo.BLL;
using EntityFrameworkDemo.BLL.IBLL;
using EntityFrameworkDemo.Controllers;
using EntityFrameworkDemo.DAL;
using EntityFrameworkDemo.DAL.IDAL;
using EntityFrameworkDemo.EF;
using EntityFrameworkDemo.Log;
using EntityFrameworkDemo.Models.Shared;
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
            service.AddTransient<CountryController>();
            service.AddTransient<CountyController>();
            service.AddTransient<ICountryBLL, CountryBLL>();
            service.AddTransient<ICountyBLL, CountyBLL>();
            service.AddTransient<ICountryDAL, CountryDAL>();
            service.AddTransient<ICountyDAL, CountyDAL>();
            service.AddTransient<UserInfo, UserInfo>(s => new UserInfo(HttpContext.Current));

            service.AddScoped<LogAdapter, LogAdapter>();
            service.AddScoped<DemoDbContext>(s => DemoDbContext.Create());

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