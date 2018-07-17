using System.Web.Mvc;
using System.Web.Routing;
using EntityFrameworkDemo.DI;

namespace EntityFrameworkDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolver.SetResolver(new DependencyInjectionResolver());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
