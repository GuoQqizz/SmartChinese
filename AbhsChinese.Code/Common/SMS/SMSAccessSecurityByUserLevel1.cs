using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Code.Common.SMS
{
    public class SMSAccessSecurityByUserLevel1 : ISMSAccessSecurityCheckUser
    {
        public string OpenId
        {
            set; get;
        }

        public enumSMSCheckResult Check()
        {
            if (SMSAccessCache.GetUserAccessInLimit1(OpenId))
            {
                return enumSMSCheckResult.UserLimited;
            }
            return enumSMSCheckResult.Normal;
        }
    }
}