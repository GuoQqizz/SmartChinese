using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbhsChinese.Code.Common;

namespace AbhsChinese.Web
{
    public class AjaxResponse
    {
        public int State
        {
            set;get;
        }

        public SmsErrorEnum ErrorCode
        {
            set;get;
        }

        public string ErrorMsg
        {
            set;get;
        }

        public static AjaxResponse Success()
        {
            return new AjaxResponse() { State = 1 };
        }

        public static AjaxResponse Fail(SmsErrorEnum errorCode)
        {
            return new AjaxResponse() { State = -1, ErrorCode = errorCode, ErrorMsg = errorCode.Description() };
        }
    }
}