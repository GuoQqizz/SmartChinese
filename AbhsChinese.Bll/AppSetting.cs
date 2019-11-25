using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll
{
    public static class AppSetting
    {
        public static string OssHost = ConfigurationManager.AppSettings["uploadUrl"];
    }
}
