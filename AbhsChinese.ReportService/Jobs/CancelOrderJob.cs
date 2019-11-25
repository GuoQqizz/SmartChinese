using AbhsChinese.Bll;
using System;

namespace AbhsChinese.ReportService.Jobs
{
    public class CancelOrderJob : JobBase
    {
        public override string JobName()
        {
            return "系统取消过期未支付订单";
        }

        public override bool IsTimeToRun(DateTime now)
        {
            return true;
        }

        public override void Run(DateTime now)
        {
            StudentOrderBll orderBll = new StudentOrderBll();
            orderBll.CancelOrderAuto();
        }
    }
}
