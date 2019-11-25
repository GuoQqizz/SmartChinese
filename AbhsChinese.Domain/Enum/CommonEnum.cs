using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    class CommonEnum
    {
    }
    public enum RegionTypeEnum
    {
        省 = 1,
        市 = 2,
        区 = 3,
    }

    public enum SexEnum
    {
        男 = 1,
        女 = 2,
    }

    public enum WeekDayEnum
    {
        周一 = 1,
        周二 = 2,
        周三 = 3,
        周四 = 4,
        周五 = 5,
        周六 = 6,
        周日 = 7,
    }

    public enum AccountVerifyTypeEnum
    {
        学生未注册 = 1,
        学生已注册 = 2,
        校区教师未注册 = 3,
        校区教师已注册 = 4,
        教研教师未注册 = 5,
        教研教师已注册 = 6,
        校区找回密码 = 7,
        其他 = 100,
    }

    public enum DateCycleTypeEnum
    {
        Total = 1,
        Year = 2,
        Month = 3,
        Week = 4
    }
}
