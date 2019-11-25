using AbhsChinese.ReportService.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.ReportService.JobProcess
{
    public class CancelOrderProcess : JobProcessBase
    {
        public CancelOrderProcess()
        {
            SetInterval(5000);
        }

        protected override IList<JobBase> AddJobs()
        {
            List<JobBase> list = new List<JobBase>();
            list.Add(new CancelOrderJob());
            return list;
        }
    }
}
