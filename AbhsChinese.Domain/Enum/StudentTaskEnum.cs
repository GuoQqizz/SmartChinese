using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    public enum StudentTaskStatusEnum
    {
        未开始 = 1,
        进行中 = 2,
        已完成 = 3
    }

    public enum StudyTaskTypeEnum
    {
        系统课后任务 = 1,
        老师课后任务 = 2,
        课后练习 = 3
    }

    public enum StudyWrongStatusEnum
    {
        未消除 = 1,
        消除中 = 2,
        已做对 = 3,
        已消除 = 4,
    }

    public enum StudyWrongSourceEnum
    {
        课程学习 = 1,
        课后任务 = 2,
        课后练习 = 3,
    }
}
