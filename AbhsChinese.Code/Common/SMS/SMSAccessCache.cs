using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Code.Common.SMS
{
    public class SMSAccessCache
    {
        public const int UserLimitLevel1Minutes = 1;
        public const int UserLimitLevel2Minutes = 10;
        public const int UserLimitLevel2LockMinutes = 10;
        public const int GlobalLimitLevel1Minutes = 1;
        public const int GlobalLimitLevel2Minutes = 5;

        public static bool GetUserAccessInLimit1(string openId)
        {
            bool result = true;
            string key = "SMS.Cache.User1";
            Dictionary<string, int> obj = HttpRuntime.Cache.Get(key) as Dictionary<string, int>;
            if (obj == null)
            {
                obj = new Dictionary<string, int>();

                HttpRuntime.Cache.Insert(key,
                    obj,
                    null,
                    DateTime.Now.AddMinutes(SMSAccessCache.UserLimitLevel1Minutes),
                    System.Web.Caching.Cache.NoSlidingExpiration,
                    System.Web.Caching.CacheItemPriority.Default,
                    null);

                result = false;
            }
            if (!obj.ContainsKey(openId))
            {
                obj.Add(openId, 1);
                result = false;
            }

            return result;
        }

        public static SMSAccessCacheObject GetUserAccessInLimit2(string openId)
        {
            string key = "SMS.Cache.User2";
            Dictionary<string, SMSAccessCacheObject> obj = HttpRuntime.Cache.Get(key) as Dictionary<string, SMSAccessCacheObject>;
            if (obj == null)
            {
                obj = new Dictionary<string, SMSAccessCacheObject>();

                HttpRuntime.Cache.Insert(key,
                    obj,
                    null,
                    DateTime.Now.AddMinutes(SMSAccessCache.UserLimitLevel2Minutes),
                    System.Web.Caching.Cache.NoSlidingExpiration,
                    System.Web.Caching.CacheItemPriority.Default,
                    null);

            }
            if (!obj.ContainsKey(openId))
            {
                obj.Add(openId, new SMSAccessCacheObject(1));
            }
            else
            {
                obj[openId].Count++;
            }

            return obj[openId];
        }

        public static SMSAccessCacheObject GetGlobalAccessLevel1InLimit()
        {
            string key = "SMS.Cache.Global1";
            SMSAccessCacheObject obj = HttpRuntime.Cache.Get(key) as SMSAccessCacheObject;
            if (obj == null)
            {
                obj = new SMSAccessCacheObject(1);
                HttpRuntime.Cache.Insert(key,
                   obj,
                    null,
                    DateTime.Now.AddMinutes(SMSAccessCache.GlobalLimitLevel1Minutes),
                    System.Web.Caching.Cache.NoSlidingExpiration,
                    System.Web.Caching.CacheItemPriority.Default,
                    null);
            }
            else
            {
                obj.Count++;
            }
            return obj;
        }

        public static SMSAccessCacheObject GetGlobalAccessLevel2InLimit()
        {
            string key = "SMS.Cache.Global2";
            SMSAccessCacheObject obj = HttpRuntime.Cache.Get(key) as SMSAccessCacheObject;
            if (obj == null)
            {
                obj = new SMSAccessCacheObject(1);
                HttpRuntime.Cache.Insert(key,
                    obj,
                    null,
                    DateTime.Now.AddMinutes(SMSAccessCache.GlobalLimitLevel2Minutes),
                    System.Web.Caching.Cache.NoSlidingExpiration,
                    System.Web.Caching.CacheItemPriority.Default,
                    null);
            }
            else
            {
                obj.Count++;
            }
            return obj;
        }

        public static void SetUserAccessInLimit2Lock(string openId)
        {
            string key = "SMS.User2.Lock." + openId;
            HttpRuntime.Cache.Insert(key,
                  1,
                  null,
                  DateTime.Now.AddMinutes(SMSAccessCache.UserLimitLevel2LockMinutes),
                  System.Web.Caching.Cache.NoSlidingExpiration,
                  System.Web.Caching.CacheItemPriority.Default,
                  null);
        }
        public static object GetUserAccessInLimit2Lock(string openId)
        {
            string key = "SMS.User2.Lock." + openId;
            return HttpRuntime.Cache.Get(key);
        }
    }
}