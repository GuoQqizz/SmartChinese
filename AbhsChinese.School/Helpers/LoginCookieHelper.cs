using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Domain.Enum;
using AbhsChinese.School.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.School.Helpers
{
    public static class LoginCookieHelper
    {
        public static string CookieKey = "SchoolLoginInfo";
        public static CookieUserModel GetCurrentUser()
        {
            try
            {
                var result = string.IsNullOrWhiteSpace(WebHelper.GetCookie(CookieKey)) ? null : JsonConvert.DeserializeObject<CookieUserModel>(Encrypt.DecryptQueryString(WebHelper.GetCookie(CookieKey)));
                //TODO 正式删除下面代码
                //if (result == null)
                //{
                //    result = new CookieUserModel();
                //    result.Teacher = new DtoSchoolTeacher() { Yoh_Id = 10005, Yoh_Phone = "13800000000", Yoh_SchoolId = 10000, Yoh_Name="测试教师", Yoh_Avatar= "/SmartChinese/Images/3d05ae77ea1649f3abd8e16e27f281ef.jpg" };
                //    result.School = new DtoSchool() { Bsl_Id = 10000, Bsl_SchoolName = "校区1", Bsl_Level = 10000, Bhl_DividePercent = 30, LevelName = "A级校区" };
                //    SetCurrentUser(result);
                //}
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