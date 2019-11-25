using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Code.Common.SMS
{
    public class SMSAccessSecurityByGlobalLevel1 : ISMSAccessSecurityCheck
    {
        private const int GlobalLevel1Limit = 1000;
        public enumSMSCheckResult Check()
        {
            SMSAccessCacheObject obj = SMSAccessCache.GetGlobalAccessLevel1InLimit();
            if (obj.Count <= SMSAccessSecurityByGlobalLevel1.GlobalLevel1Limit)
            {
                return enumSMSCheckResult.Normal;
            }
            return enumSMSCheckResult.GlobalLimited;
        }
    }
}