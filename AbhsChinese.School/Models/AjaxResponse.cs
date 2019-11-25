using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.School.Models
{
    /// <summary>
    /// 仅短信验证码用
    /// </summary>
    public class AjaxResponse
    {
        public int Status
        {
            set; get;
        }

        public SmsErrorEnum ErrorCode
        {
            set; get;
        }

        public string ErrorMsg
        {
            set; get;
        }

        public static AjaxResponse Success()
        {
            return new AjaxResponse() { Status = 1 };
        }

        public static AjaxResponse Fail(SmsErrorEnum errorCode)
        {
            return new AjaxResponse() { Status = -1, ErrorCode = errorCode, ErrorMsg = errorCode.Description() };
        }
    }
}