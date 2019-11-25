using AbhsChinese.Admin.App_Start.Filter;
using AbhsChinese.Admin.Helpers;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Enum;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    [LoginCookie]
    public class BaseController : Controller
    {
        public BaseController()
        {
            var user = LoginCookieHelper.GetCurrentUser();
            if (user != null)
            {
                this.CurrentUser = LoginCookieHelper.GetCurrentUser();
                this.CurrentUserID = CurrentUser.UserId;
            }

        }
        public int CurrentUserID { get; private set; }

        public Models.CookieUserModel CurrentUser { get; private set; }

        protected override void OnException(ExceptionContext filterContext)
        {
            LogHelper.ErrorLog("", filterContext.Exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = 200;

            base.OnException(filterContext);

            JsonSimpleResponse response = new JsonSimpleResponse();
            if (filterContext.Exception is AbhsException && (filterContext.Exception as AbhsException).ErrorCode < 10000)
            {
                response.ErrorCode = (filterContext.Exception as AbhsException).ErrorCode;
                response.ErrorMsg = (filterContext.Exception as AbhsException).Message;
            }
            else
            {
                response.ErrorCode = 10000;
                response.ErrorMsg = "出错了，请联系管理员";
            }

            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                //do something
                filterContext.HttpContext.Response.Headers.Add(AbhsProtocolConst.ERROR_HEADER_NAME, response.ErrorCode.ToString());
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = new JsonResult() { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                if (IsNoLoginError(filterContext))
                {
                    filterContext.Result = new RedirectResult(Url.Action("index", "Login"));
                }
                else
                {
                    filterContext.Result = new RedirectResult(Url.Action("index", "error"));
                }
            }
        }
        private bool IsNoLoginError(ExceptionContext filterContext)
        {
            return filterContext.Exception is AbhsException && (filterContext.Exception as AbhsException).ErrorCode == (int)ErrorCodeEnum.NotHasLoginInfo;
        }
    }
}