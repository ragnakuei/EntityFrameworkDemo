using System;
using System.Data.Entity;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EntityFrameworkDemo.DI;
using EntityFrameworkDemo.EF;
using EntityFrameworkDemo.Helpers;
using NLog;

namespace EntityFrameworkDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            Database.SetInitializer<DemoDbContext>(null);
            DependencyResolver.SetResolver(new DependencyInjectionResolver());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        var ci = new CultureInfo(CultureHelper.GetDefaultCulture());
        try
        {
            var userLanguages = Request.Cookies["_culture"]
                                       ?.Value;

            ci = string.IsNullOrWhiteSpace(userLanguages)
                     ? ci
                     : new CultureInfo(userLanguages);
        }
        catch (Exception ex)
        {
            _log.Error(ex.StackTrace);
            _log.Error(ex.Message);
        }
        finally
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            System.Threading.Thread.CurrentThread.CurrentCulture   = ci;
        }
    }
    }
}
