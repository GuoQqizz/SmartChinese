using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Code.Common.SMS
{
    public class SmsModel
    {
        public string Phone
        {
            set; get;
        }

        public int Code
        {
            set; get;
        }

        public string CreateTime
        {
            set; get;
        }

        [JsonIgnore]
        public DateTime ExpireTime
        {
            get
            {
                return Convert.ToDateTime(CreateTime).AddMinutes(5);
            }
        }

        public bool Check(string phone, string code)
        {
            if (Phone.Equals(phone) && Code.ToString().Equals(code) && ExpireTime > DateTime.Now)
            {
                SmsCookie.RemoveSmsCode();
                return true;
            }
            return false;
        }
    }
}