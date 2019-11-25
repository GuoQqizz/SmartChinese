using Abhs.Service.Client;
using Abhs.Service.Client.Results;
using Abhs.Service.Client.ShortMessages;
using AbhsChinese.Bll.AccessCheckPolicy;
using AbhsChinese.Bll.Account;
using AbhsChinese.Code.Common;
using AbhsChinese.Code.Common.SMS;
using AbhsChinese.Domain.Enum;
using AbhsChinese.School.App_Start.Filter;
using AbhsChinese.School.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.School.Controllers
{
    public class SmsController : BaseController
    {

        AccountBll accountBll = new AccountBll();
        [HttpPost]
        [LoginCookie(false)]
        public ActionResult SendValidCode(string phone, AccountVerifyTypeEnum type, string authCode = "")
        {
            bool checkAuthCode = true;
            bool checkPhone = false;
            if (string.IsNullOrEmpty(phone))
            {
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.PhoneInvalid) };
            }
            if (type == AccountVerifyTypeEnum.校区找回密码)
            {

                checkAuthCode = VerifyCode.Check(authCode);
                if (!checkAuthCode)
                {
                    return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.AuthCodeError) };
                }
            }
            if (checkAuthCode)
            {
                checkPhone = accountBll.CheckPhone(phone, type);
            }
            if (!checkPhone)
            {
                SmsErrorEnum errorCode = SmsErrorEnum.PhoneInvalid;
                switch (type)
                {
                    case AccountVerifyTypeEnum.学生未注册:
                    case AccountVerifyTypeEnum.校区教师未注册:
                    case AccountVerifyTypeEnum.教研教师未注册:
                        errorCode = SmsErrorEnum.PhoneRegistered;
                        break;
                    case AccountVerifyTypeEnum.学生已注册:
                    case AccountVerifyTypeEnum.校区教师已注册:
                    case AccountVerifyTypeEnum.校区找回密码:
                    case AccountVerifyTypeEnum.教研教师已注册:
                        errorCode = SmsErrorEnum.PhoneNoRegister;
                        break;
                    default:
                        break;
                }
                return new JsonResult() { Data = AjaxResponse.Fail(errorCode) };
            }
            if (type != AccountVerifyTypeEnum.校区找回密码)
            {
                LogonUserAccessCheckPolicy checkPolicy = new LogonUserAccessCheckPolicy(
                AccessCheckKeyEnum.Chinese_SchoolSMSCode_Check,
                CurrentUser.Teacher.Yoh_Id.ToString(),
                50,
                1,
                0);
                if (!checkPolicy.Check())
                {
                    return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.SendSmsTooOften) };
                }
            }


            //enumSMSCheckResult result = SMSAccessSecurityManager.Check(clsCommon.GetIP());

            //if (result == enumSMSCheckResult.UserLimited || result == enumSMSCheckResult.GlobalLimited)
            //{
            //    return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.SendSmsTooOften) };
            //}

            int code = new Random().Next(1001, 9999);
            //return new JsonResult() { Data = AjaxResponse.Success() };//status:1
            try
            {
                if (SendSms(phone, code.ToString(), type))
                {
                    SmsCookie.SetSmsCode(phone, code);
                    return new JsonResult() { Data = AjaxResponse.Success() };//status:1
                }
                else
                {
                    return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.SystemError) };
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("发短信error", ex);
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.SystemError) };
            }
        }

        private string GetSmsConfig(AccountVerifyTypeEnum type)
        {
            string key = "";
            switch (type)
            {
                case AccountVerifyTypeEnum.学生未注册:
                case AccountVerifyTypeEnum.校区教师未注册:
                case AccountVerifyTypeEnum.教研教师未注册:
                    key = ConfigurationManager.AppSettings["SmsTemplateCodeRegister"];
                    break;
                case AccountVerifyTypeEnum.学生已注册:
                case AccountVerifyTypeEnum.校区教师已注册:
                case AccountVerifyTypeEnum.教研教师已注册:
                case AccountVerifyTypeEnum.其他:
                case AccountVerifyTypeEnum.校区找回密码:
                default:
                    key = ConfigurationManager.AppSettings["SmsTemplateCodeVerify"];
                    break;
            }
            return key;

        }

        private bool SendSms(string phone, string code, AccountVerifyTypeEnum type)
        {
            string templateCode = GetSmsConfig(type);
            ShortMessageClient client = new ShortMessageClient(SmsServiceUrl());
            Result r = client.Send(new CommonShortMessage()
            {
                PhoneNumbers = phone,
                TemplateCode = templateCode,
                Parameters = new List<ShortMessageParameter>() {
                    new ShortMessageParameter() {
                        name = "code",
                        value = code
                    }
                }
            });
            return r.State;
        }

        private string SmsServiceUrl()
        {
            return ConfigurationManager.AppSettings["SmsServiceUrl"];
        }
    }
}