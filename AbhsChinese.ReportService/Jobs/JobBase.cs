using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.ReportService.Jobs
{
    public abstract class JobBase
    {
        public JobBase()
        {
            RetryTimes = 3;
        }

        public DateTime LastExecTime
        {
            set;
            get;
        }

        public int RetryTimes
        {
            set;
            get;
        }

        public bool LastExecSuccess
        {
            set;
            get;
        }

        public abstract string JobName();
       
        public abstract bool IsTimeToRun(DateTime now);

        public abstract void Run(DateTime now);
    }
}
