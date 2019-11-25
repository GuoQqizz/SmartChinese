using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Code.Common.SMS
{
    public class SMSAccessSecurityByUserLevel2 : ISMSAccessSecurityCheckUser
    {
        public string OpenId
        {
            set; get;
        }

        private const int UserAccessLimit2 = 8;

        public enumSMSCheckResult Check()
        {
            object lockObj = SMSAccessCache.GetUserAccessInLimit2Lock(OpenId);
            if (lockObj != null)
            {
                return enumSMSCheckResult.UserLimited;
            }

            SMSAccessCacheObject obj = SMSAccessCache.GetUserAccessInLimit2(OpenId);
            if (obj.Count <= SMSAccessSecurityByUserLevel2.UserAccessLimit2)
            {
                return enumSMSCheckResult.Normal;
            }
            else
            {
                SMSAccessCache.SetUserAccessInLimit2Lock(OpenId);
                return enumSMSCheckResult.UserLimited;
            }
        }
    }
}