using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Web.App_Start.Filter;
using AbhsChinese.Web.Models.StudentInfo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    //[LoginAuthorizationFilter(true)]
    public class BaseController : Controller
    {
        private LoginInfo data;

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        protected virtual LoginInfo GetCurrentUser()
        {
            if (data == null)
            {
                data = JsonConvert.DeserializeObject<LoginInfo>(LoginStudent.Info);
            }
            return data;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            LogHelper.ErrorLog("", filterContext.Exception);

            filterContext.ExceptionHandled = true;

            base.OnException(filterContext);

            JsonSimpleResponse response = new JsonSimpleResponse();
            response.State = false;
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
                filterContext.HttpContext.Response.Headers.Add(AbhsProtocolConst.ERROR_HEADER_NAME, response.ErrorCode.ToString());
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = new JsonResult() { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                filterContext.Result = new RedirectResult(Url.Action("index", "error", new { code = response.ErrorCode, msg = response.ErrorMsg }));
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
    }
}