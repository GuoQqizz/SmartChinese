using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Code.Common.SMS
{
    public class SMSAccessSecurityManager
    {
        static object lockObj = new object();
        public static bool ForceCheckObj = false;

        static List<ISMSAccessSecurityCheck> checkStrategys
        {
            set; get;
        }

        static SMSAccessSecurityManager()
        {
            checkStrategys = new List<ISMSAccessSecurityCheck>();
            checkStrategys.Add(new SMSAccessSecurityByUserLevel1());
            checkStrategys.Add(new SMSAccessSecurityByUserLevel2());
            //checkStrategys.Add(new SMSAccessSecurityByGlobalLevel1());
            //checkStrategys.Add(new SMSAccessSecurityByGlobalLevel2());
        }

        public static enumSMSCheckResult Check(string openId)
        {
            if (ForceCheckObj)
            {
                return enumSMSCheckResult.GlobalLimited;
            }

            lock (lockObj)
            {
                foreach (ISMSAccessSecurityCheck strategy in checkStrategys)
                {
                    if (strategy is ISMSAccessSecurityCheckUser)
                    {
                        ((ISMSAccessSecurityCheckUser)strategy).OpenId = openId;
                    }
                    enumSMSCheckResult result = strategy.Check();
                    if (result != enumSMSCheckResult.Normal)
                    {
                        return result;
                    }
                }

                return enumSMSCheckResult.Normal;
            }
        }
    }

    public enum enumSMSCheckResult
    {
        Normal = 1,
        UserLimited = 2,
        GlobalLimited = 3
    }
}