using AbhsChinese.Admin.Helpers;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Admin.App_Start.Filter
{
    public class LoginCookieAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var teacher = LoginCookieHelper.GetCurrentUser();
            if (teacher == null)
            {
                if (filterContext.HttpContext.Request.Url != null)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        FilterContextAjaxNoLogin(filterContext);
                    }
                    else
                    {
                        FilterContextNoLogin(filterContext);
                    }
                }
                return;
            }
        }



        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            //将当前用户信息注册到ViewBag中
            //var result = filterContext.Result;
            //if (result != null && result is ViewResult && ((ViewResult)result).ViewBag.CurrentUser == null)
            //{
            //    ((ViewResult)result).ViewBag.CurrentUser = LoginCookieHelper.GetCurrentUser();
            //}
        }
        /// <summary>
        /// Ajax请求
        /// </summary>
        /// <param name="filterContext"></param>
        private void FilterContextAjaxNoLogin(ActionExecutingContext filterContext)
        {
            throw new AbhsException(ErrorCodeEnum.NotHasLoginInfo, Domain.AbhsResource.AbhsErrorMsg.ConstNotHasLoginInfo);
        }
        /// <summary>
        /// 未登录
        /// </summary>
        /// <param name="filterContext"></param>
        private void FilterContextNoLogin(ActionExecutingContext filterContext)
        {
            //filterContext.HttpContext.Response.Write($@"<script>alert('您还登录，请先登录!');window.top.location.href='/Login/Index';</script>");
            filterContext.HttpContext.Response.Write($@"<script>window.top.location.href='/Login/Index';</script>");
            filterContext.Result = new EmptyResult();
            filterContext.HttpContext.Response.End();
        }
    }
}