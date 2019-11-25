using System.Web.Mvc;
using System.Web.Routing;

namespace AbhsChinese.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "LoginIndex", id = UrlParameter.Optional },
                namespaces: new string[] { "AbhsChinese.Web.Controllers" }
            );
        }
    }
}
