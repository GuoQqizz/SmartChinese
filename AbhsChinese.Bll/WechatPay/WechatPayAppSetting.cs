using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll.WechatPay
{
    public class WechatPayAppSetting
    {
        public static string WeiXinPayNotifyUrl = ConfigurationManager.AppSettings["WeiXinPayNotifyUrl"];
        public static string WeiXinPayAppId = ConfigurationManager.AppSettings["WeiXinPayAppId"];
        public static string WeiXinPaySecret = ConfigurationManager.AppSettings["WeiXinPaySecret"]; 
        public static string WeiXinPayMchId = ConfigurationManager.AppSettings["WeiXinPayMchid"];
    }
}
