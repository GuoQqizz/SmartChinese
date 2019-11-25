using AbhsChinese.ReportService.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.ReportService.JobProcess
{
    public class ReportJobProcess : JobProcessBase
    {
        public ReportJobProcess()
        {
            SetInterval(1000);
        }

        protected override IList<JobBase> AddJobs()
        {
            List<JobBase> list = new List<JobBase>();
            list.Add(new ReportJob());
            list.Add(new AdvertisingStatisticJob()); 
            return list;
        }
    }
}
