using AbhsChinese.Code.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StudentInfo
{
    public static class LoginStudent
    {
        /// <summary>
        /// 登录信息
        /// </summary>
        public static string Info
        {
            get
            {
                return string.IsNullOrWhiteSpace(WebHelper.GetCookie("LoginInfo")) ? "" : Encrypt.DecryptQueryString(WebHelper.GetCookie("LoginInfo"));
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    WebHelper.WriteCookie("LoginInfo", Encrypt.EncryptQueryString(value));
                }
                else
                {
                    WebHelper.RemoveCookie("LoginInfo");
                }
            }
        }

        /// <summary>
        /// 获取当前数据信息
        /// </summary>
        /// <returns></returns>
        public static LoginInfo GetCustomerUser()
        {
            return JsonConvert.DeserializeObject<LoginInfo>(Info);
        }
    }
}