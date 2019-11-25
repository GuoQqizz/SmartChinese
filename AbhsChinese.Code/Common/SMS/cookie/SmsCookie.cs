using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Code.Common.SMS
{
    public class SmsCookie
    {
        public static SmsModel GetSmsCode
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["SmsCookieCode"] == null ||
                    HttpContext.Current.Request.Cookies["SmsCookieCode"].Value == "")
                {
                    return null;
                }
                string value = HttpContext.Current.Request.Cookies["SmsCookieCode"] != null ?
                    Encrypt.DecryptQueryString(HttpContext.Current.Request.Cookies["SmsCookieCode"].Value) : "";

                SmsModel model = JsonConvert.DeserializeObject<SmsModel>(value);
                return model;
            }
        }

        public static void SetSmsCode(string phone, int code)
        {
            SmsModel model = new SmsModel();
            model.Code = code;
            model.CreateTime = DateTime.Now.ToString();
            model.Phone = phone;
            HttpContext.Current.Response.Cookies["SmsCookieCode"].Value = Encrypt.EncryptQueryString(JsonConvert.SerializeObject(model));
        }

        public static void RemoveSmsCode()
        {
            HttpContext.Current.Response.Cookies["SmsCookieCode"].Value = null;
        }
    }
}