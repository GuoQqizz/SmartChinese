using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Code.Common.SMS
{
    public class SMSAccessSecurityByGlobalLevel2 : ISMSAccessSecurityCheck
    {
        private const int GlobalLevel2Limit = 3000;
        public enumSMSCheckResult Check()
        {
            SMSAccessCacheObject obj = SMSAccessCache.GetGlobalAccessLevel2InLimit();
            if (obj.Count <= SMSAccessSecurityByGlobalLevel2.GlobalLevel2Limit)
            {
                return enumSMSCheckResult.Normal;
            }
            return enumSMSCheckResult.GlobalLimited;
        }
    }
}