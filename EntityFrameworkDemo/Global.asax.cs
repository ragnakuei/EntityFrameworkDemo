using System;
using System.Data.Entity;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EntityFrameworkDemo.DI;
using EntityFrameworkDemo.EF;
using EntityFrameworkDemo.Helpers;
using NLog;

namespace EntityFrameworkDemo
{
    public class MvcApplication : HttpApplication
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
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture   = ci;
            }

            HttpContext.Current.Items["CurrentLanguage"] = ci.ToString();
        }
    }
}