using AbhsChinese.Code.Common;
using AbhsChinese.ReportService.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WenDu.NetSchool.WinService.Common;

namespace AbhsChinese.ReportService.JobProcess
{
    public abstract class JobProcessBase
    {
        public bool Stop
        {
            set;
            get;
        }

        protected IList<JobBase> Jobs
        {
            set;
            get;
        }

        protected abstract IList<JobBase> AddJobs();

        private int interval = AppSetting.Interval;

        protected virtual void SetInterval(int intervalMillisecond)
        {
            interval = intervalMillisecond;
        }

        public void Execute()
        {
            Jobs = AddJobs();

            while (true)
            {
                if (Stop)
                {
                    return;
                }

                Thread.Sleep(interval);

                IList<JobBase> jobs = Jobs.Where(item => item.IsTimeToRun(DateTime.Now)).ToList();

                if (jobs.Count > 0)
                {
                    foreach (JobBase job in jobs)
                    {
                        try
                        {
                            string log = job.JobName() + ":Started on " + DateTime.Now;
                            job.Run(DateTime.Now);
                            job.LastExecSuccess = true;
                            log = log + " - Finished on " + DateTime.Now;
                            LogHelper.WriteLog(log);
                        }
                        catch (Exception ex)
                        {
                            job.LastExecSuccess = false;
                            LogHelper.WriteLog(job.JobName() + ":" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                        }
                        finally
                        {
                            job.LastExecTime = DateTime.Now;
                        }
                    }
                }
            }
        }
    }
}
