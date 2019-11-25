using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    class StudentEnum
    {
    }
    public enum StudentStatusEnum
    {
        已绑定校区 = 1,
        在读 = 2,
        不在读 = 3,
    }

    public enum StudentAccountSourceEnum
    {
        手机 = 1,
        人脸识别 = 2,
        微信 = 3
    }
    public enum StudentAccountTypeEnum
    {
        客户 = 1,
        用户 = 2
    }

    public enum StudentAccountStatusEnum
    {
        启用 = 1,
        禁用 = 2
    }
}
