using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository
{
    public class AppSetting
    {
        public static string ConnString = ConfigurationManager.ConnectionStrings["AbhsChineseConn"].ConnectionString;
    }
}
