using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Domain.Enum;
using AbhsChinese.School.App_Start.Filter;
using AbhsChinese.School.Helpers;
using AbhsChinese.School.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.School.Controllers
{
    [LoginCookie]
    public class BaseController : Controller
    {

        public CookieUserModel CurrentUser { get; private set; } = LoginCookieHelper.GetCurrentUser();

        protected override void OnException(ExceptionContext filterContext)
        {
            LogHelper.ErrorLog("", filterContext.Exception);

            filterContext.ExceptionHandled = true;

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
                //filterContext.Result = new Json(new JsonSimpleResponse() { State = false, ErrorMsg = "服务器异常" });

                filterContext.HttpContext.Response.Headers.Add(AbhsProtocolConst.ERROR_HEADER_NAME, response.ErrorCode.ToString());
                filterContext.HttpContext.Response.StatusCode = 500;
                //filterContext.HttpContext.Response.Write(JsonConvert.SerializeObject(response));

                //filterContext.ExceptionHandled = true;
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


        public ActionResult SimpleResult(bool status, string msg = "")
        {
            if (!msg.HasValue())
            {
                msg = status ? "操作成功" : "操作失败";
            }
            return Json(new JsonSimpleResponse() { State = status, ErrorMsg = msg });
        }

        public void CheckUpdateEntity(object o)
        {
            if (o == null)
            {
                throw new AbhsException(ErrorCodeEnum.ParameterInvalid, AbhsErrorMsg.ConstParameterInvalid);
            }
        }

        private bool IsNoLoginError(ExceptionContext filterContext)
        {
            return filterContext.Exception is AbhsException && (filterContext.Exception as AbhsException).ErrorCode == (int)ErrorCodeEnum.NotHasLoginInfo;
        }
    }
}