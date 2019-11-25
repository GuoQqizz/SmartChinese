using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Web.App_Start.Filter
{
    public class LoginAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //判断是否跳过授权过滤器
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
               || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            var cookie = HttpContext.Current.Request.Cookies["LoginInfo"];
            if (cookie == null)
            {
                filterContext.HttpContext.Response.Write($@"<script>window.top.location.href='/Login/LoginIndex';</script>");
               // filterContext.Result//new RedirectResult("/Login/LoginIndex");
                HttpContext.Current.Response.End();
            }
        }
    }
}