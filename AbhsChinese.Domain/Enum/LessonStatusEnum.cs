using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    /// <summary>
    /// 课程状态
    /// </summary>
    public enum LessonStatusEnum
    {
        未编辑 = 1,
        制作中,
        待审批,
        审批中,
        合格,
        不合格
    }
}
