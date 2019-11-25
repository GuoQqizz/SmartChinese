using AbhsChinese.ReportService.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.ReportService.JobProcess
{
    public class MakeLessonTaskProcess : JobProcessBase
    {
        public MakeLessonTaskProcess()
        {
            SetInterval(500);
        }

        protected override IList<JobBase> AddJobs()
        {
            List<JobBase> list = new List<JobBase>();
            list.Add(new MakeLessonTaskJob());
            return list;
        }
    }
}