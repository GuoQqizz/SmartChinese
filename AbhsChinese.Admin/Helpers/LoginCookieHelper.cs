using AbhsChinese.Admin.Models;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Helpers
{
    public static class LoginCookieHelper
    {
        public static string CookieKey = "AdminLoginInfo";
        public static CookieUserModel GetCurrentUser()
        {
            try
            {
                var result = string.IsNullOrWhiteSpace(WebHelper.GetCookie(CookieKey)) ? null : JsonConvert.DeserializeObject<CookieUserModel>(Encrypt.DecryptQueryString(WebHelper.GetCookie(CookieKey)));
                return result;
            }
            catch (Exception)
            {
                //throw new AbhsException(ErrorCodeEnum.NotHasLoginInfo, AbhsErrorMsg.ConstNotHasLoginInfo);
                return null;
            }
        }

        public static void SetCurrentUser(CookieUserModel user)
        {
            if (user != null)
            {
                WebHelper.WriteCookie(CookieKey, Encrypt.EncryptQueryString(Newtonsoft.Json.JsonConvert.SerializeObject(user)));
            }
            else
            {
                WebHelper.RemoveCookie(CookieKey);
            }
        }
    }
}