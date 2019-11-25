using AbhsChinese.Bll;
using AbhsChinese.Bll.Message;
using AbhsChinese.Code.Cache;
using AbhsChinese.Code.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbhsChinese.ReportService.Jobs
{
    public class ReportJob : JobBase
    {
        private DateTime nextRunTime = new DateTime(1900, 1, 1);

        public override string JobName()
        {
            return "数据统计";
        }

        public override bool IsTimeToRun(DateTime now)
        {
            if (now >= nextRunTime)
            {
                return true;
            }
            return false;
        }

        public override void Run(DateTime now)
        {
            nextRunTime = new DateTime(1900, 1, 1);

            MessageBll bll = new MessageBll();
            SumStudentBll dailyBll = new SumStudentBll();
            int count = 1;
            while (count <= 10)
            {
                try
                {
                    string message = bll.ReceiveMessage(MessageChannel.REPORT_CHANNEL);
                    if (!string.IsNullOrEmpty(message))
                    {
                        dailyBll.SaveReportData(message);
                        Thread.Sleep(20);
                    }
                    else
                    {
                        nextRunTime = DateTime.Now.AddSeconds(3);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLog(this.JobName(), ex);
                }
                count++;
            }
        }
    }
}