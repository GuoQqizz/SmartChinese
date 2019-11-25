using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    public enum VoucherStatusEnum
    {
        未领完 = 1,
        已领完 = 2
    }

    public enum StudentCashVoucherStatusEnum
    {
        未使用 = 1,
        已使用 = 2,
        已过期 = 3
    }

    public enum CashVoucherStatusEnum
    {
        已启用 = 1,
        未启用 = 2,
        已关闭 = 3,
        已删除 = 4
    }

    public enum ExpireTypeEnum
    {
        截止日期 = 1,
        固定天数 = 2,
        长期有效 = 3
    }

    public enum CashApplyScopeTypeEnum
    {
        全部课程 = 1,
        指定类型 = 2,
        指定课程 = 3,
    }

    public enum IsAvailableEnum
    {

        学生已拥有且可用的 = 1,
        学生可领取且可用的 = 2
    }
}
