using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    public enum SchoolStatusEnum
    {
        正式运行 = 1,
        营建期 = 2,
        合同到期 = 3,
    }
    public enum ApplyStatusEnum
    {
        申请 = 1,
        同意 = 2,
        拒绝 = 3,
        解绑 = 4,
        关闭 = 10,
    }



    public enum SchoolClassEnum
    {
        未开始 = 1,
        上课中 = 2,
        已结课 = 3,
    }

}
