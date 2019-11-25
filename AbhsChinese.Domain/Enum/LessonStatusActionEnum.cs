using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    /// <summary>
    /// 课程状态流转动作枚举
    /// </summary>
    public enum LessonStatusActionEnum
    {
        创建 = 1,
        编辑,
        完成编辑,
        重新编辑,
        审批
    }
}
