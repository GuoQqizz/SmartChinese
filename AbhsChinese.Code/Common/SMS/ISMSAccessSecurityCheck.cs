using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Code.Common.SMS
{
    public interface ISMSAccessSecurityCheck
    {
        enumSMSCheckResult Check();
    }

    public interface ISMSAccessSecurityCheckUser : ISMSAccessSecurityCheck
    {
        string OpenId
        {
            set; get;
        }
    }
}
