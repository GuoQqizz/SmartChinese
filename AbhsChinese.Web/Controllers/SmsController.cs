using AbhsChinese.Code.Common.SMS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abhs.Service.Client;
using Abhs.Service.Client.Results;
using Abhs.Service.Client.ShortMessages;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Code.Common;
using AbhsChinese.Bll.AccessCheckPolicy;

namespace AbhsChinese.Web.Controllers
{
    [AllowAnonymous]
    public class SmsController : BaseController
    {
        [HttpPost]
        public ActionResult SendValidCode(string phone, string authCode, StuAcountVerifyTypeEnum type, bool isValidate = false)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.PhoneInvalid) };
            }

            if (isValidate)
            {
                LogonUserAccessCheckPolicy checkPolicy = new LogonUserAccessCheckPolicy(
                AccessCheckKeyEnum.Chinese_UserSMSCod_Check,
                phone,//GetCurrentUser().StudentId.ToString()
                50,
                1);
                if (!checkPolicy.Check())
                {
                    return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.SendSmsTooOften) };
                }
            }
            else
            {
                if (!VerifyCode.Check(authCode))
                {
                    return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.AuthCodeError) };
                }
            }

            int code = new Random().Next(1001, 9999);
            try
            {
                Result result = SendSms(phone, code.ToString(), type);
                if (result.State)
                {
                    SmsCookie.SetSmsCode(phone, code);
                    return new JsonResult() { Data = AjaxResponse.Success() };//status:1
                }
                else
                {
                    LogHelper.WriteLog("Code：" + result.Code + " Msg：" + result.Message);
                    return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.SystemError) };
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("", ex);
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.SystemError) };
            }
        }

        private Result SendSms(string phone, string code, StuAcountVerifyTypeEnum type)
        {
            Result r = new Result();
            if (type == StuAcountVerifyTypeEnum.注册验证)
            {
                ShortMessageClient client = new ShortMessageClient(SmsServiceUrl());
                r = client.Send(new VerificationCodeShortMessage() { Code = code, PhoneNumbers = phone });
            }
            else if (type == StuAcountVerifyTypeEnum.登录验证)
            {
                ShortMessageClient client = new ShortMessageClient(SmsServiceUrl());
                r = client.Send(new CommonShortMessage()
                {
                    PhoneNumbers = phone,
                    TemplateCode = ConfigurationManager.AppSettings["SmsTemplateCodeLogin"],
                    Parameters = new List<ShortMessageParameter>() {
                    new ShortMessageParameter
                    {
                        name="code",
                        value=code
                    }
                }
                });
            }
            else if (type == StuAcountVerifyTypeEnum.身份验证)
            {
                ShortMessageClient client = new ShortMessageClient(SmsServiceUrl());
                r = client.Send(new CommonShortMessage()
                {
                    PhoneNumbers = phone,
                    TemplateCode = ConfigurationManager.AppSettings["SmsTemplateCodeVerify"],
                    Parameters = new List<ShortMessageParameter>() {
                    new ShortMessageParameter
                    {
                        name="code",
                        value=code
                    }
                }
                });
            }
            return r;
        }

        private string SmsServiceUrl()
        {
            return ConfigurationManager.AppSettings["uploadUrl"];
        }
    }
}