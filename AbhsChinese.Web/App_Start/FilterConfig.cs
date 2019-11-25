using AbhsChinese.Web.App_Start.Filter;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new LoginAuthorizationFilter());
        }
    }
}
