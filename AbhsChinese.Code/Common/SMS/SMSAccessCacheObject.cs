using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Code.Common.SMS
{
    public class SMSAccessCacheObject
    {
        public SMSAccessCacheObject(int count)
        {
            this.Count = count;
        }

        public int Count
        {
            set;get;
        }
    }
}